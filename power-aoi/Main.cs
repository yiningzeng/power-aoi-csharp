using Amib.Threading;
using FubarDev.FtpServer;
using FubarDev.FtpServer.FileSystem.DotNet;
using ImageProcessor;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using power_aoi.DockerPanal;
using power_aoi.Model;
using power_aoi.Tools;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace power_aoi
{
    public partial class Main : DockContent
    {
        public delegate void RabbitmqMessageCallback(IModel channel, string message);
        public RabbitmqMessageCallback doWorkD;

        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private TwoSidesPcb twoSidesPcb;
        private PartOfPcb partOfPcb;
        private PcbDetails pcbDetails;

        public bool isLeisure = true;
        public IModel mainChannel;


        // 队列处理回调！！所有的界面操作方法写在这个函数里
        public void doWork(IModel channel, string message)
        {
            mainChannel = channel;
            //处理完成，手动确认
            //channel.BasicAck(Rabbitmq.deliveryTag, false);
            //Thread.Sleep(1000);
            if (isLeisure)
            {
                // 反序列化json
                JsonData<Pcb> lst2 = JsonConvert.DeserializeObject<JsonData<Pcb>>(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                if (lst2 == null) { mainChannel.BasicAck(Rabbitmq.deliveryTag, false); return; };
                #region 开启线程更新数据库

                AoiModel aoiModel = DB.GetAoiModel();
                Action<Pcb> t = (pcb) =>
                {
                    lock (aoiModel)
                    {
                        string res = "";
                        try
                        {
                            if (pcb.results.Count > 0)
                            {
                                pcb.IsError = 1;
                            }
                            aoiModel.pcbs.Add(pcb);
                            aoiModel.results.AddRange(pcb.results);
                            if (aoiModel.SaveChanges() > 0)
                            {
                                res = pcb.PcbNumber + "[已入库]";
                            }
                            else
                            {
                                res = pcb.PcbNumber + "[入库失败]";
                            }
                            aoiModel.Dispose();
                        }
                        catch (Exception er)//UpdateException
                        {
                            res = pcb.PcbNumber + "[入库失败,ID冲突]";
                        }

                        this.BeginInvoke((Action)(() =>
                            {
                                this.Text = res;
                            }));
                    }
                };
                MySmartThreadPool.Instance().QueueWorkItem<Pcb>(t, lst2.data);
                #endregion

                #region 处理图片
                
                string path = ConfigurationManager.AppSettings["FtpPath"] + lst2.data.PcbPath + "/";
                string frontImg = path + "front.jpg";
                string backImg = path + "back.jpg";
                var imageFrontFactory = new ImageFactory().Load(frontImg);
                var imageBackFactory = new ImageFactory().Load(frontImg);
                imageFrontFactory.Quality(70);
                imageBackFactory.Quality(70);

                int drawNum = 0;
                int allNum = lst2.data.results.Count;

                // 画框所有线程中调用显示图片的委托
                Action showImg = () =>
                {
                    drawNum++;
                    if (drawNum >= allNum)
                    {
                        twoSidesPcb.showBackImg(imageBackFactory.Image);
                        twoSidesPcb.showFrontImg(imageFrontFactory.Image);
                        imageFrontFactory.Save(path + "drawfront.jpg");
                        imageBackFactory.Save(path + "drawback.jpg");
                    }
                };

                // 裁剪
                Action<Rectangle, string, string,int> actCrop = (rect, pp, fi, ii) =>
                  {
                      if (!File.Exists(pp))
                      {
                          var partImg = new ImageFactory().Load(fi);
                          partImg.Crop(rect);
                          partImg.Save(pp);
                          if (ii == 0)
                          {
                              int timeOut = 0;
                              while (timeOut<50)
                              {
                                  timeOut++;
                                  if (File.Exists(pp))
                                  {
                                      partOfPcb.BeginInvoke((Action)(() =>
                                      {
                                          partOfPcb.showImgThread(pp);
                                      }));
                                      partImg.Dispose();
                                      break;
                                  }
                                  Thread.Sleep(100);
                              }
                          }
                      }
                  };

                // 正面图画框的委托
                Action<Result, Rectangle, int> actFrontDrawImg = (result, rect, index) =>
                {
                    lock (imageFrontFactory)
                    {

                        #region 在画框之前先裁剪下来用作局部窗体显示使用
                        MySmartThreadPool.Instance().QueueWorkItem(actCrop, rect, path + result.PartImagePath, frontImg, index);
                        #endregion

                        Graphics g = Graphics.FromImage(imageFrontFactory.Image);
                        g.DrawString(result.NgType, new Font("宋体", 10, FontStyle.Bold), Brushes.Red, rect.X, rect.Y - 15);
                        g.DrawRectangle(
                            new Pen(Color.Red, 3),
                            rect);
                        twoSidesPcb.BeginInvoke(showImg);
                    }
                };

                // 背面图画框的委托
                Action<Result, Rectangle, int> actBackDrawImg = (result, rect, index) =>
                {
                    lock (imageBackFactory)
                    {

                        #region 在画框之前先裁剪下来用作局部窗体显示使用
                        MySmartThreadPool.Instance().QueueWorkItem(actCrop, rect, path + result.PartImagePath, backImg, index);
                        #endregion

                        Graphics g = Graphics.FromImage(imageBackFactory.Image);
                        g.DrawString(result.NgType, new Font("宋体", 10, FontStyle.Bold), Brushes.Red, rect.X, rect.Y - 15);
                        g.DrawRectangle(
                            new Pen(Color.Red, 3),
                            rect);
                        twoSidesPcb.BeginInvoke(showImg);
                    }
                };

                for(int i=0; i < lst2.data.results.Count; i++)
                {
                    var result = lst2.data.results[i];
                    string[] reg = result.Region.Split(',');
                    Rectangle rect = new Rectangle(
                            int.Parse(reg[0]),
                            int.Parse(reg[1]),
                            int.Parse(reg[2]),
                            int.Parse(reg[3]));
                    if (result.IsBack == 1)
                    {
                        MySmartThreadPool.Instance().QueueWorkItem(actBackDrawImg, result, rect, i);
                    }
                    else
                    {
                        MySmartThreadPool.Instance().QueueWorkItem(actFrontDrawImg, result, rect, i);
                    }
                }


                //}
                //catch (Exception err)
                //{
                //    string aa = err.Message;
                //}
                #endregion

                #region 加载ng列表
                pcbDetails.BeginInvoke((Action)(() =>
                {
                    pcbDetails.loadData(lst2.data);
                }));
                #endregion


                isLeisure = false;
            }
            else
            {
                channel.BasicNack(Rabbitmq.deliveryTag, false, true);
            }

            // isLeisure = true; // 这个需要在pcbDetails页面最后一个执行完成后执行
            // mainChannel.BasicAck(Rabbitmq.deliveryTag, false); // 这个需要在pcbDetails页面最后一个执行完成后执行


            //htmlLabel1.Invoke((Action)(() =>
            //{
            //    htmlLabel1.Text = $"{message} is news, deal it";
            //}));
            //channel.BasicNack(Rabbitmq.deliveryTag, false, true);
        }

        public void doLeisure()
        {
            isLeisure = true; 
            mainChannel.BasicAck(Rabbitmq.deliveryTag, false);
            pcbDetails.lvList.Items.Clear();
            pcbDetails.lbPcbNumber.Text = "";
            pcbDetails.lbSurfaceNumber.Text = "";
            pcbDetails.lbPcbWidth.Text = "";
            pcbDetails.lbPcbHeight.Text = "";
            pcbDetails.lbPcbChildenNumber.Text = "";
            this.Text = "等待最新的校验信息...";

            twoSidesPcb.BeginInvoke((Action)(() =>
            {
                twoSidesPcb.showFrontImg(null);
                twoSidesPcb.showBackImg(null);
            }));
            partOfPcb.BeginInvoke((Action)(() =>
            {
                partOfPcb.showImg(null);
            }));

        }

        public Main()
        {
            InitializeComponent();

  

            //ShowDockContent();
            //测试代码
            //var f2 = new NewDockContent() { TabText = "Float", CloseButton = false, CloseButtonVisible = false };
            //f2.Show(this.dockPanel1, DockState.DockLeft);
            //var f3 = new Test(this) { TabText = "Float", CloseButton = false, CloseButtonVisible = false };
            //f3.Show(this.dockPanel1, DockState.DockRight);



            //dockPanel1.SuspendLayout(true);

            //CloseAllDocuments();

            CreateStandardControls();

            //aa.Show(dockPanel1, DockState.DockRight);
            //bb.Show(dockPanel1, DockState.DockLeft);


            //dockPanel1.ResumeLayout(true, true);

            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);


        }

        /// <summary>
        /// 键盘监听
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // if it is a hotkey, return true; otherwise, return false
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                    pcbDetails.btnNG.Focus();
                    pcbDetails.lvListNextItemSelect("NG");
                    return true;
                case Keys.Down:
                    pcbDetails.btnOK.Focus();
                    pcbDetails.lvListNextItemSelect("OK");
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CloseAllDocuments()
        {
            if (dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                foreach (IDockContent document in dockPanel1.DocumentsToArray())
                {
                    document.DockHandler.Close();
                }
            }
        }

        private void CreateStandardControls()
        {
            partOfPcb = new PartOfPcb() { TabText = "局部图", CloseButton = false, CloseButtonVisible = false };
            twoSidesPcb = new TwoSidesPcb(partOfPcb) { TabText = "正反图", CloseButton = false, CloseButtonVisible = false };
            pcbDetails = new PcbDetails(this,partOfPcb, twoSidesPcb) { TabText = "结果列表", CloseButton = false, CloseButtonVisible = false };
        }

        public void ttt(string s)
        {
            this.Text = s;
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(PartOfPcb).ToString())
            {
                partOfPcb = new PartOfPcb() { TabText = "局部图", CloseButton = false, CloseButtonVisible = false };
                return partOfPcb;
            }
            else if (persistString == typeof(TwoSidesPcb).ToString())
            {
                twoSidesPcb = new TwoSidesPcb(partOfPcb) { TabText = "正反图", CloseButton = false, CloseButtonVisible = false };
                return twoSidesPcb;
            }
            else if (persistString == typeof(PcbDetails).ToString())
            {
                pcbDetails = new PcbDetails(this, partOfPcb, twoSidesPcb) { TabText = "结果列表", CloseButton = false, CloseButtonVisible = false };
                return pcbDetails;
            }
            else return null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (m_bSaveLayout)
                dockPanel1.SaveAsXml(configFile);
            else if (File.Exists(configFile)) // 不需要保存窗体状态时，删除配置文件。
                File.Delete(configFile);
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                dockPanel1.LoadFromXml(configFile, m_deserializeDockContent);

            #region 队列

            Action<RabbitmqMessageCallback> doS = (val) =>
            {

                if (Rabbitmq.run(doWorkD) == 0)
                {
                    this.BeginInvoke((Action)(() => {
                        MessageBox.Show("连接队列失败!!!");
                        Environment.Exit(0);
                    }));
                }
            };
            doWorkD = new RabbitmqMessageCallback(doWork);
            MySmartThreadPool.Instance().QueueWorkItem(doS, doWorkD);
            #endregion
        }


        private void uiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dockPanel1.SuspendLayout(true);

            CloseAllDocuments();

            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "default.config");

            if (File.Exists(configFile))
                dockPanel1.LoadFromXml(configFile, m_deserializeDockContent);

            dockPanel1.ResumeLayout(true, true);
        }
    }
}
