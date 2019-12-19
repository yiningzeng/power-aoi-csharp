
using System;
using System.Collections.Generic;

using System.Linq;

using System.Windows.Forms;

namespace power_aoi
{

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DB.init();
            Login login = new Login();
            DialogResult dialogResult = login.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
    
                Application.Run(new Main());
            }
        }
    }
}
