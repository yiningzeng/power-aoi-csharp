using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace power_aoi.DockerPanal
{
    public partial class PartOfPcb : DockContent
    {
        Main here_f1;
        TwoSidesPcb bbc;
        public PartOfPcb(Main f1, TwoSidesPcb bb)
        {
            InitializeComponent();
            here_f1 = f1;
            bbc = bb;
        }

        public void showImg(string str) {
            if (str == null)
            {
                pbPart.Image = null;
            }
            else
            {
                pbPart.Image = Image.FromFile(ConfigurationManager.AppSettings["FtpPath"] + str);
            }
        }
    }
}
