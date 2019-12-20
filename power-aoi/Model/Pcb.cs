using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace power_aoi.Model
{
    [Table(name: "pcbs")]
    public class Pcb
    {
        [Description("主键")]
        [Key]
        [Column(name:"id",TypeName ="bigint")]
        public long Id { get; set; }

        [Column(name: "pcb_number")]
        [Description("板号")]
        public string PcbNumber { get; set; }

        [Column(name: "pcb_name")]
        [Description("PCB名称")]
        public string PcbName { get; set; }

        [Column(name: "pcb_width")]
        [Description("PCB名称")]
        public int PcbWidth { get; set; }

        [Column(name: "pcb_height")]
        [Description("PCB名称")]
        public int PcbHeight { get; set; }

        [Column(name: "pcb_childen_number")]
        [Description("PCB名称")]
        public int PcbChildenNumber { get; set; }

        [Column(name: "surface_number")]
        [Description("检测的面数")]
        public int SurfaceNumber { get; set; }
        

        [Column(name: "pcb_path")]
        [Description("对应的FTP保存的地址")]
        public string PcbPath { get; set; }

        [Column(name: "create_time")]
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }

        public List<Result> results { get; set; }
    }
}
