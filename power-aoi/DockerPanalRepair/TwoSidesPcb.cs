using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

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
            // 先计算图像缩放的比例
            double wPro = (double)pictureBox.Width / (double)pictureBox.Image.Width;
            double hPro = (double)pictureBox.Height / (double)pictureBox.Image.Height;
            Rectangle newRect = new Rectangle(
                Convert.ToInt32((rect.X + rect.Width/2) * wPro),
                Convert.ToInt32((rect.Y + rect.Height/2) * hPro),
                Convert.ToInt32(rect.Width * wPro),
                Convert.ToInt32(rect.Height * hPro));

            pictureBox.Refresh();
            Graphics ghFront = pictureBox.CreateGraphics();

            Pen newPen = new Pen(Color.Yellow, 2);//定义一个画笔，黄色

            pictureBox.Update();//这句话相当关键  会是消除之前画的图 速度加快
            ghFront.DrawLine(newPen, new Point(newRect.X, 0), new Point(newRect.X, pictureBox.Height));
            ghFront.DrawLine(newPen, new Point(0, newRect.Y), new Point(pictureBox.Width, newRect.Y));


            //LogHelper.WriteLog(string.Format("pictureBox [ width: {0}, height: {1}]\nfileimages [ width: {2}, height: {3}]", pictureBox.Width, pictureBox.Height, pictureBox.Image.Width, pictureBox.Image.Height));
     
        }

        public void pictureBoxDraw(bool front, Rectangle rect)
        {
            if (front) drawLine(pbFront, rect); else drawLine(pbBack, rect);
        }
    }
}
