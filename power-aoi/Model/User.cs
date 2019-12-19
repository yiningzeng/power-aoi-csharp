using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace power_aoi
{
    [Table(name:"users")]
    public class User
    {
        [Description("主键")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Description("用户名")]
        public string username { get; set; }
        [Description("密码")]
        public string password { get; set; }
        [Description("类型")]
        public string type { get; set; }
        [Description("创建时间")]
        public DateTime create_time { get; set; }
    }
}
