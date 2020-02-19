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
        Bitmap frontBitmap, backBitmap;
        public TwoSidesPcb(PartOfPcb partP)
        {
            InitializeComponent();
            partOfPcb = partP;
        }

        public void showFrontImg(Image image)
        {
            frontBitmap = (Bitmap)image;
            pbFront.Image = image;
        }

        public void showBackImg(Image image)
        {
            backBitmap = (Bitmap)image;
            pbBack.Image = image;
        }

        void drawLine(PictureBox pictureBox, Rectangle rect)
        {
            //pictureBox.Refresh();
            Graphics ghFront = pictureBox.CreateGraphics();

            Pen newPen = new Pen(Color.Yellow, 5);//定义一个画笔，黄色

            pictureBox.Update();//这句话相当关键  会是消除之前画的图 速度加快
            ghFront.DrawLine(newPen, new Point(2401, 0), new Point(2401, 250));
            //ghFront.DrawLine(newPen, new Point(0, rect.Y), new Point(pictureBox.Image.Width, rect.Y));
     
        }

        public void pictureBoxDraw(bool front, Rectangle rect)
        {
            //pbFront.Image = Utils.DrawLine(frontBitmap, rect);
            drawLine(pbFront, rect);
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics gh = e.Graphics;

            Pen newPen = new Pen(Color.Yellow, 5);//定义一个画笔，黄色

            pbFront.Update();//这句话相当关键  会是消除之前画的图 速度加快
            gh.DrawLine(newPen, new Point(2401, 0), new Point(2401, 1000));
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
