using power_aoi.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace power_aoi.Database
{
    // [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))] 
    // 可以配置文件进行配置(codeConfigurationType)
    public class SwartzUserUserContext : DbContext
    {

        //使用UserContext connectionString
        public SwartzUserUserContext() : base("aoi")
        {

        }

        public DbSet<MUserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除将表名称设置为实体名称的约定
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //表名加swartz前缀
            modelBuilder.Types().Configure(f => f.ToTable("swartz" + f.ClrType.Name));
        }
    }
}
