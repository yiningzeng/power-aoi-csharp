using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Configuration;

namespace power_aoi.DockerPanal
{
    public partial class TwoSidesPcb : DockContent
    {
        bool draw = true;
        public TwoSidesPcb()
        {
            InitializeComponent();
        }
        public void fuck(string aa)
        {
            //label1.Text = aa;
        }

        public void showFrontImg(string str)
        {
            if (str == null)
            {
                pbFront.Image = null;
            }
            else
            {
                pbFront.Image = Image.FromFile(ConfigurationManager.AppSettings["FtpPath"] + str);
            }
        }

        public void showBackImg(string str)
        {
            if (str == null)
            {
                pbBack.Image = null;
            }
            else
            {
                pbBack.Image = Image.FromFile(ConfigurationManager.AppSettings["FtpPath"] + str);
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            //if (draw)
            //{
            //    Graphics gh = e.Graphics;
            //    Pen pp = new Pen(Color.Red);
            //    Rectangle rect = new Rectangle();
            //    rect.X = 0;
            //    rect.Y = 0;
            //    rect.Width = 50;
            //    rect.Height = 60;
            //    rect.Location = new Point(0, 0);
            //    gh.DrawRectangle(new Pen(Color.FromArgb(255, 60, 60)), rect);
            //    //draw = false;
            //}

        }
    }
}
