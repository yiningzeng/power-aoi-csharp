﻿using Amib.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace power_aoi.Tools
{
    public class MySmartThreadPool
    {
        static SmartThreadPool Pool = new SmartThreadPool();
        public static SmartThreadPool Instance()
        {
            return Pool;
        }
    }
}
