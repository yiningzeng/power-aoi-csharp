﻿using FubarDev.FtpServer;
using FubarDev.FtpServer.FileSystem.DotNet;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using power_aoi.DockerPanal;
using power_aoi.Model;
using power_aoi.Tools;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                pcbDetails.BeginInvoke((Action)(() =>
                {

                    JsonData<Pcb> lst2 = JsonConvert.DeserializeObject<JsonData<Pcb>>(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    pcbDetails.loadData(lst2.data);
                }));
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
            pcbDetails.gbPcb.Text = $"板号: ";
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
            twoSidesPcb = new TwoSidesPcb() { TabText = "正反图", CloseButton = false, CloseButtonVisible = false };
            partOfPcb = new PartOfPcb(this, twoSidesPcb) { TabText = "局部图", CloseButton = false, CloseButtonVisible = false };
            pcbDetails = new PcbDetails(this,partOfPcb, twoSidesPcb) { TabText = "结果列表", CloseButton = false, CloseButtonVisible = false };
        }

        public void ttt(string s)
        {
            this.Text = s;
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(TwoSidesPcb).ToString())
            {
                twoSidesPcb = new TwoSidesPcb() { TabText = "正反图", CloseButton = false, CloseButtonVisible = false };
                return twoSidesPcb;
            }
            else if (persistString == typeof(PartOfPcb).ToString())
            {
                partOfPcb = new PartOfPcb(this, twoSidesPcb) { TabText = "局部图", CloseButton = false, CloseButtonVisible = false };
                return partOfPcb;
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
            doWorkD = new RabbitmqMessageCallback(doWork);
            Rabbitmq.run(doWorkD);
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