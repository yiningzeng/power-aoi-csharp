using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace power_aoi
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // if it is a hotkey, return true; otherwise, return false
            switch (keyData)
            {
                case Keys.Enter:
                    btnLogin.Focus();
                    btnLogin.PerformClick();
                    return true;
                //......
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lbResult.Visible = true;
                lbResult.Text = "登录中......";
                string md5Pass = Utils.GenerateMD5(tbPassword.Text);
                User user = DB.GetAoiModel().Users.Where(u => u.username == tbUsername.Text && u.password == md5Pass).FirstOrDefault();
                if (user != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lbResult.Text = "用户名或密码错误";
                    lbResult.Visible = true;
                }
            }
            catch (Exception err)
            {
                lbResult.Text = "连接数据库出错";
                lbResult.Visible = true;
                LogHelper.WriteLog("Login error", err);
            }
        }
    }
}
