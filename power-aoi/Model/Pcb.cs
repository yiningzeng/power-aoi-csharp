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

        [Column(name: "pcb_other_number")]
        [Description("料号")]
        public string PcbOtherNumber { get; set; }

        [Column(name: "pcb_path")]
        [Description("对应的FTP保存的地址")]
        public string PcbPath { get; set; }

        [Column(name: "create_time")]
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }

        public List<Result> results { get; set; }
    }
}
