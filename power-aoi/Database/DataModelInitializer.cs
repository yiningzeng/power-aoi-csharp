using power_aoi.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace power_aoi.Database
{
    public class DataModelInitializer : DropCreateDatabaseIfModelChanges<SwartzUserUserContext>
    {
        protected override void Seed(SwartzUserUserContext context)
        {
            //初始化数据
            context.UserInfo.Add(new MUserInfo
            {
                ID = 1,
                Age = 12,
                Name = "dzjx"
            });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
