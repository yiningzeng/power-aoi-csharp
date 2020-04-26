using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskWatcher
{
    public partial class Form1 : Form
    {
        Tip tip;
        public Form1()
        {

            InitializeComponent();
            timer1.Start();
            timerAutoDeletePath.Start();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        public void AutoDelete()
        {
            try
            {
                
                string disk = ConfigurationManager.AppSettings["AutoDeletePath"];
                //C#遍历指定文件夹中的所有文件 
                DirectoryInfo TheFolder = new DirectoryInfo(disk);
                if (!TheFolder.Exists)
                    return;

                //遍历文件夹
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    Console.WriteLine();
                    Console.WriteLine(NextFolder.FullName);
                    DateTime todayZero = Convert.ToDateTime(DateTime.Now.ToString("D").ToString());
                    if(NextFolder.LastWriteTime < todayZero)
                    {
                        Directory.Delete(NextFolder.FullName,true);
                    }
                   
                }


                //遍历文件
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    //if (NextFile.Name == "0-0-11.grid")
                    //    continue;
                    // 获取文件完整路径
                    string heatmappath = NextFile.FullName;
                    Console.WriteLine(heatmappath);
                    DateTime todayZero = Convert.ToDateTime(DateTime.Now.ToString("D").ToString());
                    if (NextFile.LastWriteTime < todayZero)
                    {
                        File.Delete(heatmappath);
                    }
                }
            }
            catch (Exception er)
            {

            }
        }

        #region 【WinForm禁用窗体的关闭按钮】

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                var myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        #endregion
        private void timerAutoDeletePath_Tick(object sender, EventArgs e)
        {
            AutoDelete();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string disk = ConfigurationManager.AppSettings["Disk"];
                int lim = Convert.ToInt32(ConfigurationManager.AppSettings["DiskRemind"]);
                long freeGb = Utils.GetHardDiskFreeSpace(disk);
                if (freeGb < lim)
                {
                    if (tip == null)
                    {
                        tip = new Tip(disk + "盘空间已经不足" + lim + "GB，请及时清理");
                        DialogResult dialogResult = tip.ShowDialog();
                        if(dialogResult == DialogResult.Cancel)
                        {
                            tip = null;
                        }
                    }
                }
                Console.WriteLine("1");
            }
            catch(Exception er)
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                button1.Text = "开启监控";
            }
            else
            {
                timer1.Start();
                button1.Text = "停止监控";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AutoDelete();
        }

        private void 手动清理硬盘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoDelete();
        }
    }
}
