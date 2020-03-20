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
using DRectangle = System.Drawing.Rectangle;
using DPoint = System.Drawing.Point;
using RRectangle = RTree.Rectangle;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using RTree;

namespace power_aoi.DockerPanal
{
    public partial class PcbDetails : DockContent
    {
        public class XBoard
        {
            public Mat matImg;
            public bool isBack = false;
            public RTree<DRectangle> badTree = new RTree<DRectangle>();
            public RTree<DRectangle> okTree = new RTree<DRectangle>();

            public XBoard(string path, bool isBack)
            {
                matImg = new Mat(path, Emgu.CV.CvEnum.LoadImageType.AnyColor);
                this.isBack = isBack;
            }
        }
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

        //RTree<DRectangle> frontTree = new RTree<DRectangle>();
        //RTree<DRectangle> backTree = new RTree<DRectangle>();

        Pcb nowWorkingPcb;

        int xboardDoneNum = 0;
        XBoard frontBoard;
        XBoard backBoard;

        List<DRectangle> frontMarkerCheckArea = new List<DRectangle>();
        List<DRectangle> backMarkerCheckArea = new List<DRectangle>();
        Mat marker = new Mat(Application.StartupPath + "/marker.jpg", Emgu.CV.CvEnum.LoadImageType.AnyColor);
        Mat badMarker = new Mat(Application.StartupPath + "/bad_marker.jpg", Emgu.CV.CvEnum.LoadImageType.AnyColor);
        double threshold = Convert.ToDouble(ConfigurationManager.AppSettings["MarkerThreshold"]);//0.7;

        public PcbDetails(Main m, PartOfPcb pPcb, TwoSidesPcb tPcb)
        {
            InitializeComponent();

            #region marker点检测区域
            backMarkerCheckArea.Add(new DRectangle(1031, 3298, 863, 475));
            backMarkerCheckArea.Add(new DRectangle(2743, 3257, 781, 493));
            backMarkerCheckArea.Add(new DRectangle(4417, 3245, 724, 502));
            backMarkerCheckArea.Add(new DRectangle(6022, 3221, 811, 535));
            backMarkerCheckArea.Add(new DRectangle(7649, 3224, 847, 511));
            backMarkerCheckArea.Add(new DRectangle(9342, 3242, 823, 467));
            backMarkerCheckArea.Add(new DRectangle(10981, 3217, 796, 511));
            backMarkerCheckArea.Add(new DRectangle(12658, 3247, 769, 496));

            frontMarkerCheckArea.Add(new DRectangle(1254, 3277, 983, 473));
            frontMarkerCheckArea.Add(new DRectangle(2970, 3251, 887, 511));
            frontMarkerCheckArea.Add(new DRectangle(4639, 3295, 861, 477));
            frontMarkerCheckArea.Add(new DRectangle(6287, 3307, 858, 507));
            frontMarkerCheckArea.Add(new DRectangle(7867, 3319, 995, 517));
            frontMarkerCheckArea.Add(new DRectangle(9573, 3355, 923, 517));
            frontMarkerCheckArea.Add(new DRectangle(11215, 3375, 881, 523));
            frontMarkerCheckArea.Add(new DRectangle(12907, 3403, 869, 521));
            #endregion

            #region Rtree marker点检测区域，反馈对应的板子区域
            //backTree.Add(new RRectangle(1031, 3298, 1031 + 863, 3298 + 475, 0, 0), new DRectangle(new DPoint(724, 594), new Size(1453, 2664)));
            //backTree.Add(new RRectangle(2743, 3257, 2743 + 781, 3257 + 493, 0, 0), new DRectangle(new DPoint(2389, 585), new Size(1435, 2706)));
            //backTree.Add(new RRectangle(4417, 3245, 4417 + 724, 3245 + 502, 0, 0), new DRectangle(new DPoint(4084, 600), new Size(1402, 2691)));
            //backTree.Add(new RRectangle(6022, 3221, 6022 + 811, 3221 + 535, 0, 0), new DRectangle(new DPoint(5737, 600), new Size(1399, 2654)));
            //backTree.Add(new RRectangle(7649, 3224, 7649 + 847, 3224 + 511, 0, 0), new DRectangle(new DPoint(7364, 568), new Size(1411, 2660)));
            //backTree.Add(new RRectangle(9342, 3242, 9342 + 823, 3242 + 467, 0, 0), new DRectangle(new DPoint(9042, 598), new Size(1359, 2618)));
            //backTree.Add(new RRectangle(10981, 3217, 10981 + 796, 3217 + 511, 0, 0), new DRectangle(new DPoint(10681, 576), new Size(1414, 2675)));
            //backTree.Add(new RRectangle(12658, 3247, 12658 + 769, 3247 + 496, 0, 0), new DRectangle(new DPoint(12358, 570), new Size(1348, 2660)));

            //frontTree.Add(new RRectangle(1254, 3277, 1254 + 983, 3277 + 473, 0, 0), new DRectangle(new DPoint(1096, 592), new Size(1381, 2661)));
            //frontTree.Add(new RRectangle(2970, 3251, 2970 + 887, 3251 + 511, 0, 0), new DRectangle(new DPoint(2734, 607), new Size(1354, 2691)));
            //frontTree.Add(new RRectangle(4639, 3295, 4639 + 861, 3295 + 477, 0, 0), new DRectangle(new DPoint(4387, 643), new Size(1369, 2682)));
            //frontTree.Add(new RRectangle(6287, 3307, 6287 + 858, 3307 + 507, 0, 0), new DRectangle(new DPoint(6022, 703), new Size(1333, 2651)));
            //frontTree.Add(new RRectangle(7867, 3319, 7867 + 995, 3319 + 517, 0, 0), new DRectangle(new DPoint(7668, 718), new Size(1363, 2624)));
            //frontTree.Add(new RRectangle(9573, 3355, 9573 + 923, 3355 + 517, 0, 0), new DRectangle(new DPoint(9324, 706), new Size(1363, 2666)));
            //frontTree.Add(new RRectangle(11215, 3375, 11215 + 881, 3375 + 523, 0, 0), new DRectangle(new DPoint(10995, 775), new Size(1366, 2633)));
            //frontTree.Add(new RRectangle(12907, 3403, 12907 + 869, 3403 + 521, 0, 0), new DRectangle(new DPoint(12630, 748), new Size(1372, 2678)));
            #endregion

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


        /// <summary>
        /// 增加坏板的区域， 里面存储的都是坏板的直接区域
        /// </summary>
        /// <param name="i"></param>
        /// <param name="xboard"></param>
        private void xBoardAddTree(int i,double dres, XBoard xboard)
        {
            DRectangle dRectangle;
            //只有匹配结果分值较高时才新增
            if(dres <= threshold)
            {
                Console.WriteLine("加入加入:" +i+"  "+ dres+"     " +xboard.isBack);
                switch (i)
                {
                    case 0:
                        if (xboard.isBack)
                        {
                            dRectangle = new DRectangle(new DPoint(724, 594), new Size(1453, 2664));
                            xboard.badTree.Add(new RRectangle(724, 594, 724 + 1453, 594 + 2664, 0, 0), dRectangle);
                            CvInvoke.Rectangle(backBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        else
                        {
                            dRectangle = new DRectangle(new DPoint(1096, 592), new Size(1381, 2661));
                            xboard.badTree.Add(new RRectangle(1096, 592, 1096 + 1381, 592 + 2661, 0, 0), dRectangle);
                            CvInvoke.Rectangle(frontBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        break;
                    case 1:
                        if (xboard.isBack)
                        {
                            dRectangle = new DRectangle(new DPoint(2389, 585), new Size(1435, 2706));
                            xboard.badTree.Add(new RRectangle(2389, 585, 2389 + 1435, 585 + 2706, 0, 0), dRectangle);
                            CvInvoke.Rectangle(backBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        else
                        {
                            dRectangle = new DRectangle(new DPoint(2734, 607), new Size(1354, 2691));
                            xboard.badTree.Add(new RRectangle(2734, 607, 2734 + 1354, 607 + 2691, 0, 0), dRectangle);
                            CvInvoke.Rectangle(frontBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        break;
                    case 2:
                        if (xboard.isBack)
                        {
                            dRectangle = new DRectangle(new DPoint(4084, 600), new Size(1402, 2691));
                            xboard.badTree.Add(new RRectangle(4084, 600, 4084 + 1402, 600 + 2691, 0, 0), dRectangle);
                            CvInvoke.Rectangle(backBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        else
                        {
                            dRectangle = new DRectangle(new DPoint(4387, 643), new Size(1369, 2682));
                            xboard.badTree.Add(new RRectangle(4387, 643, 4387 + 1369, 643 + 2682, 0, 0), dRectangle);
                            CvInvoke.Rectangle(frontBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        break;
                    case 3:
                        if (xboard.isBack)
                        {
                            dRectangle = new DRectangle(new DPoint(5737, 600), new Size(1399, 2654));
                            xboard.badTree.Add(new RRectangle(5737, 600, 5737 + 1399, 600 + 2654, 0, 0), dRectangle);
                            CvInvoke.Rectangle(backBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        else
                        {
                            dRectangle = new DRectangle(new DPoint(6022, 703), new Size(1333, 2651));
                            xboard.badTree.Add(new RRectangle(6022, 703, 6022 + 1333, 703 + 2651, 0, 0), dRectangle);
                            CvInvoke.Rectangle(frontBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        break;
                    case 4:
                        if (xboard.isBack)
                        {
                            dRectangle = new DRectangle(new DPoint(7364, 568), new Size(1411, 2660));
                            xboard.badTree.Add(new RRectangle(7364, 568, 7364 + 1411, 568 + 2660, 0, 0), dRectangle);
                            CvInvoke.Rectangle(backBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        else
                        {
                            dRectangle = new DRectangle(new DPoint(7668, 718), new Size(1363, 2624));
                            xboard.badTree.Add(new RRectangle(7668, 718, 7668 + 1363, 718 + 2624, 0, 0), dRectangle);
                            CvInvoke.Rectangle(frontBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        break;
                    case 5:
                        if (xboard.isBack)
                        {
                            dRectangle = new DRectangle(new DPoint(9042, 598), new Size(1359, 2618));
                            xboard.badTree.Add(new RRectangle(9042, 598, 9042 + 1359, 598 + 2618, 0, 0), dRectangle);
                            CvInvoke.Rectangle(backBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        else
                        {
                            dRectangle = new DRectangle(new DPoint(9324, 706), new Size(1363, 2666));
                            xboard.badTree.Add(new RRectangle(9324, 706, 9324 + 1363, 706 + 2666, 0, 0), dRectangle);
                            CvInvoke.Rectangle(frontBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        break;
                    case 6:
                        if (xboard.isBack)
                        {
                            dRectangle = new DRectangle(new DPoint(10681, 576), new Size(1414, 2675));
                            xboard.badTree.Add(new RRectangle(10681, 576, 10681 + 1414, 576 + 2675, 0, 0), dRectangle);
                            CvInvoke.Rectangle(backBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        else
                        {
                            dRectangle = new DRectangle(new DPoint(10995, 775), new Size(1366, 2633));
                            xboard.badTree.Add(new RRectangle(10995, 775, 10995 + 1366, 775 + 2633, 0, 0), dRectangle);
                            CvInvoke.Rectangle(frontBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        break;
                    case 7:
                        if (xboard.isBack)
                        {
                            dRectangle = new DRectangle(new DPoint(12358, 570), new Size(1348, 2660));
                            xboard.badTree.Add(new RRectangle(12358, 570, 12358 + 1348, 570 + 2660, 0, 0), dRectangle);
                            CvInvoke.Rectangle(backBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(backBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        else
                        {
                            dRectangle = new DRectangle(new DPoint(12630, 748), new Size(1372, 2678));
                            xboard.badTree.Add(new RRectangle(12630, 748, 12630 + 1372, 748 + 2678, 0, 0), dRectangle);
                            CvInvoke.Rectangle(frontBoard.matImg, dRectangle, new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, dRectangle.Location, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                            CvInvoke.Line(frontBoard.matImg, new DPoint(dRectangle.X + dRectangle.Width, dRectangle.Y), new DPoint(dRectangle.X, dRectangle.Y + dRectangle.Height), new MCvScalar(0, 0, 255), 20);
                        }
                        break;
                }
            }
    
            xboardDoneNum++;
            //到这里结束了！！！！大于等于16，说明正反面都执行完了
            //所以要执行loadData
            if(frontBoard!=null && backBoard != null)
            {
                if (xboardDoneNum >= 16)
                {
                    loadData(nowWorkingPcb);
                }
            }
            else if (frontBoard != null || backBoard != null)
            {
                if (xboardDoneNum >= 8)
                {
                    loadData(nowWorkingPcb);
                }
            }

        }

        /// <summary>
        /// 执行打X板子
        /// </summary>
        /// <param name="frontImg"></param>
        /// <param name="backImg"></param>
        /// <param name="pcb"></param>
        public void xxxx(Pcb pcb)
        {
            checkedNum = 0;
            needCheckNumAll = 0;
            xboardDoneNum = 0;
            nowWorkingPcb = pcb;
            frontBoard = null;
            backBoard = null;

            string frontImgPath = ConfigurationManager.AppSettings["FtpPath"] + pcb.Id + "/front.jpg";
            string backImgPath = ConfigurationManager.AppSettings["FtpPath"] + pcb.Id + "/back.jpg";

            if (File.Exists(frontImgPath))
            {
                frontBoard = new XBoard(frontImgPath, false);
            }
            if (File.Exists(backImgPath))
            {
                backBoard = new XBoard(backImgPath, false);
                backBoard.isBack = true;
            }

            for (int i = 0; i <= 7; i++)
            {
                if (frontBoard != null)
                {
                    MySmartThreadPool.Instance().QueueWorkItem((act, n) =>
                    {
                        lock (frontBoard)
                        {
                            try
                            {
                                DPoint point = new DPoint();
                                double dres = Aoi.marker_match_crop(frontBoard.matImg.Ptr, marker.Ptr, ref point, ref act);
                                this.BeginInvoke((Action<int, double, XBoard>)((bn, bdress, board) =>
                                {
                                    xBoardAddTree(bn, bdress, board);
                                }), n, dres, frontBoard);
                                //if (dres > threshold)
                                //{


                                //    //CvInvoke.PutText(frontMatImg, dres + "", new DPoint(point.X + act.X, point.Y + act.Y), FontFace.HersheyComplex, 3, new MCvScalar(0, 0, 255));
                                //}
                                //CvInvoke.PutText(frontMatImg, dres + "", new DPoint(point.X + act.X, point.Y + act.Y), FontFace.HersheyComplex, 3, new MCvScalar(0, 0, 255));
                                //CvInvoke.Rectangle(frontMatImg, new DRectangle(new DPoint(point.X + act.X, point.Y + act.Y), new Size(75, 75)), new MCvScalar(0, 0, 255), 20);
                                //frontMatImg.Save(@"C:\res\f" + point.X + ".jpg");
                                //Console.WriteLine(dres);
                                //Console.WriteLine("正面:" + dres);
                            }
                            catch (Exception er)
                            {
                                LogHelper.WriteLog("front marker error", er);
                            }
                        }
                    }, frontMarkerCheckArea[i], i);
                }
                if (backBoard != null)
                {
                    MySmartThreadPool.Instance().QueueWorkItem((act, n) =>
                    {
                        lock (backBoard)
                        {
                            try
                            {

                                DPoint point = new DPoint();
                                double dres = Aoi.marker_match_crop(backBoard.matImg.Ptr, marker.Ptr, ref point, ref act);
                                this.BeginInvoke((Action<int, double, XBoard>)((bn, bdress, board) =>
                                {
                                    xBoardAddTree(bn, bdress, board);
                                }), n, dres, backBoard);
                                //if (dres > threshold)
                                //{

                                //}
                                //CvInvoke.PutText(backMatImg, dres + "", new DPoint(point.X + act.X, point.Y + act.Y), FontFace.HersheyComplex, 3, new MCvScalar(0, 0, 255));
                                //CvInvoke.Rectangle(backMatImg, new DRectangle(new DPoint(point.X + act.X, point.Y + act.Y), new Size(75, 75)), new MCvScalar(255, 0, 0), 3);
                                //backMatImg.Save(@"C:\res\b" + point.X + ".jpg");
                                //Console.WriteLine(dres);
                                //Console.WriteLine("反面:" + dres);
                            }
                            catch (Exception er)
                            {
                                LogHelper.WriteLog("back marker error", er);
                            }
                        }
                    }, backMarkerCheckArea[i], i);
                }


                //DPoint point = new DPoint();
                //double dres = Aoi.marker_match(img.Ptr, marker.Ptr, ref point);
                //CvInvoke.Rectangle(img, new DRectangle(point, new Size(200, 200)), new MCvScalar(0, 0, 255), 20);
                //img.Save(@"E:\索米测试图片\result\" + i + ".jpg");
                //Console.WriteLine(res);
            }
        }


        public void listSwitch(bool isback)
        {
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

            if (frontBoard != null)
            { bitmapFront = frontBoard.matImg.Bitmap;
                twoSidesPcb.showFrontImg(bitmapFront);
            }
            if (backBoard != null)
            {
                bitmapBack = backBoard.matImg.Bitmap;
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

            lbPcbNumber.Text = pcb.PcbNumber;
            lbSurfaceNumber.Text = pcb.SurfaceNumber.ToString();
            lbPcbWidth.Text = pcb.PcbWidth.ToString();
            lbPcbHeight.Text = pcb.PcbHeight.ToString();
            lbPcbChildenNumber.Text = pcb.PcbChildenNumber.ToString();
            lbResult.Text = "NG";
            lbResult.ForeColor = Color.Red;

            //这里判断下！！！！是否在X板子里，如果是的话就不加载
            foreach (var item in pcb.results)
            {
                #region 判断缺陷点是否在badmarker所对应的坐标内
                List<DRectangle> badlist = new List<DRectangle>();
                try
                {
                    string[] reg = item.Region.Split(',');
                    RTree.Point point = new RTree.Point(int.Parse(reg[0]), int.Parse(reg[1]), 0);
              
                    if (item.IsBack == 0)
                    {
                        badlist = frontBoard.badTree.Nearest(point, 0);
                    }
                    else
                    {
                        badlist = backBoard.badTree.Nearest(point, 0);
                    }
                }
                catch (Exception er)
                {
                    LogHelper.WriteLog("marker error", er);
                }
                #endregion

                if (badlist.Count == 0)
                {
                    needCheckNumAll ++;
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
                    if (item.IsBack == 0)
                    {
                        lvListFront.Items.Add(li);
                    }
                    else if (item.IsBack == 1)
                    {
                        lvListBack.Items.Add(li);
                    }
                }
            }
            if (needCheckNumAll <= 0)
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
                    DRectangle rect = new DRectangle(
                            int.Parse(reg[0]),
                            int.Parse(reg[1]),
                            int.Parse(reg[2]),
                            int.Parse(reg[3]));
                    DRectangle oldRect = rect;
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

                    DRectangle newRect = new DRectangle(oldRect.X - rect.X - oldRect.Width / 2, oldRect.Y - rect.Y - oldRect.Height / 2, oldRect.Width, oldRect.Height);
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
