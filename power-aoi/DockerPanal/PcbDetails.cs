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

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            ////声明movie类
            //List<movie> lstmovie = new List<movie> {
            //    new movie{ Title="速度与激情系列1",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列2",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列3",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列4",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列5",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列6",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列7",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //    new movie{ Title="速度与激情系列8",Genre="动作",ReleaseDate=DateTime.Now,Price=50 },
            //};
            //DB.GetAoiModel().movies.AddRange(lstmovie);
            //if (DB.GetAoiModel().SaveChanges() > 0) { }
        }

        /// <summary>
        /// ListView加载数据
        /// </summary>
        /// <param name="pcb"></param>
        public void loadData(Pcb pcb)
        {
            gbPcb.Text = $"板号: {pcb.PcbNumber}";
            foreach (var item in pcb.results)
            {
                ListViewItem li = new ListViewItem();
                li.BackColor = Color.Red;
                li.SubItems[0].Text = item.PcbId.ToString();
                li.SubItems.Add(item.IsFront.ToString());
                li.SubItems.Add(item.PartImagePath);
                li.SubItems.Add(item.Id.ToString());
                li.SubItems.Add(item.Area);
                li.SubItems.Add(item.NgType);
                li.SubItems.Add("未判定");

                lvList.Items.Add(li);
            }
            lvList.Items[0].Selected = true;
        }

        /// <summary>
        /// ListView跳转到下一行
        /// </summary>
        /// <param name="res"></param>
        public void lvListNextItemSelect(string res)
        {
            try
            {
                int index = lvList.SelectedItems[0].Index;
                lvList.Items[index].BackColor = Color.Green;

                lvList.Items[index].SubItems[6].Text = res;
                if (index + 1 >= lvList.Items.Count)
                {
                    main.doLeisure();
                }
                lvList.Items[index].Selected = false;
                lvList.Items[index + 1].Selected = true;
            }
            catch(Exception err) { }
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
    }
}
