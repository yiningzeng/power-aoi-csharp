using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace power_aoi
{
    public class Utils
    {
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 缺陷画框行数
        /// </summary>
        /// <param name="bitmap">原始bitmap</param>
        /// <param name="rect">Rectangle</param>
        /// <param name="ngType">缺陷类型</param>
        /// <returns></returns>
        public static Bitmap DrawRect(Bitmap bitmap, Rectangle rect, string ngType)
        {
            Graphics ghFront = Graphics.FromImage(bitmap);
            ghFront.DrawString(ngType, new Font("宋体", 10, FontStyle.Bold), Brushes.Red, rect.X, rect.Y - 15);
            ghFront.DrawRectangle(
                new Pen(Color.Red, 3),
                rect);
            return bitmap;
        }

        /// <summary>
        /// 缺陷画框行数
        /// </summary>
        /// <param name="bitmap">原始bitmap</param>
        /// <param name="rect">Rectangle</param>
        /// <param name="ngType">缺陷类型</param>
        /// <returns></returns>
        public static Bitmap DrawLine(Bitmap aa, Rectangle rect)
        {
            Bitmap bitmap = aa.Clone(new Rectangle(0, 0, aa.Width, aa.Height), aa.PixelFormat);
            Graphics ghFront = Graphics.FromImage(bitmap);
            Pen newPen = new Pen(Color.Yellow, 50);//定义一个画笔，黄色
            ghFront.DrawLine(newPen, new Point(rect.X, 0), new Point(rect.X, bitmap.Height));
            ghFront.DrawLine(newPen, new Point(0, rect.Y), new Point(bitmap.Width, rect.Y));
            return bitmap;
        }
        /// <summary>  
        /// 剪裁 -- 用GDI+   
        /// </summary>  
        /// <param name="b">原始Bitmap</param>  
        /// <returns>剪裁后的Bitmap</returns>  
        public static Bitmap BitmapCut(Bitmap b, Rectangle rect)
        {
            int StartX = rect.X, StartY = rect.Y, iWidth = rect.Width, iHeight = rect.Height;
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }
    }
}
