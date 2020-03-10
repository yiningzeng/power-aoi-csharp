using ImageProcessor;
using Newtonsoft.Json;
using power_aoi.Model;
using power_aoi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static power_aoi.Utils;

namespace power_aoi.DockerPanal
{
    public partial class PcbDetails : DockContent
    {

        PartOfPcb partOfPcb;
        TwoSidesPcb twoSidesPcb;
        Main main;
        Bitmap bitmapFront = null;
        Bitmap bitmapBack = null;

        int needCheckNumAll = 0;
        
        int checkedNum = 0;

        // listview 通用的参数，复用
        int selectIndex;
        ListView selectListView;

        public PcbDetails(Main m, PartOfPcb pPcb, TwoSidesPcb tPcb)
        {
            InitializeComponent();
            partOfPcb = pPcb;
            twoSidesPcb = tPcb;
            main = m;


            this.lvListFront.SelectedIndexChanged += new System.EventHandler(this.lvList_SelectedIndexChanged);
            this.lvListBack.SelectedIndexChanged += new System.EventHandler(this.lvList_SelectedIndexChanged);

            #region 序列化字符串

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
            #endregion
        }

        public void listSwitch(bool isback){
            if (isback)
            {
                tabListView.SelectedIndex = 1;

            }
            else
            {
                tabListView.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// ListView加载数据
        /// </summary>
        /// <param name="pcb"></param>
        public void loadData(Pcb pcb)
        {
            tabListView.SelectedIndex = 0;

            if (bitmapFront != null)
            {
                bitmapFront.Dispose();
                bitmapFront = null;
            }
            if (bitmapBack != null)
            {
                bitmapBack.Dispose();
                bitmapBack = null;
            }
            string frontImgPath = ConfigurationManager.AppSettings["FtpPath"] + pcb.Id + "/front.jpg";
            string backImgPath = ConfigurationManager.AppSettings["FtpPath"] + pcb.Id + "/back.jpg";
            if (File.Exists(frontImgPath))
            {
                bitmapFront = new Bitmap(frontImgPath);
                twoSidesPcb.showFrontImg(bitmapFront);
            }
            if (File.Exists(backImgPath))
            {
                bitmapBack = new Bitmap(backImgPath);
                twoSidesPcb.showBackImg(bitmapBack);
            }
            if (pcb.results.Count == 0)
            {
                main.doLeisure(false);
                lbPcbNumber.Text = pcb.PcbNumber;
                lbSurfaceNumber.Text = pcb.SurfaceNumber.ToString();
                lbPcbWidth.Text = pcb.PcbWidth.ToString();
                lbPcbHeight.Text = pcb.PcbHeight.ToString();
                lbPcbChildenNumber.Text = pcb.PcbChildenNumber.ToString();
                lbResult.Text = "OK";
                lbResult.ForeColor = Color.Green;

                partOfPcb.showImg(null);
                return;
            }
            
            needCheckNumAll = pcb.results.Count;
            checkedNum = 0;

            lbPcbNumber.Text = pcb.PcbNumber;
            lbSurfaceNumber.Text = pcb.SurfaceNumber.ToString();
            lbPcbWidth.Text = pcb.PcbWidth.ToString();
            lbPcbHeight.Text = pcb.PcbHeight.ToString();
            lbPcbChildenNumber.Text = pcb.PcbChildenNumber.ToString();
            lbResult.Text = "NG";
            lbResult.ForeColor = Color.Red;
            foreach (var item in pcb.results)
            {
                ListViewItem li = new ListViewItem();
                li.BackColor = Color.Red;
                li.SubItems[0].Text = item.PcbId.ToString();
                li.SubItems.Add(item.IsBack.ToString());
                li.SubItems.Add(pcb.PcbPath);
                li.SubItems.Add(item.PartImagePath);
                li.SubItems.Add(item.Region);
                li.SubItems.Add(item.Id.ToString());
                li.SubItems.Add(item.Area);
                li.SubItems.Add(item.NgType);
                li.SubItems.Add("未判定");
                if(item.IsBack == 0)
                {
                    lvListFront.Items.Add(li);
                }
                else if(item.IsBack == 1)
                {
                    lvListBack.Items.Add(li);
                }
            }
            ImageList ImgList = new ImageList();
            //高度设为25
            ImgList.ImageSize = new Size(1, 25);
            //在Details显示模式下，小图标才会起作用
            lvListFront.SmallImageList = ImgList;
            lvListBack.SmallImageList = ImgList;

            selectListView = lvListFront;
            lvListNextItemSelect("未判定");
            lvListFront.Select();
            if (lvListFront.Items.Count > 0) lvListFront.SelectedIndices.Add(0);
            if (lvListBack.Items.Count > 0) lvListBack.SelectedIndices.Add(0);
            LogHelper.WriteLog("接收到数据》界面显示\n");
            //partOfPcb.showImg(lvList.Items[0].SubItems[2].Text + "/" + lvList.Items[0].SubItems[3].Text);
        }

        /// <summary>
        /// 判断listview列表是否存在未判定的数据
        /// </summary>
        /// <param name="isBack">是否是背面</param>
        /// <returns></returns>
        public bool hasListViewUncheck()
        {
            foreach (ListViewItem li in selectListView.Items)
            {
                if (li.SubItems[8].Text == "未判定")
                {
                    return true;
                }
            }
            return false;
        }

        public void cutBitmapShow(int index)
        {
            #region 截图显示下一个
            try
            {
                string tFilePath = ConfigurationManager.AppSettings["FtpPath"] + selectListView.Items[index].SubItems[2].Text + "\\" + selectListView.Items[index].SubItems[3].Text;
                //if (!File.Exists(tFilePath))
                {
                    Bitmap resBitmap = null;
                    string[] reg = selectListView.Items[index].SubItems[4].Text.Split(',');
                    Rectangle rect = new Rectangle(
                            int.Parse(reg[0]),
                            int.Parse(reg[1]),
                            int.Parse(reg[2]),
                            int.Parse(reg[3]));
                    Rectangle oldRect = rect;
                    if (selectListView.Items[index].SubItems[1].Text == "0") // 正面
                    {
                        //Bitmap drawBitmap = Utils.DrawRect(bitmapFront, rect, lvList.Items[index].SubItems[7].Text);
                        rect.Inflate(250, 250);
                        resBitmap = Utils.BitmapCut(bitmapFront, rect);//.Save(tFilePath);
                        twoSidesPcb.BeginInvoke((Action)(() =>
                        {
                            twoSidesPcb.pictureBoxDraw(true, rect);
                        }));
                    }
                    else if (selectListView.Items[index].SubItems[1].Text == "1") // 背面
                    {
                        //Bitmap drawBitmap = Utils.DrawRect(bitmapBack, rect, lvList.Items[index].SubItems[7].Text);
                        rect.Inflate(250, 250);
                        resBitmap = Utils.BitmapCut(bitmapBack, rect);//.Save(tFilePath);
                        twoSidesPcb.BeginInvoke((Action)(() =>
                        {
                            twoSidesPcb.pictureBoxDraw(false, rect);
                        }));
                    }

                    Rectangle newRect = new Rectangle(oldRect.X - rect.X - oldRect.Width / 2, oldRect.Y - rect.Y - oldRect.Height / 2, oldRect.Width, oldRect.Height);
                    //resBitmap.Save(tFilePath);
                    partOfPcb.BeginInvoke((Action)(() =>
                    {
                        try
                        {
                            partOfPcb.showImgThread(resBitmap, newRect, selectListView.Items[index].SubItems[7].Text);
                        }
                        catch (Exception er)
                        { }
                    }));
   
                }
             
                //partOfPcb.showImg(lvList.Items[index].SubItems[2].Text + "/" + lvList.Items[index].SubItems[3].Text);

            }
            catch (Exception er)
            {
            }
            #endregion
        }

        /// <summary>
        /// ListView跳转到下一行
        /// </summary>
        /// <param name="res"></param>
        public void lvListNextItemSelect(string res)
        {
            try
            {
                selectListView.Select();
                int index = selectIndex;
                if (selectListView.SelectedItems.Count == 0)
                {
                    index = 0;
                }
                if (res == "OK")
                {
                    if (selectListView.Items[index].SubItems[8].Text == "未判定") checkedNum++; // 只有在未判定更改状态后才+1

                    selectListView.Items[index].BackColor = Color.Green;
                    #region 更新数据库
                    try
                    {
                        AoiModel aoiModel = DB.GetAoiModel();
                        Result users = aoiModel.results.Find(selectListView.Items[index].SubItems[5].Text);
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
                else if(res == "NG")
                {
                    if (selectListView.Items[index].SubItems[8].Text == "未判定") checkedNum++; // 只有在未判定更改状态后才+1

                    selectListView.Items[index].BackColor = Color.Yellow;
                }
                else
                {
                    return;
                }
                selectListView.Items[index].SubItems[8].Text = res;

                // 判断是否到最后一行
                if (index + 1 >= selectListView.Items.Count) // 是最后一行
                {
                    #region 查询是否存在未校验的数据
                    if (!hasListViewUncheck()) // 数据校验完毕
                    {
                        //main.doLeisure(true);
                        // 主要应用于，客户手动选了行，造成前面有些未验证
                        if (checkedNum >= needCheckNumAll)
                        {
                            main.doLeisure(true);
                            bitmapFront.Dispose();
                            bitmapBack.Dispose();
                        }
                        else
                        {
                            if(tabListView.SelectedIndex == 0)
                            {
                                tabListView.SelectedIndex = 1;
                            }
                            else if(tabListView.SelectedIndex == 1)
                            {
                                tabListView.SelectedIndex = 0;
                            }
                        }
                        return;
                    }
                    else // 未校验完毕，返回第一行从新开始
                    {
                        selectListView.SelectedIndices.Remove(index);
                        index = 0;
                        selectListView.SelectedIndices.Add(index);
                        //由于光标不会跟着移动，需要手动设置
                        selectListView.FocusedItem = selectListView.Items[index];
                    }
                    #endregion
                }
                else
                {
                    selectListView.SelectedIndices.Remove(index);
                    selectListView.SelectedIndices.Add(++index);
                    //由于光标不会跟着移动，需要手动设置
                    selectListView.FocusedItem = selectListView.Items[index];
                }
                if (checkedNum >= needCheckNumAll)
                {
                    main.doLeisure(true);
                    bitmapFront.Dispose();
                    bitmapBack.Dispose();
                }




            }
            catch (Exception err)
            {
                LogHelper.WriteLog("update result", err);
            }
        }

        private void lvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectListView.SelectedIndices != null && selectListView.SelectedIndices.Count > 0)
            {
                selectIndex = selectListView.SelectedItems[0].Index;
                // 切换正反面
                //twoSidesPcb.tabControl.SelectedIndex = int.Parse(lvListFront.Items[selectIndex].SubItems[1].Text);
                // 确保index行可见，必要时滚动
                //if(lvList.EnsureVisible)
                selectListView.EnsureVisible(selectIndex);
                //截图并显示
                cutBitmapShow(selectListView.SelectedItems[0].Index);

            }
        }



        /// <summary>
        /// 缺陷列表切换函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabListView.SelectedIndex == 0)
            {
                selectListView = lvListFront;
            }else if(tabListView.SelectedIndex == 1)
            {
                selectListView = lvListBack;
            }
            if (selectListView.SelectedIndices.Count > 0)
            {
                selectIndex = selectListView.SelectedIndices[0];
            }
            else
            {
                selectIndex = 0;
            }

            selectListView.Select();
            cutBitmapShow(selectIndex);

            twoSidesPcb.BeginInvoke((Action)(() => {
                twoSidesPcb.tabControl.SelectedIndex = tabListView.SelectedIndex;
            }));
        }

        #region 底部四个button
        private void btnOK_Click(object sender, EventArgs e)
        {
            lvListNextItemSelect("OK");
        }

        private void btnNG_Click(object sender, EventArgs e)
        {
            lvListNextItemSelect("NG");
        }
        private void btnLeft_Click(object sender, EventArgs e)
        {
            tabListView.SelectedIndex = 0;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            tabListView.SelectedIndex = 1;
        }
        #endregion
    }
}
