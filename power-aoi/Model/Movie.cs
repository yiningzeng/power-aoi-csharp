using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace power_aoi
{
    public class movie
    {
        [Description("主键")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Description("电影名称")]
        public string Title { get; set; }
        [Description("日期")]
        public DateTime ReleaseDate { get; set; }
        [Description("类型")]
        public string Genre { get; set; }
        [Description("价格")]
        public decimal Price { get; set; }
    }
}
