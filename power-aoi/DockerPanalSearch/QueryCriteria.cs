using Amib.Threading;
using power_aoi.Model;
using power_aoi.SqlPars;
using power_aoi.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace power_aoi.DockerPanalSearch
{
    public partial class QueryCriteria : DockContent
    {
        Query query;
        QueryPars queryPars = new QueryPars();

        bool needSecond = false;
        public QueryCriteria(Query q)
        {
            InitializeComponent();
            query = q;
            queryPars.searchPcbNumberAccurate = true;
            queryPars.startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 00, 00, 00, 00);
            queryPars.endTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59, 999);
        }


        #region 控件事件
        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            dtpStartTime.CustomFormat = "yyyy-MM-dd";
            dtpEndTime.CustomFormat = "yyyy-MM-dd";
            needSecond = false;
        }

        private void rbSecond_CheckedChanged(object sender, EventArgs e)
        {
            dtpStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            needSecond = true;
        }
        

        private void btnLastDay_Click(object sender, EventArgs e)
        {
            dtpStartTime.Value = DateTimeHelper.LastDayStart();
            dtpEndTime.Value = DateTime.Now;
        }

        private void btnLastWeek_Click(object sender, EventArgs e)
        {
            dtpStartTime.Value = DateTimeHelper.LastWeekStart();
            dtpEndTime.Value = DateTime.Now;
        }

        private void btnLastMonth_Click(object sender, EventArgs e)
        {
            dtpStartTime.Value = DateTimeHelper.LastMonthStart();
            dtpEndTime.Value = DateTime.Now;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartTime.Value = DateTimeHelper.TodayStart();
            dtpEndTime.Value = DateTimeHelper.TodayEnd();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            dtpStartTime.Value = DateTimeHelper.WeekStart();
            dtpEndTime.Value = DateTimeHelper.WeekEnd();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            dtpStartTime.Value = DateTimeHelper.MonthStart();
            dtpEndTime.Value = DateTimeHelper.MonthEnd();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cbPcbNumber.Checked)
            {
                if (rbAccurate.Checked)
                {
                    queryPars.searchPcbNumberAccurate = true;
                }
                else
                {
                    queryPars.searchPcbNumberAccurate = false;
                }
                queryPars.pcbNumber = tbPcbNumber.Text.Trim();
            }
            else
            {
                queryPars.pcbNumber = null;
            }
            if (cbPcbName.Checked)
            {
                queryPars.pcbName = combPcbName.Text;
            }
            else
            {
                queryPars.pcbName = null;
            }

            DateTime dt = dtpStartTime.Value;
            if (needSecond)
            {
                queryPars.startTime = dtpStartTime.Value;
            }
            else
            {
                queryPars.startTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00, 00);
            }

            dt = dtpEndTime.Value;
            if (needSecond)
            {
                queryPars.endTime = dtpEndTime.Value;
            }
            else
            {
                queryPars.endTime = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
            }

            query.DoSearch(queryPars);
        }


        #endregion

        private void QueryCriteria_Load(object sender, EventArgs e)
        {
            AoiModel aoiModel = DB.GetAoiModel();
            Action t = () =>
            {
                lock (aoiModel)
                {
                    var tables = aoiModel.pcbNames.Select(s => new { s.Name }).ToList();
                    this.BeginInvoke((Action)(() =>
                    {
                        combPcbName.DataSource = tables;
                        combPcbName.ValueMember = "Name";
                        combPcbName.DisplayMember = "Name";
                    }));
                    aoiModel.Dispose();
                }
            };
            MySmartThreadPool.Instance().QueueWorkItem(t);
        }
    }
}
