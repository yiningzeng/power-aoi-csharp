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

        List<Result> frontResults = new List<Result>();
        List<Result> backResults = new List<Result>();

        List<DRectangle> frontMarkerCheckArea = new List<DRectangle>();
        List<DRectangle> backMarkerCheckArea = new List<DRectangle>();
        Mat marker = new Mat(Application.StartupPath + "/marker.jpg", Emgu.CV.CvEnum.LoadImageType.AnyColor);
        Mat badMarker = new Mat(Application.StartupPath + "/bad_marker.jpg", Emgu.CV.CvEnum.LoadImageType.AnyColor);
        double threshold = Convert.ToDouble(ConfigurationManager.AppSettings["MarkerThreshold"]);//0.7;

        public PcbDetails(Main m, PartOfPcb pPcb, TwoSidesPcb tPcb)
        {
            InitializeComponent();

            #region marker点检测区域老版本
            //backMarkerCheckArea.Add(new DRectangle(1031, 3298, 863, 475));
            //backMarkerCheckArea.Add(new DRectangle(2743, 3257, 781, 493));
            //backMarkerCheckArea.Add(new DRectangle(4417, 3245, 724, 502));
            //backMarkerCheckArea.Add(new DRectangle(6022, 3221, 811, 535));
            //backMarkerCheckArea.Add(new DRectangle(7649, 3224, 847, 511));
            //backMarkerCheckArea.Add(new DRectangle(9342, 3242, 823, 467));
            //backMarkerCheckArea.Add(new DRectangle(10981, 3217, 796, 511));
            //backMarkerCheckArea.Add(new DRectangle(12658, 3247, 769, 496));

            //frontMarkerCheckArea.Add(new DRectangle(1254, 3277, 983, 473));
            //frontMarkerCheckArea.Add(new DRectangle(2970, 3251, 887, 511));
            //frontMarkerCheckArea.Add(new DRectangle(4639, 3295, 861, 477));
            //frontMarkerCheckArea.Add(new DRectangle(6287, 3307, 858, 507));
            //frontMarkerCheckArea.Add(new DRectangle(7867, 3319, 995, 517));
            //frontMarkerCheckArea.Add(new DRectangle(9573, 3355, 923, 517));
            //frontMarkerCheckArea.Add(new DRectangle(11215, 3375, 881, 523));
            //frontMarkerCheckArea.Add(new DRectangle(12907, 3403, 869, 521));
            #endregion

            #region marker点检测区域
            backMarkerCheckArea.Add(new DRectangle(766, 151, 401, 187));
            backMarkerCheckArea.Add(new DRectangle(1558, 135, 473, 217));
            backMarkerCheckArea.Add(new DRectangle(2396, 129, 433, 217));
            backMarkerCheckArea.Add(new DRectangle(3200, 121, 449, 211));
            backMarkerCheckArea.Add(new DRectangle(4042, 117, 441, 209));
            backMarkerCheckArea.Add(new DRectangle(4878, 117, 419, 201));
            backMarkerCheckArea.Add(new DRectangle(5680, 113, 449, 201));
            backMarkerCheckArea.Add(new DRectangle(6502, 131, 451, 193));


            frontMarkerCheckArea.Add(new DRectangle(466, 34, 454, 223));
            frontMarkerCheckArea.Add(new DRectangle(1327, 37, 454, 229));
            frontMarkerCheckArea.Add(new DRectangle(2169, 45, 367, 253));
            frontMarkerCheckArea.Add(new DRectangle(2946, 68, 445, 237));
            frontMarkerCheckArea.Add(new DRectangle(3763, 67, 449, 243));
            frontMarkerCheckArea.Add(new DRectangle(4636, 87, 372, 241));
            frontMarkerCheckArea.Add(new DRectangle(5409, 90, 475, 249));
            frontMarkerCheckArea.Add(new DRectangle(6259, 111, 428, 237));
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
        /// 更新图片上的板子区域和区域树的添加
        /// </summary>
        /// <param name="xboard"></param>
        /// <param name="dBackRectangle">背面板子的区域</param>
        /// <param name="dFrontRectangle">正面板子的区域</param>
        private void drawAndTree(XBoard xboard, double markerScore, DRectangle dBackRectangle, DRectangle dFrontRectangle)
        {
            if (xboard.isBack)
            {
                MCvScalar mCvScalar;
                //只有匹配结果分值较高时才新增
                if (markerScore >= threshold) // 得分大于阈值就是坏板
                {
                    mCvScalar = new MCvScalar(0, 0, 255); // 使用红色 bgr
                    //并且需要画X
                    CvInvoke.Line(backBoard.matImg, dBackRectangle.Location, new DPoint(dBackRectangle.X + dBackRectangle.Width, dBackRectangle.Y + dBackRectangle.Height), mCvScalar, 20);
                    CvInvoke.Line(backBoard.matImg, new DPoint(dBackRectangle.X + dBackRectangle.Width, dBackRectangle.Y), new DPoint(dBackRectangle.X, dBackRectangle.Y + dBackRectangle.Height), mCvScalar, 20);
                    xboard.badTree.Add(new RRectangle(dBackRectangle.X, dBackRectangle.Y, dBackRectangle.X + dBackRectangle.Width, dBackRectangle.Y + dBackRectangle.Height, 0, 0), dBackRectangle);
                }
                else
                {
                    mCvScalar = new MCvScalar(0, 255, 0); // 使用绿色 bgr
                    xboard.okTree.Add(new RRectangle(dBackRectangle.X, dBackRectangle.Y, dBackRectangle.X + dBackRectangle.Width, dBackRectangle.Y + dBackRectangle.Height, 0, 0), dBackRectangle);
                }
                CvInvoke.Rectangle(backBoard.matImg, dBackRectangle, mCvScalar, 20);
            }
            else
            {
                MCvScalar mCvScalar;
                //只有匹配结果分值较高时才新增
                if (markerScore >= threshold) // 得分大于阈值就是坏板
                {
                    mCvScalar = new MCvScalar(0, 0, 255); // 使用红色 bgr
                    CvInvoke.Line(frontBoard.matImg, dFrontRectangle.Location, new DPoint(dFrontRectangle.X + dFrontRectangle.Width, dFrontRectangle.Y + dFrontRectangle.Height), mCvScalar, 20);
                    CvInvoke.Line(frontBoard.matImg, new DPoint(dFrontRectangle.X + dFrontRectangle.Width, dFrontRectangle.Y), new DPoint(dFrontRectangle.X, dFrontRectangle.Y + dFrontRectangle.Height), mCvScalar, 20);
                    xboard.badTree.Add(new RRectangle(dFrontRectangle.X, dFrontRectangle.Y, dFrontRectangle.X + dFrontRectangle.Width, dFrontRectangle.Y + dFrontRectangle.Height, 0, 0), dFrontRectangle);
                }
                else
                {
                    mCvScalar = new MCvScalar(0, 255, 0); // 使用绿色 bgr
                    xboard.okTree.Add(new RRectangle(dFrontRectangle.X, dFrontRectangle.Y, dFrontRectangle.X + dFrontRectangle.Width, dFrontRectangle.Y + dFrontRectangle.Height, 0, 0), dFrontRectangle);
                }
                CvInvoke.Rectangle(frontBoard.matImg, dFrontRectangle, mCvScalar, 20);
            }
        }

        /// <summary>
        /// 增加坏板的区域， 里面存储的都是坏板的直接区域
        /// </summary>
        /// <param name="i"></param>
        /// <param name="xboard"></param>
        private void xBoardAddTree(int i, double dres, XBoard xboard)
        {

            if (xboard.isBack)
            {
                Console.WriteLine("反面:" + i + "  " + dres);
            }
            else
            {
                Console.WriteLine("正面:" + i + "  " + dres);
            }
            switch (i)
            {
                case 0:
                    drawAndTree(xboard, dres,
                        new DRectangle(new DPoint(597, 300), new Size(717, 1365)),
                        new DRectangle(new DPoint(376, 271), new Size(679, 1361)));
                    break;
                case 1:
                    drawAndTree(xboard, dres,
                        new DRectangle(new DPoint(1442, 313), new Size(685, 1350)),
                        new DRectangle(new DPoint(1192, 277), new Size(702, 1367)));
                    break;
                case 2:
                    drawAndTree(xboard, dres,
                        new DRectangle(new DPoint(2265, 306), new Size(688, 1359)),
                        new DRectangle(new DPoint(2013, 271), new Size(700, 1385)));
                    break;
                case 3:
                    drawAndTree(xboard, dres,
                        new DRectangle(new DPoint(3083, 316), new Size(713, 1362)),
                        new DRectangle(new DPoint(2856, 295), new Size(679, 1355)));
                    break;
                case 4:
                    drawAndTree(xboard, dres,
                        new DRectangle(new DPoint(3909, 322), new Size(701, 1357)),
                        new DRectangle(new DPoint(3672, 295), new Size(679, 1334)));
                    break;
                case 5:
                    drawAndTree(xboard, dres,
                        new DRectangle(new DPoint(4740, 327), new Size(698, 1368)),
                        new DRectangle(new DPoint(4506, 295), new Size(673, 1346)));
                    break;
                case 6:
                    drawAndTree(xboard, dres,
                        new DRectangle(new DPoint(5563, 333), new Size(694, 1353)),
                        new DRectangle(new DPoint(5322, 295), new Size(681, 1367)));
                    break;
                case 7:
                    drawAndTree(xboard, dres,
                        new DRectangle(new DPoint(6385, 339), new Size(669, 1358)),
                        new DRectangle(new DPoint(6143, 316), new Size(697, 1349)));
                    break;
            }

            #region 老的板区域
            //switch (i)
            //{
            //    case 0:
            //        drawAndTree(xboard, dres,
            //            new DRectangle(new DPoint(724, 594), new Size(1453, 2664)),
            //            new DRectangle(new DPoint(1096, 592), new Size(1381, 2661)));
            //        break;
            //    case 1:
            //        drawAndTree(xboard, dres,
            //            new DRectangle(new DPoint(2389, 585), new Size(1435, 2706)),
            //            new DRectangle(new DPoint(2734, 607), new Size(1354, 2691)));
            //        break;
            //    case 2:
            //        drawAndTree(xboard, dres,
            //            new DRectangle(new DPoint(4084, 600), new Size(1402, 2691)),
            //            new DRectangle(new DPoint(4387, 643), new Size(1369, 2682)));
            //        break;
            //    case 3:
            //        drawAndTree(xboard, dres,
            //            new DRectangle(new DPoint(5737, 600), new Size(1399, 2654)),
            //            new DRectangle(new DPoint(6022, 703), new Size(1333, 2651)));
            //        break;
            //    case 4:
            //        drawAndTree(xboard, dres,
            //            new DRectangle(new DPoint(7364, 568), new Size(1411, 2660)),
            //            new DRectangle(new DPoint(7668, 718), new Size(1363, 2624)));
            //        break;
            //    case 5:
            //        drawAndTree(xboard, dres,
            //            new DRectangle(new DPoint(9042, 598), new Size(1359, 2618)),
            //            new DRectangle(new DPoint(9324, 706), new Size(1363, 2666)));
            //        break;
            //    case 6:
            //        drawAndTree(xboard, dres,
            //            new DRectangle(new DPoint(10681, 576), new Size(1414, 2675)),
            //            new DRectangle(new DPoint(10995, 775), new Size(1366, 2633)));
            //        break;
            //    case 7:
            //        drawAndTree(xboard, dres,
            //            new DRectangle(new DPoint(12358, 570), new Size(1348, 2660)),
            //            new DRectangle(new DPoint(12630, 748), new Size(1372, 2678)));
            //        break;
            //}
            #endregion
            xboardDoneNum++;
            //到这里结束了！！！！大于等于16，说明正反面都执行完了
            //所以要执行loadData
            if (frontBoard != null && backBoard != null)
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
        /// 缺陷得分比较
        /// </summary>
        /// <param name="ngType"></param>
        /// <param name="score"></param>
        /// <returns>bool true那么需要显示，false不显示</returns>
        private bool aacompare(string ngType, float score)
        {
            try
            {
                if (float.Parse(INIHelper.Read("AIConfig", ngType, Application.StartupPath + "/config.ini")) <= score) return true; // 如果分值小于阈值直接返回
                return false;
            }
            catch (Exception err)
            {
                LogHelper.WriteLog("config.ini > AIConfig err", err);
                return true;
            }
        }

        /// <summary>
        /// Listview加载显示
        /// </summary>
        /// <param name="pchPath"></param>
        public void showData(string pchPath)
        {
            frontResults.Sort((x, y) => Convert.ToInt32(double.Parse(x.Region.Split(',')[0])).CompareTo(Convert.ToInt32(double.Parse(y.Region.Split(',')[0]))));
            backResults.Sort((x, y) => Convert.ToInt32(double.Parse(x.Region.Split(',')[0])).CompareTo(Convert.ToInt32(double.Parse(y.Region.Split(',')[0]))));

            foreach (var item in frontResults)
            {
                needCheckNumAll++;
                ListViewItem li = new ListViewItem();
                li.BackColor = Color.Red;
                li.SubItems[0].Text = item.PcbId.ToString();
                li.SubItems.Add(item.IsBack.ToString());
                li.SubItems.Add(pchPath);
                li.SubItems.Add(item.PartImagePath);
                li.SubItems.Add(item.Region);
                li.SubItems.Add(item.Id.ToString());
                li.SubItems.Add(item.Area);
                li.SubItems.Add(item.NgType);
                li.SubItems.Add(item.score.ToString());
                li.SubItems.Add("未判定");
                lvListFront.Items.Add(li);
            }

            foreach (var item in backResults)
            {
                needCheckNumAll++;
                ListViewItem li = new ListViewItem();
                li.BackColor = Color.Red;
                li.SubItems[0].Text = item.PcbId.ToString();
                li.SubItems.Add(item.IsBack.ToString());
                li.SubItems.Add(pchPath);
                li.SubItems.Add(item.PartImagePath);
                li.SubItems.Add(item.Region);
                li.SubItems.Add(item.Id.ToString());
                li.SubItems.Add(item.Area);
                li.SubItems.Add(item.NgType);
                li.SubItems.Add(item.score.ToString());
                li.SubItems.Add("未判定");
                lvListBack.Items.Add(li);
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
            frontResults.Clear();
            backResults.Clear();

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
                lbPcbWidth.Text = pcb.PcbLength.ToString();
                lbPcbLength.Text = pcb.PcbWidth.ToString();
                lbPcbChildenNumber.Text = pcb.PcbChildenNumber.ToString();
                lbResult.Text = "OK";
                lbResult.ForeColor = Color.Green;

                partOfPcb.showImg(null);
                return;
            }

            lbPcbNumber.Text = pcb.PcbNumber;
            lbSurfaceNumber.Text = pcb.SurfaceNumber.ToString();
            lbPcbWidth.Text = pcb.PcbLength.ToString();
            lbPcbLength.Text = pcb.PcbWidth.ToString();
            lbPcbChildenNumber.Text = pcb.PcbChildenNumber.ToString();
            lbResult.Text = "NG";
            lbResult.ForeColor = Color.Red;
            try
            {
                //pcb.results.Sort((x, y) => Convert.ToInt32(double.Parse(x.Region.Split(',')[0])).CompareTo(Convert.ToInt32(double.Parse(y.Region.Split(',')[0]))));

                //pcb.results.OrderBy(s => s.NgType).ThenBy(s => double.Parse(s.Region.Split(',')[1]));//.ThenBy(s => s.NgType);
                pcb.results = pcb.results.OrderBy(s => s.NgType).ThenBy(s => double.Parse(s.Region.Split(',')[0])).ThenBy(s => double.Parse(s.Region.Split(',')[1])).ToList();
            }
            catch(Exception er)
            {
                LogHelper.WriteLog("排序", er);
            }
            if (Convert.ToBoolean(int.Parse(ConfigurationManager.AppSettings["XXX"])))
            {
                DRectangle beforeRect = new DRectangle(0, 0, 0, 0);
                string oldNgType = "";
                foreach (var item in pcb.results)
                {

                    DRectangle nowRect = new DRectangle(0, 0, 0, 0);
                    //这里判断下！！！！是否在X板子里，如果是的话就不加载
                    #region 判断缺陷点是否在badmarker所对应的坐标内
                    List<DRectangle> badlist = new List<DRectangle>();
                    List<DRectangle> oklist = new List<DRectangle>();
                    try
                    {
                        string[] reg = item.Region.Split(',');
                        int x = Convert.ToInt32(double.Parse(reg[0]));
                        int y = Convert.ToInt32(double.Parse(reg[1]));
                        int w = Convert.ToInt32(double.Parse(reg[2]));
                        int h = Convert.ToInt32(double.Parse(reg[3]));
                        nowRect = new DRectangle(x, y, w, h);
                        RTree.Point point = new RTree.Point(x,y, 0);
                        if (item.IsBack == 0)
                        {
                            badlist = frontBoard.badTree.Nearest(point, 0); //检索坏板
                            oklist = frontBoard.okTree.Nearest(point, 0); //检索好板
                        }
                        else
                        {
                            badlist = backBoard.badTree.Nearest(point, 0); //检索坏板
                            oklist = backBoard.okTree.Nearest(point, 0); //检索好板
                        }
                    }
                    catch (Exception er)
                    {
                        LogHelper.WriteLog("marker error", er);
                    }
                    #endregion

                    if (badlist.Count == 0 && oklist.Count > 0)
                    {
                        //这里只有通过比较分值大的才显示在列表
                        if (aacompare(item.NgType, item.score))
                        {
                            double distance = 0;
                            try
                            {
                                int xdiff = nowRect.X - beforeRect.X;
                                int ydiff = nowRect.Y - beforeRect.Y;
                                distance = Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
                            }
                            catch(Exception er)
                            {
                                LogHelper.WriteLog("合并计算失败", er);
                            }

                            if (distance!=0 &&distance <= int.Parse(ConfigurationManager.AppSettings["MergeRadius"]) && oldNgType.Equals(item.NgType))//float.Parse(INIHelper.Read("AIConfig", ngType, Application.StartupPath + "/config.ini"))
                            {
                                int finalLeftTopX = nowRect.X, finalLeftTopY = nowRect.Y, finalRightBotomX = nowRect.X + nowRect.Width, finalRightBotomY = nowRect.Y + nowRect.Height;
                                if (nowRect.X > beforeRect.X)
                                {
                                    finalLeftTopX = beforeRect.X;
                                }
                                if (nowRect.Y > beforeRect.Y)
                                {
                                    finalLeftTopY = beforeRect.Y;
                                }

                                if (finalRightBotomX < beforeRect.X + beforeRect.Width)
                                {
                                    finalRightBotomX = beforeRect.X + beforeRect.Width;
                                }
                                if (finalRightBotomY < beforeRect.Y + beforeRect.Height)
                                {
                                    finalRightBotomY = beforeRect.Y + beforeRect.Height;
                                }

                                nowRect = new DRectangle(finalLeftTopX, finalLeftTopY, finalRightBotomX - finalLeftTopX, finalRightBotomY - finalLeftTopY);
                                Result result = item;
                                result.Region = nowRect.X + "," + nowRect.Y + "," + nowRect.Width + "," + nowRect.Height;
                                if (item.IsBack == 0)
                                {
                                    if (frontResults.Count > 0) frontResults.RemoveAt(frontResults.Count - 1);
                                    frontResults.Add(result);
                                }
                                else if (item.IsBack == 1)
                                {
                                    if (backResults.Count > 0) backResults.RemoveAt(backResults.Count - 1);
                                    backResults.Add(result);
                                }
                                //ListViewItem li = new ListViewItem();
                                //li.BackColor = Color.Red;
                                //li.SubItems[0].Text = item.PcbId.ToString();
                                //li.SubItems.Add(item.IsBack.ToString());
                                //li.SubItems.Add(pcb.PcbPath);
                                //li.SubItems.Add(item.PartImagePath);
                                //li.SubItems.Add(nowRect.X + "," + nowRect.Y + "," + nowRect.Width + "," + nowRect.Height);
                                //li.SubItems.Add(item.Id.ToString());
                                //li.SubItems.Add(item.Area);
                                //li.SubItems.Add(item.NgType);
                                //li.SubItems.Add(item.score.ToString());
                                //li.SubItems.Add("未判定");
                                //if (item.IsBack == 0)
                                //{
                                //    lvListFront.Items.Add(li);
                                //}
                                //else if (item.IsBack == 1)
                                //{
                                //    lvListBack.Items.Add(li);
                                //}
                            }
                            else
                            {
                                if (item.IsBack == 0)
                                {
                                    frontResults.Add(item);
                                }
                                else if (item.IsBack == 1)
                                {
                                    backResults.Add(item);
                                }
                            }
                        }
                        beforeRect = nowRect;
                        oldNgType = item.NgType;
                    }
                }
            }
            else // 不进行打X板操作
            {
                DRectangle beforeRect = new DRectangle(0, 0, 0, 0);
                string oldNgType = "";
                foreach (var item in pcb.results)
                {
                    try
                    {
                        string[] reg = item.Region.Split(',');
                        DRectangle nowRect = new DRectangle(0, 0, 0, 0);
                        int x = Convert.ToInt32(double.Parse(reg[0]));
                        int y = Convert.ToInt32(double.Parse(reg[1]));
                        int w = Convert.ToInt32(double.Parse(reg[2]));
                        int h = Convert.ToInt32(double.Parse(reg[3]));
                        nowRect = new DRectangle(x, y, w, h);
                        //这里只有通过比较分值大的才显示在列表
                        if (aacompare(item.NgType, item.score))
                        {
                            double distance = 0;
                            try
                            {
                                int xdiff = nowRect.X - beforeRect.X;
                                int ydiff = nowRect.Y - beforeRect.Y;
                                distance = Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
                            }
                            catch (Exception er)
                            {
                                LogHelper.WriteLog("合并计算失败", er);
                            }

                            if (distance != 0 && distance <= int.Parse(ConfigurationManager.AppSettings["MergeRadius"]) && oldNgType.Equals(item.NgType))//float.Parse(INIHelper.Read("AIConfig", ngType, Application.StartupPath + "/config.ini"))
                            {
                                int finalLeftTopX = nowRect.X, finalLeftTopY = nowRect.Y, finalRightBotomX = nowRect.X + nowRect.Width, finalRightBotomY = nowRect.Y + nowRect.Height;
                                if (nowRect.X > beforeRect.X)
                                {
                                    finalLeftTopX = beforeRect.X;
                                }
                                if (nowRect.Y > beforeRect.Y)
                                {
                                    finalLeftTopY = beforeRect.Y;
                                }

                                if(finalRightBotomX < beforeRect.X + beforeRect.Width)
                                {
                                    finalRightBotomX = beforeRect.X + beforeRect.Width;
                                }
                                if(finalRightBotomY < beforeRect.Y + beforeRect.Height)
                                {
                                    finalRightBotomY = beforeRect.Y + beforeRect.Height;
                                }
                             
                                nowRect = new DRectangle(finalLeftTopX, finalLeftTopY, finalRightBotomX - finalLeftTopX, finalRightBotomY - finalLeftTopY);
                                Result result = item;
                                result.Region = nowRect.X + "," + nowRect.Y + "," + nowRect.Width + "," + nowRect.Height;
                                if (item.IsBack == 0)
                                {
                                    if (frontResults.Count > 0) frontResults.RemoveAt(frontResults.Count - 1);
                                    frontResults.Add(result);
                                }
                                else if (item.IsBack == 1)
                                {
                                    if (backResults.Count > 0) backResults.RemoveAt(backResults.Count - 1);
                                    backResults.Add(result);
                                }
                            }
                            else
                            {
                                if (item.IsBack == 0)
                                {
                                    frontResults.Add(item);
                                }
                                else if (item.IsBack == 1)
                                {
                                    backResults.Add(item);
                                }
                            }
                        }
                        beforeRect = nowRect;
                        oldNgType = item.NgType;
                    }
                    catch(Exception er)
                    {
                        int a = 0;
                    }


                }
            }

            showData(pcb.PcbPath);
            if (needCheckNumAll <= 0)
            {
                main.doLeisure(false);
                lbPcbNumber.Text = pcb.PcbNumber;
                lbSurfaceNumber.Text = pcb.SurfaceNumber.ToString();
                lbPcbWidth.Text = pcb.PcbLength.ToString();
                lbPcbLength.Text = pcb.PcbWidth.ToString();
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
                if (li.SubItems[9].Text == "未判定")
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
                string tFilePath = ConfigurationManager.AppSettings["FtpPath"] + selectListView.Items[index].SubItems[2].Text + selectListView.Items[index].SubItems[3].Text;
                int Zoom = int.Parse(ConfigurationManager.AppSettings["Zoom"]);
                //if (!File.Exists(tFilePath))
                {
                    Bitmap resBitmap = null;
                    string[] reg = selectListView.Items[index].SubItems[4].Text.Split(',');
                    DRectangle rect = new DRectangle(
                            Convert.ToInt32(double.Parse(reg[0])),
                            Convert.ToInt32(double.Parse(reg[1])),
                            Convert.ToInt32(double.Parse(reg[2])),
                            Convert.ToInt32(double.Parse(reg[3])));
                    DRectangle oldRect = rect;
                    if (selectListView.Items[index].SubItems[1].Text == "0") // 正面
                    {
                        //Bitmap drawBitmap = Utils.DrawRect(bitmapFront, rect, lvList.Items[index].SubItems[7].Text);
                        rect.Inflate(Zoom, Zoom);
                        resBitmap = Utils.BitmapCut(bitmapFront, rect);//.Save(tFilePath);
                        twoSidesPcb.BeginInvoke((Action)(() =>
                        {
                            twoSidesPcb.pictureBoxDraw(true, rect);
                        }));
                    }
                    else if (selectListView.Items[index].SubItems[1].Text == "1") // 背面
                    {
                        //Bitmap drawBitmap = Utils.DrawRect(bitmapBack, rect, lvList.Items[index].SubItems[7].Text);
                        rect.Inflate(Zoom, Zoom);
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
                    if (selectListView.Items[index].SubItems[9].Text == "未判定") checkedNum++; // 只有在未判定更改状态后才+1

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
                    if (selectListView.Items[index].SubItems[9].Text == "未判定") checkedNum++; // 只有在未判定更改状态后才+1

                    selectListView.Items[index].BackColor = Color.Yellow;
                }
                else
                {
                    return;
                }
                selectListView.Items[index].SubItems[9].Text = res;

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

        public void changePause(string str, bool enable)
        {
            btnPause.Text = str + " (End)";
            btnPause.Enabled = enable;
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
        private void btnPause_Click(object sender, EventArgs e)
        {
            main.changeStatus();
        }
        #endregion
    }
}
