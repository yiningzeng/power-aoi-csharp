using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace power_aoi.Model
{
    class ChildrenPcbMarkerInfo
    {
        [Description("是否是反面")]
        public bool isBack { get; set; } = false;

        [Description("对应的marker点的区域")]
        public Rectangle MarkerRect { get; set; }

        [Description("marker点对应的板子的区域")]
        public Rectangle ChildrenPcbRect { get; set; }
    }

    class PcbAlgorithmsInfo
    {
        public Dictionary<string, ChildrenPcbMarkerInfo> childrenPcbMarkerInfos { get; set; } = new Dictionary<string, ChildrenPcbMarkerInfo>(); //new List<ChildrenPcbMarkerInfo>();
    }
}
