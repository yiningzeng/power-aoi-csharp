using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace power_aoi.Model
{
    [Serializable]
    [Table(name: "results")]
    public class Result
    {
        [Description("主键")]
        [Key]
        [Column(name: "id", TypeName = "bigint")]
        public long Id { get; set; }

        [Column(name: "is_front")]
        [Description("是否正面,0不是，1是")]
        public int IsFront { get; set; }

        [Column(name: "pdb_id")]
        [Description("对应的pcb板id")]
        public long PcbId { get; set; }

        [Column(name: "element_number")]
        [Description("元件位号")]
        public string ElementNumber { get; set; }

        [Column(name: "region")]
        [Description("范围 坐标")]
        public string Region { get; set; }
        
        [Column(name: "ng_type")]
        [Description("Ng种类")]
        public string NgType { get; set; }

        [Column(name: "is_misjudge")]
        [Description("是否误判, 0否 1是")]
        public int IsMisjudge { get; set; } = 0;

        [Column(name: "result_string")]
        [Description("校验结果")]
        public string ResultString { get; set; }

        [Column(name: "part_image_path")]
        [Description("缺陷的局部图相对于FTP路径的地址")]
        public string PartImagePath { get; set; }

        [Column(name: "create_time")]
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }

    }
}
