using power_aoi.DockerPanalSearch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace power_aoi
{
    public partial class MainSearch : DockContent
    {
        QueryCriteria queryCriteria;
        Query query;
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        public MainSearch()
        {
            InitializeComponent();

            CreateStandardControls();

            queryCriteria.Show(this.dockPanel1, DockState.DockLeft);
            query.Show(this.dockPanel1, DockState.Document);
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(Query).ToString())
            {
                query = new Query(this) { TabText = "查询结果", CloseButton = false, CloseButtonVisible = false };
                return query;
            }
            else if(persistString == typeof(QueryCriteria).ToString())
            {
                queryCriteria = new QueryCriteria(query) { TabText = "查询条件", CloseButton = false, CloseButtonVisible = false };
                return queryCriteria;
            }
            else return null;
        }

        private void CreateStandardControls()
        {
            query = new Query(this) { TabText = "查询结果", CloseButton = false, CloseButtonVisible = false };
            queryCriteria = new QueryCriteria(query) { TabText = "查询条件", CloseButton = false, CloseButtonVisible = false };
        }

        public void SetQueryCriteriaControlEnable(bool enable)
        {
            queryCriteria.panelPcbNumber.Enabled = enable;
            queryCriteria.panelPcbName.Enabled = enable;
        }


        /// <summary>
        /// 键盘监听
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // if it is a hotkey, return true; otherwise, return false
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "MainSearch.config");
            if (m_bSaveLayout)
                dockPanel1.SaveAsXml(configFile);
            else if (File.Exists(configFile)) // 不需要保存窗体状态时，删除配置文件。
                File.Delete(configFile);
            Environment.Exit(0);
        }

        private void MainSearch_Load(object sender, EventArgs e)
        {
            try
            {
                string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "MainSearch.config");

                if (File.Exists(configFile))
                    dockPanel1.LoadFromXml(configFile, m_deserializeDockContent);
            }
            catch(Exception er)
            {
                LogHelper.WriteLog("MainSearch", er);
            }

        }
    }
}
