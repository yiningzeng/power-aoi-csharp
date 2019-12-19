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
    public partial class PcbDetails : DockContent
    {
        PartOfPcb partOfPcb;
        TwoSidesPcb twoSidesPcb;
        //public PcbDetails()
        //{
        //    InitializeComponent();
        //}

        public PcbDetails(PartOfPcb pPcb, TwoSidesPcb tPcb)
        {
            InitializeComponent();
            partOfPcb = pPcb;
            twoSidesPcb = tPcb;
        }
    }
}
