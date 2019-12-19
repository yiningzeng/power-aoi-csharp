using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace power_aoi
{
    public class DB
    {
        static AoiModel _db;
        public static void init()
        {
            _db = new AoiModel();
        }
        public static AoiModel GetAoiModel()
        {
            return _db;
        }
    }
}
