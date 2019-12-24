using Newtonsoft.Json;
using power_aoi.Model;
using power_aoi.Tools;
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
using static power_aoi.Utils;

namespace power_aoi.DockerPanal
{
    public partial class PcbDetails : DockContent
    {
        int checkedNum = 0;
        PartOfPcb partOfPcb;
        TwoSidesPcb twoSidesPcb;
        Main main;
        //public PcbDetails()
        //{
        //    InitializeComponent();
        //}

        public PcbDetails(Main m, PartOfPcb pPcb, TwoSidesPcb tPcb)
        {
            InitializeComponent();
            partOfPcb = pPcb;
            twoSidesPcb = tPcb;
            main = m;

            //#region:序列化字符串

            ////List<long> aaaa = new List<long>();
            ////for (int i = 0; i < 1000; i++)
            ////{
            ////    aaaa.Add(snowflake.nextId());
            ////}

            //Snowflake snowflake = new Snowflake(1);
            //List<Result> lst = new List<Result>();
            //Result result = new Result();
            //result.Id = snowflake.nextId();
            //result.Region = "fuckckckckc";
            ////result.PcbId = 1;
            //lst.Add(result);
            //result = new Result();
            //result.Id = snowflake.nextId();
            //result.Region = "我奥数的撒娇";
            ////result.PcbId = 1;
            //lst.Add(result);
            //result = new Result();
            //result.Id = snowflake.nextId();
            ////result.PcbId = 1;
            //result.Region = "空空空空";
            //lst.Add(result);
            ////省略赋值

            //JsonData<Pcb> obj = new JsonData<Pcb>();
            //obj.data = new Pcb() { results = lst };

            ////序列化为json
            //string json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { /*NullValueHandling = NullValueHandling.Ignore*/ });

            ////反序列化
            //JsonData<Pcb> lst2 = JsonConvert.DeserializeObject<JsonData<Pcb>>(json, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            //#endregion

            //for (int i = 20; i >= 1; i--)
            //{
            //    ListViewItem li = new ListViewItem();
            //    li.SubItems[0].Text = i.ToString();
            //    li.SubItems.Add("aaa");
            //    li.SubItems.Add("25");
            //    li.SubItems.Add("11223344");
            //    li.SubItems.Add(i.ToString());
            //    this.lvList.Items.Add(li);
            //}
            //this.lvList.Items[0].Selected = true;
        }



        /// <summary>
        /// ListView加载数据
        /// </summary>
        /// <param name="pcb"></param>
        public void loadData(Pcb pcb)
        {
            if (pcb.results.Count == 0) return;
            checkedNum = 0;
            lbPcbNumber.Text = pcb.PcbNumber;
            lbSurfaceNumber.Text = pcb.SurfaceNumber.ToString();
            lbPcbWidth.Text = pcb.PcbWidth.ToString();
            lbPcbHeight.Text = pcb.PcbHeight.ToString();
            lbPcbChildenNumber.Text = pcb.PcbChildenNumber.ToString();
            foreach (var item in pcb.results)
            {
                ListViewItem li = new ListViewItem();
                li.BackColor = Color.Red;
                li.SubItems[0].Text = item.PcbId.ToString();
                li.SubItems.Add(item.IsBack.ToString());
                li.SubItems.Add(pcb.PcbPath);
                li.SubItems.Add(item.PartImagePath);
                li.SubItems.Add(item.Id.ToString());
                li.SubItems.Add(item.Area);
                li.SubItems.Add(item.NgType);
                li.SubItems.Add("未判定");

                lvList.Items.Add(li);
            }
            lvList.Items[0].Selected = true;
        }

        public bool hasListViewUncheck()
        {
            foreach(ListViewItem li in lvList.Items)
            {
                if(li.SubItems[7].Text == "未判定")
                {
                    li.Selected = true;
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// ListView跳转到下一行
        /// </summary>
        /// <param name="res"></param>
        public void lvListNextItemSelect(string res)
        {
            try
            {
                int index = 0;
                if (lvList.SelectedItems.Count == 0)
                {
                    index = 0;
                }
                else
                {
                    index = lvList.SelectedItems[0].Index;
                }

                if (res == "OK")
                {
                    lvList.Items[index].BackColor = Color.Green;
                    #region 更新数据库
                    try
                    {
                        AoiModel aoiModel = DB.GetAoiModel();
                        Result users = aoiModel.results.Find(lvList.Items[index].SubItems[4].Text);
                        users.IsMisjudge = 1;
                        Pcb pcb = aoiModel.pcbs.Find(users.PcbId);
                        pcb.IsMisjudge = 1;
                        int result = aoiModel.SaveChanges();
                        aoiModel.Dispose();
                    }
                    catch (Exception err)
                    {

                    }
                    #endregion
                }
                else
                {
                    lvList.Items[index].BackColor = Color.Yellow;
                }
                lvList.Items[index].SubItems[7].Text = res;



                // 判断是否到最后一行
                if (index + 1 >= lvList.Items.Count)
                {
                    #region 查询是否存在未校验的数据
                    if (!hasListViewUncheck())
                    {
                        main.doLeisure();
                        return;
                    }
                    #endregion
                }
                // 主要应用于，客户手动选了行，造成前面有些未验证
                if (++checkedNum >= lvList.Items.Count)
                {
                    main.doLeisure();
                    return;
                }
                lvList.Items[index].Selected = false;
                lvList.Items[index + 1].Selected = true;
                twoSidesPcb.tabControl.SelectedIndex = int.Parse(lvList.Items[index + 1].SubItems[1].Text);
                partOfPcb.showImg(lvList.Items[index].SubItems[2].Text + "/" + lvList.Items[index].SubItems[3].Text);

            }
            catch (Exception err)
            {
                LogHelper.WriteLog("update result", err);
            }
        }

        private void lvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvList.SelectedItems.Count != 0)
            {
                if (lvList.SelectedItems[0].Index + 1 > lvList.Items.Count)
                {
                    main.doLeisure();
                }
            }
      
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lvListNextItemSelect("OK");
        }

        private void btnNG_Click(object sender, EventArgs e)
        {
            lvListNextItemSelect("NG");
        }
    }
}
