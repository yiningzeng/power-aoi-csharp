using Amib.Threading;
using power_aoi.SqlPars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace power_aoi.DockerPanalSearch
{
    public partial class Query : DockContent
    {
        QueryPars queryPars = new QueryPars();
        MainSearch mainSearch;
        public Query(MainSearch qForm)
        {
            InitializeComponent();
            mainSearch = qForm;
        }

        public void DoSearch(QueryPars q)
        {
            queryPars = q;
            if (tabControl1.SelectedIndex == 0)
            {
                Tab0Ini(q);
                mainSearch.SetQueryCriteriaControlEnable(true);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                Tab1Ini(q);
                mainSearch.SetQueryCriteriaControlEnable(false);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                Tab2Ini(q);
                mainSearch.SetQueryCriteriaControlEnable(false);
            }
        }

        #region 通用函数
        public void TopGridView(DataGridView dataGridView)
        {
            SmartThreadPool smartThreadPool = new SmartThreadPool();
            AoiModel aoiModel = DB.GetAoiModel();
            Action<DataGridView> t = (control) =>
            {
                lock (aoiModel)
                {
                    string AllPcbNums = "(select count(*) from pcbs)";
                    string PassPcbNums = "(select count(*) from pcbs where is_misjudge = 0 and is_error = 0)";
                    string ErrorPcbNums = "(select count(*) from pcbs)";
                    string MisreportPcbNums = "(select count(*) from pcbs where is_misjudge = 1)";
                    string MisreportRate = MisreportPcbNums + "/" + AllPcbNums;
                    string PassRate = PassPcbNums + "/" + AllPcbNums;
                    string final = "select " + AllPcbNums + " as AllPcbNums, "
                    + ErrorPcbNums + " as ErrorPcbNums, "
                    + MisreportPcbNums + " as MisreportPcbNums, "
                    + MisreportRate + " as MisreportRate, "
                    + PassRate + "as PassRate";
                    var tables = aoiModel.Database.SqlQuery<SqlQueryData1>(final).ToList(); //.SqlQuery("select *from BlogMaster where UserId='3'");
                    this.BeginInvoke((Action)(() =>
                    {
                        bindingSourceAll.DataSource = tables;
                        control.DataSource = bindingSourceAll;
                    }));
                    aoiModel.Dispose();
                }
            };
            smartThreadPool.QueueWorkItem<DataGridView>(t, dataGridView);
        }
        #endregion

        #region Tab原始数据
        public void Tab0Ini(QueryPars q)
        {
            TopGridView(dgvAll);

            queryPars.nums = int.Parse(combPageNums.Text);
            queryPars.pages = int.Parse(tbNowPage.Text)-1;
            #region 开启线程查询数据库
            SmartThreadPool smartThreadPool = new SmartThreadPool();
            AoiModel aoiModel = DB.GetAoiModel();
            Action<QueryPars> t = (v) =>
            {
                lock (aoiModel)
                {
                    var db = aoiModel.pcbs.Where(p => p.CreateTime >= queryPars.startTime
                                                    && p.CreateTime <= queryPars.endTime);
                    if (queryPars.pcbName != null && queryPars.pcbNumber == null)
                    {
                        db = aoiModel.pcbs.Where(p => p.CreateTime >= queryPars.startTime
                                                    && p.CreateTime <= queryPars.endTime
                                                    && p.PcbName == queryPars.pcbName);
                    }
                    else if (queryPars.pcbName == null && queryPars.pcbNumber != null)
                    {
                        if (queryPars.searchPcbNumberAccurate)
                        {
                            db = aoiModel.pcbs.Where(p => p.CreateTime >= queryPars.startTime
                                                        && p.CreateTime <= queryPars.endTime
                                                        && p.PcbNumber == queryPars.pcbNumber);
                        }
                        else
                        {
                            db = aoiModel.pcbs.Where(p => p.CreateTime >= queryPars.startTime
                                                        && p.CreateTime <= queryPars.endTime
                                                        && p.PcbNumber.Contains(queryPars.pcbNumber));
                        }
                    }
                    else if (queryPars.pcbName != null && queryPars.pcbNumber != null)
                    {
                        if (queryPars.searchPcbNumberAccurate)
                        {
                            db = aoiModel.pcbs.Where(p => p.CreateTime >= queryPars.startTime
                                                        && p.CreateTime <= queryPars.endTime
                                                        && p.PcbNumber == queryPars.pcbNumber
                                                        && p.PcbName == queryPars.pcbName);
                        }
                        else
                        {
                            db = aoiModel.pcbs.Where(p => p.CreateTime >= queryPars.startTime
                                                        && p.CreateTime <= queryPars.endTime
                                                        && p.PcbNumber.Contains(queryPars.pcbNumber)
                                                        && p.PcbName == queryPars.pcbName);
                        }
                        
                    }
                    var list = db.OrderByDescending(p => p.CreateTime)
                    .Skip(queryPars.pages * queryPars.nums)
                    .Take(queryPars.nums)
                    .ToList();


                    //#region 最上面的报表
                    ///**select (select count(*) from pcbs) as AllPcbNums,
                    // * (select count(*) from pcbs where is_error = 1) as ErrorPcbNums,
                    // * (select count(*) from pcbs where is_misjudge = 1) as MisreportPcbNums,
                    // * (select count(*) from pcbs where is_misjudge = 1)/(select count(*) from pcbs) as MisreportRate,
                    // * (select count(*) from pcbs where is_misjudge = 0 and is_error = 0)/(select count(*) from pcbs) as PassRate;
                    // * **/
                    //string AllPcbNums = "(select count(*) from pcbs)";
                    //string PassPcbNums = "(select count(*) from pcbs where is_misjudge = 0 and is_error = 0)";
                    //string ErrorPcbNums = "(select count(*) from pcbs)";
                    //string MisreportPcbNums = "(select count(*) from pcbs where is_misjudge = 1)";
                    //string MisreportRate = MisreportPcbNums + "/" + AllPcbNums;
                    //string PassRate = PassPcbNums + "/" + AllPcbNums;
                    //string final = "select " + AllPcbNums + " as AllPcbNums, "
                    //+ ErrorPcbNums + " as ErrorPcbNums, "
                    //+ MisreportPcbNums + " as MisreportPcbNums, "
                    //+ MisreportRate + " as MisreportRate, " 
                    //+ PassRate + "as PassRate";
                    //var tables = aoiModel.Database.SqlQuery<SqlQueryData1>(final).ToList(); //.SqlQuery("select *from BlogMaster where UserId='3'");
                    //this.BeginInvoke((Action)(() =>
                    //{
                    //    bindingSourceAll.DataSource = tables;
                    //    dgvAll.DataSource = bindingSourceAll;
                    //}));
                    //#endregion

                    #region 更新底部页数据
                    int totalRows = totalRows = db.Count();//aoiModel.Database.SqlQuery<int>("select count(*) from pcbs").ToArray()[0];
                    int totalPage = 1;
                    if(totalRows > queryPars.nums)
                    {
                        totalPage = (totalRows + v.nums - 1) / v.nums;
                    }
                    this.BeginInvoke((Action)(() =>
                    {
                        lbTotalPages.Text = totalPage + "";
                        lbTotalRows.Text = totalRows + "";

                    }));
                    #endregion


                    aoiModel.Dispose();
                    this.BeginInvoke((Action)(() =>
                    {
                        bindingSourcePcbs.DataSource = list;
                        dgvOriginalData.DataSource = bindingSourcePcbs;
                    }));

                }
            };
            smartThreadPool.QueueWorkItem<QueryPars>(t, queryPars);
            #endregion
        }

        private void tbNowPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void tbNowPage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(tbNowPage.Text == "")
                {
                    tbNowPage.Text = "1";
                }
                if (int.Parse(tbNowPage.Text) <= 1)
                {
                    tbNowPage.Text = "1";
                }
            }
            catch(Exception err)
            {

            }

        }

        private void btnGoPage_Click(object sender, EventArgs e)
        {
            Tab0Ini(queryPars);
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            try
            {
                tbNowPage.Text = int.Parse(tbNowPage.Text) - 1 + "";
                Tab0Ini(queryPars);
            }
            catch(Exception er) { }

        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            try
            {
                tbNowPage.Text = int.Parse(tbNowPage.Text) + 1 + "";
                Tab0Ini(queryPars);
            }
            catch (Exception er) { }
        }

        private void combPageNums_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                queryPars.nums = int.Parse(combPageNums.Text);
                Tab0Ini(queryPars);
            }
            catch (Exception er) { }
        }

        #endregion


        #region 缺陷分析
        public void Tab1Ini(QueryPars q)
        {
            TopGridView(dgvAll2);

            SmartThreadPool smartThreadPool = new SmartThreadPool();
            AoiModel aoiModel = DB.GetAoiModel();
            Action<QueryPars> t = (v) =>
            {
                lock (aoiModel)
                {
                    var tables = aoiModel.Database.SqlQuery<SqlQueryData2NG>("select ng_type as Type, count(*) as Nums from results where create_time between '" +
                        v.startTime +
                        "' and '" +
                        v.endTime +
                        "' GROUP BY  ng_type").ToList(); //.SqlQuery("select *from BlogMaster where UserId='3'");
                    this.BeginInvoke((Action)(() =>
                    {
                        bindingSourceTab1.DataSource = tables;
                    }));

                    var xData = aoiModel.Database.SqlQuery<string>("select ng_type as Type from results where create_time between '" +
                        v.startTime +
                        "' and '" +
                        v.endTime +
                        "' GROUP BY  ng_type ORDER BY ng_type").ToList();
                    var yData = aoiModel.Database.SqlQuery<string>("select count(*) * 100 /(select count(*) from results where create_time between '"+
                        v.startTime + "' and '"+
                        v.endTime + "') as rate " +
                        " from results where create_time between '" +
                        v.startTime +
                        "' and '" +
                        v.endTime +
                        "' GROUP BY  ng_type ORDER BY ng_type").ToList();
                    this.BeginInvoke((Action)(()=>
                    {
                        chartTab1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                        chartTab1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                        chartTab1.Series[0].Points.DataBindXY(xData, yData);
                    }));
                    aoiModel.Dispose();
                }
            };
            smartThreadPool.QueueWorkItem<QueryPars>(t, q);
        }
        #endregion

        #region 误检分析
        public void Tab2Ini(QueryPars q)
        {
            TopGridView(dgvAll3);

            SmartThreadPool smartThreadPool = new SmartThreadPool();
            AoiModel aoiModel = DB.GetAoiModel();
            Action<QueryPars> t = (v) =>
            {
                lock (aoiModel)
                {
                    var tables = aoiModel.Database.SqlQuery<SqlQueryData2NG>("select ng_type as Type, count(*) as Nums from results where create_time between '" +
                        v.startTime +
                        "' and '" +
                        v.endTime +
                        "' GROUP BY  ng_type").ToList(); //.SqlQuery("select *from BlogMaster where UserId='3'");
                    this.BeginInvoke((Action)(() =>
                    {
                        bindingSourceTab2.DataSource = tables;
                    }));

                    var xData = aoiModel.Database.SqlQuery<string>("select ng_type as Type from results where is_misjudge = 1 and create_time between '" +
                        v.startTime +
                        "' and '" +
                        v.endTime +
                        "' GROUP BY  ng_type ORDER BY ng_type").ToList();
                    var yData = aoiModel.Database.SqlQuery<string>("select count(*) * 100 /(select count(*) from results where create_time between '" +
                        v.startTime + "' and '" +
                        v.endTime + "') as rate " +
                        " from results where  is_misjudge = 1 and create_time between '" +
                        v.startTime +
                        "' and '" +
                        v.endTime +
                        "' GROUP BY  ng_type ORDER BY ng_type").ToList();
                    this.BeginInvoke((Action)(() =>
                    {
                        chartTab2.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                        chartTab2.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                        chartTab2.Series[0].Points.DataBindXY(xData, yData);
                    }));
                    aoiModel.Dispose();
                }
            };
            smartThreadPool.QueueWorkItem<QueryPars>(t, q);
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (queryPars.startTime != Convert.ToDateTime("0001/1/1 0:00:00"))
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    Tab0Ini(queryPars);
                    mainSearch.SetQueryCriteriaControlEnable(true);
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    Tab1Ini(queryPars);
                    mainSearch.SetQueryCriteriaControlEnable(false);
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    Tab2Ini(queryPars);
                    mainSearch.SetQueryCriteriaControlEnable(false);
                }
            }
            else
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    mainSearch.SetQueryCriteriaControlEnable(true);
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    mainSearch.SetQueryCriteriaControlEnable(false);
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    mainSearch.SetQueryCriteriaControlEnable(false);
                }
            }

        }
    }
}
