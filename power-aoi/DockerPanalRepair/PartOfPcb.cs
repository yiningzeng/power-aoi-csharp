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
        public PartOfPcb()
        {
            InitializeComponent();
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
