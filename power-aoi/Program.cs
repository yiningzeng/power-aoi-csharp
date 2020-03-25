
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

            bool Exist;//定义一个bool变量，用来表示是否已经运行
                       //创建Mutex互斥对象
            System.Threading.Mutex newMutex = new System.Threading.Mutex(true, "仅一次", out Exist);
            if (Exist)//如果没有运行
            {
                newMutex.ReleaseMutex();//运行新窗体
                Login login = new Login();
                DialogResult dialogResult = login.ShowDialog();
                login.Close();
                if (dialogResult == DialogResult.OK)
                {
                    Application.Run(new Main());
                }
                else if (dialogResult == DialogResult.Yes)
                {
                    Application.Run(new MainSearch());
                }
            }
            else
            {
                MessageBox.Show("本程序一次只能运行一个实例！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);//弹出提示信息
            }


        }
    }
}
