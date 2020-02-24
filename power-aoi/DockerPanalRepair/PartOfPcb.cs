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
using WeifenLuo.WinFormsUI.Docking;

namespace power_aoi.DockerPanal
{
    public partial class PartOfPcb : DockContent
    {
        Rectangle globalRect;
        string globalNgType;
        double globalImageWidth, globalImageHeight;
        public PartOfPcb()
        {
            InitializeComponent();
        }

        public void drawRect()
        {
            try
            {
                // 先计算图像缩放的比例
                double wPro = (double)pbPart.Width / globalImageWidth; // pictureBox.Image.Width改成全局
                double hPro = (double)pbPart.Height / globalImageHeight;
                Rectangle newRect = new Rectangle(
                    Convert.ToInt32((globalRect.X + globalRect.Width / 2) * wPro),
                    Convert.ToInt32((globalRect.Y + globalRect.Height / 2) * hPro),
                    Convert.ToInt32(globalRect.Width * wPro),
                    Convert.ToInt32(globalRect.Height * hPro));
                newRect.Inflate(10, 10);
                pbPart.Refresh();
                Graphics ghFront = pbPart.CreateGraphics();

                Pen newPen = new Pen(Color.Red, 1);//定义一个画笔，黄色

                pbPart.Update();//这句话相当关键  会是消除之前画的图 速度加快
                ghFront.DrawRectangle(newPen, newRect);
                ghFront.DrawString(globalNgType, new Font("宋体", 10, FontStyle.Bold), Brushes.Red, newRect.X, newRect.Y - 15);
                //ghFront.DrawLine(newPen, new Point(newRect.X, 0), new Point(newRect.X, pictureBox.Height));
                //ghFront.DrawLine(newPen, new Point(0, newRect.Y), new Point(pictureBox.Width, newRect.Y));
            }
            catch (Exception er) { }
            //LogHelper.WriteLog(string.Format("pictureBox [ width: {0}, height: {1}]\nfileimages [ width: {2}, height: {3}]", pictureBox.Width, pictureBox.Height, pictureBox.Image.Width, pictureBox.Image.Height));

        }


        public void showImgThread(Image image, Rectangle rect, string ngType)
        {
            if (image == null)
            {
                pbPart.Image = null;
            }
            else
            {
                pbPart.Image = image;
                globalRect = rect;
                globalNgType = ngType;
                globalImageWidth = image.Width;
                globalImageHeight = image.Height;
                drawRect();
            }
        }

        public void showImgThread(string image)
        {
            if (image == null)
            {
                pbPart.Image = null;
            }
            else
            {
                pbPart.Image = Image.FromFile(image);
            }
        }

        private void pbPart_SizeChanged(object sender, EventArgs e)
        {
            drawRect();
        }

        public void showImg(string str) {
            if (str == null)
            {
                pbPart.Image = null;
            }
            else
            {
                string path = ConfigurationManager.AppSettings["FtpPath"] + str;
                if (File.Exists(path))
                {
                    pbPart.Image = Image.FromFile(path);
                }
            }
        }


    }
}
