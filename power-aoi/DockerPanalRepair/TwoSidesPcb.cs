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
using power_aoi.Tools;
using ImageProcessor;
using ImageProcessor.Imaging;
using power_aoi.Model;
using Amib.Threading;
using System.Threading;

namespace power_aoi.DockerPanal
{
    public partial class TwoSidesPcb : DockContent
    {
        bool draw = true;
        PartOfPcb partOfPcb;
        public TwoSidesPcb(PartOfPcb partP)
        {
            InitializeComponent();
            partOfPcb = partP;
        }

        public void showFrontImg(Image image)
        {
            pbFront.Image = image;
        }

        public void showBackImg(Image image)
        {
            pbBack.Image = image;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            //if (draw)
            //{
            //    Graphics gh = e.Graphics;
            //    Pen pp = new Pen(Color.Red);
            //    Rectangle rect = new Rectangle();
            //    rect.X = 80;
            //    rect.Y = 90;
            //    rect.Width = 50;
            //    rect.Height = 60;
            //    rect.Location = new Point(0, 0);
            //    gh.DrawRectangle(new Pen(Color.FromArgb(255, 60, 60)), rect);
            //    draw = false;
            //}

        }
    }
}
