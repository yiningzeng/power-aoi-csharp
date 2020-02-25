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

        Rectangle globalFrontRect, globalBackRect;

        //Rectangle rectangle
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

        void drawLine(bool isBack)
        {
            try
            {
                PictureBox pictureBox = null;
                Rectangle rect;
                if (isBack)
                {
                    pictureBox = pbBack;
                    rect = globalBackRect;
                }
                else
                {
                    pictureBox = pbFront;
                    rect = globalFrontRect;
                }
                // 先计算图像缩放的比例
                double wPro = (double)pictureBox.Width / (double)pictureBox.Image.Width; // pictureBox.Image.Width改成全局
                double hPro = (double)pictureBox.Height / (double)pictureBox.Image.Height;
                Rectangle newRect = new Rectangle(
                    Convert.ToInt32((rect.X + rect.Width / 2) * wPro),
                    Convert.ToInt32((rect.Y + rect.Height / 2) * hPro),
                    Convert.ToInt32(rect.Width * wPro),
                    Convert.ToInt32(rect.Height * hPro));

                pictureBox.Refresh();
                Graphics ghFront = pictureBox.CreateGraphics();

                Pen newPen = new Pen(Color.Yellow, 2);//定义一个画笔，黄色

                pictureBox.Update();//这句话相当关键  会是消除之前画的图 速度加快
                ghFront.DrawLine(newPen, new Point(newRect.X, 0), new Point(newRect.X, pictureBox.Height));
                ghFront.DrawLine(newPen, new Point(0, newRect.Y), new Point(pictureBox.Width, newRect.Y));
            }
            catch(Exception er) { }
            //LogHelper.WriteLog(string.Format("pictureBox [ width: {0}, height: {1}]\nfileimages [ width: {2}, height: {3}]", pictureBox.Width, pictureBox.Height, pictureBox.Image.Width, pictureBox.Image.Height));
     
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                drawLine(false);
            }
            else if(tabControl.SelectedIndex == 1)
            {
                drawLine(true);
            }
        }

        public void pictureBoxDraw(bool front, Rectangle rect)
        {
            if (front)
            {
                globalFrontRect = rect;
                drawLine(false);
            }
            else {
                globalBackRect = rect;
                drawLine(true);
            }
        }
    }
}
