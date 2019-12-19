using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void button1_Click(object sender, EventArgs e)
        {
            bbc.fuck("s");
            here_f1.ttt("ssssssssssssssssssssssssssssssssssss");
        }
    }
}
