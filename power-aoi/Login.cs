﻿using MetroFramework.Forms;
using power_aoi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
            this.Icon = Properties.Resources.aa;
         
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // if it is a hotkey, return true; otherwise, return false
            switch (keyData)
            {
                case Keys.Enter:
                    btnLoginRepair.Focus();
                    btnLoginRepair.PerformClick();
                    return true;
                //......
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lbResult.Visible = true;
            lbResult.Text = "登录中......";
            MySmartThreadPool.Instance().QueueWorkItem((username, password) => {
                AoiModel aoiModel = DB.GetAoiModel();
                try
                {
                    string md5Pass = Utils.GenerateMD5(password);
                    User user = aoiModel.users.Where(u => u.Username == username && u.Password == md5Pass).FirstOrDefault();
                    if (user != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        this.BeginInvoke((Action)(() =>
                        {
                            lbResult.Text = "用户名或密码错误";
                            lbResult.Visible = true;
                        }));

                    }
                }
                catch (Exception err)
                {
                    this.BeginInvoke((Action)(() =>
                    {
                        lbResult.Text = "连接数据库出错";
                        lbResult.Visible = true;
                    }));
                    LogHelper.WriteLog("Login error", err);
                }
                finally
                {
                    aoiModel.Dispose();
                }
            }, tbUsername.Text.Trim(), tbPassword.Text.Trim());
        }

        private void btnLoginSearch_Click(object sender, EventArgs e)
        {
            AoiModel aoiModel = DB.GetAoiModel();
            try
            {
                lbResult.Visible = true;
                lbResult.Text = "登录中......";
                string md5Pass = Utils.GenerateMD5(tbPassword.Text);
                User user = aoiModel.users.Where(u => u.Username == tbUsername.Text && u.Password == md5Pass).FirstOrDefault();
                if (user != null)
                {
                    this.DialogResult = DialogResult.Yes;
                    //this.Close();
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
            finally
            {
                aoiModel.Dispose();
            }
        }
    }
}
