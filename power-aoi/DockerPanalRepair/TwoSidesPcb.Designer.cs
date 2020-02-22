namespace power_aoi.DockerPanal
{
    partial class TwoSidesPcb
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pbFront = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFront)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 352);
            this.panel1.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Enabled = false;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(566, 352);
            this.tabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pbFront);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(558, 326);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "正面";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pbFront
            // 
            this.pbFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbFront.Location = new System.Drawing.Point(3, 3);
            this.pbFront.Margin = new System.Windows.Forms.Padding(0);
            this.pbFront.Name = "pbFront";
            this.pbFront.Size = new System.Drawing.Size(552, 320);
            this.pbFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFront.TabIndex = 0;
            this.pbFront.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pbBack);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(558, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "反面";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pbBack
            // 
            this.pbBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbBack.Location = new System.Drawing.Point(3, 3);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(552, 320);
            this.pbBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBack.TabIndex = 0;
            this.pbBack.TabStop = false;
            // 
            // TwoSidesPcb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 352);
            this.Controls.Add(this.panel1);
            this.Name = "TwoSidesPcb";
            this.panel1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFront)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TabControl tabControl;
        public System.Windows.Forms.PictureBox pbBack;
        public System.Windows.Forms.PictureBox pbFront;
    }
}
