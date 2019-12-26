namespace power_aoi.DockerPanalSearch
{
    partial class QueryCriteria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbAccurate = new System.Windows.Forms.RadioButton();
            this.rbVague = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.rbSecond = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.combPcbName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPcbName = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbPcbNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPcbNumber = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnWeek = new System.Windows.Forms.Button();
            this.btnMonth = new System.Windows.Forms.Button();
            this.btnLastMonth = new System.Windows.Forms.Button();
            this.btnLastDay = new System.Windows.Forms.Button();
            this.btnLastWeek = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.panelPcbNumber = new System.Windows.Forms.Panel();
            this.panelPcbName = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelPcbNumber.SuspendLayout();
            this.panelPcbName.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 586);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.panelPcbName);
            this.groupBox1.Controls.Add(this.panelPcbNumber);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 364);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.rbAccurate);
            this.panel3.Controls.Add(this.rbVague);
            this.panel3.Location = new System.Drawing.Point(77, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(183, 26);
            this.panel3.TabIndex = 1;
            // 
            // rbAccurate
            // 
            this.rbAccurate.AutoSize = true;
            this.rbAccurate.Checked = true;
            this.rbAccurate.Location = new System.Drawing.Point(5, 5);
            this.rbAccurate.Name = "rbAccurate";
            this.rbAccurate.Size = new System.Drawing.Size(71, 16);
            this.rbAccurate.TabIndex = 14;
            this.rbAccurate.TabStop = true;
            this.rbAccurate.Text = "精确查询";
            this.rbAccurate.UseVisualStyleBackColor = true;
            // 
            // rbVague
            // 
            this.rbVague.AutoSize = true;
            this.rbVague.Location = new System.Drawing.Point(82, 5);
            this.rbVague.Name = "rbVague";
            this.rbVague.Size = new System.Drawing.Size(71, 16);
            this.rbVague.TabIndex = 13;
            this.rbVague.Text = "模糊查询";
            this.rbVague.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbDay);
            this.panel2.Controls.Add(this.rbSecond);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(159, 25);
            this.panel2.TabIndex = 15;
            // 
            // rbDay
            // 
            this.rbDay.AutoSize = true;
            this.rbDay.Checked = true;
            this.rbDay.Location = new System.Drawing.Point(3, 4);
            this.rbDay.Name = "rbDay";
            this.rbDay.Size = new System.Drawing.Size(71, 16);
            this.rbDay.TabIndex = 12;
            this.rbDay.TabStop = true;
            this.rbDay.Text = "精确到天";
            this.rbDay.UseVisualStyleBackColor = true;
            this.rbDay.CheckedChanged += new System.EventHandler(this.rbDay_CheckedChanged);
            // 
            // rbSecond
            // 
            this.rbSecond.AutoSize = true;
            this.rbSecond.Location = new System.Drawing.Point(80, 4);
            this.rbSecond.Name = "rbSecond";
            this.rbSecond.Size = new System.Drawing.Size(71, 16);
            this.rbSecond.TabIndex = 11;
            this.rbSecond.Text = "精确到秒";
            this.rbSecond.UseVisualStyleBackColor = true;
            this.rbSecond.CheckedChanged += new System.EventHandler(this.rbSecond_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(15, 336);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(233, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "查询";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.combPcbName);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(4, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(256, 40);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // combPcbName
            // 
            this.combPcbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combPcbName.FormattingEnabled = true;
            this.combPcbName.Location = new System.Drawing.Point(83, 14);
            this.combPcbName.Name = "combPcbName";
            this.combPcbName.Size = new System.Drawing.Size(164, 20);
            this.combPcbName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "选择PCB名称";
            // 
            // cbPcbName
            // 
            this.cbPcbName.AutoSize = true;
            this.cbPcbName.Location = new System.Drawing.Point(4, 3);
            this.cbPcbName.Name = "cbPcbName";
            this.cbPcbName.Size = new System.Drawing.Size(90, 16);
            this.cbPcbName.TabIndex = 8;
            this.cbPcbName.Text = "PCB名称查询";
            this.cbPcbName.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.tbPcbNumber);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(4, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(256, 40);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // tbPcbNumber
            // 
            this.tbPcbNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPcbNumber.Location = new System.Drawing.Point(65, 14);
            this.tbPcbNumber.Name = "tbPcbNumber";
            this.tbPcbNumber.Size = new System.Drawing.Size(182, 21);
            this.tbPcbNumber.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "输入板号";
            // 
            // cbPcbNumber
            // 
            this.cbPcbNumber.AutoSize = true;
            this.cbPcbNumber.Location = new System.Drawing.Point(4, 6);
            this.cbPcbNumber.Name = "cbPcbNumber";
            this.cbPcbNumber.Size = new System.Drawing.Size(72, 16);
            this.cbPcbNumber.TabIndex = 6;
            this.cbPcbNumber.Text = "板号查询";
            this.cbPcbNumber.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dtpStartTime);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.dtpEndTime);
            this.groupBox3.Location = new System.Drawing.Point(3, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 132);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.btnToday, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnWeek, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnMonth, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnLastMonth, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnLastDay, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnLastWeek, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 65);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 62);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // btnToday
            // 
            this.btnToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToday.Location = new System.Drawing.Point(3, 34);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(74, 25);
            this.btnToday.TabIndex = 9;
            this.btnToday.Text = "当 天";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnWeek
            // 
            this.btnWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWeek.Location = new System.Drawing.Point(83, 34);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(74, 25);
            this.btnWeek.TabIndex = 8;
            this.btnWeek.Text = "本 周";
            this.btnWeek.UseVisualStyleBackColor = true;
            this.btnWeek.Click += new System.EventHandler(this.btnWeek_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMonth.Location = new System.Drawing.Point(163, 34);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(74, 25);
            this.btnMonth.TabIndex = 7;
            this.btnMonth.Text = "本 月";
            this.btnMonth.UseVisualStyleBackColor = true;
            this.btnMonth.Click += new System.EventHandler(this.btnMonth_Click);
            // 
            // btnLastMonth
            // 
            this.btnLastMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLastMonth.Location = new System.Drawing.Point(163, 3);
            this.btnLastMonth.Name = "btnLastMonth";
            this.btnLastMonth.Size = new System.Drawing.Size(74, 25);
            this.btnLastMonth.TabIndex = 6;
            this.btnLastMonth.Text = "最近一月";
            this.btnLastMonth.UseVisualStyleBackColor = true;
            this.btnLastMonth.Click += new System.EventHandler(this.btnLastMonth_Click);
            // 
            // btnLastDay
            // 
            this.btnLastDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLastDay.Location = new System.Drawing.Point(3, 3);
            this.btnLastDay.Name = "btnLastDay";
            this.btnLastDay.Size = new System.Drawing.Size(74, 25);
            this.btnLastDay.TabIndex = 4;
            this.btnLastDay.Text = "最近一天";
            this.btnLastDay.UseVisualStyleBackColor = true;
            this.btnLastDay.Click += new System.EventHandler(this.btnLastDay_Click);
            // 
            // btnLastWeek
            // 
            this.btnLastWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLastWeek.Location = new System.Drawing.Point(83, 3);
            this.btnLastWeek.Name = "btnLastWeek";
            this.btnLastWeek.Size = new System.Drawing.Size(74, 25);
            this.btnLastWeek.TabIndex = 5;
            this.btnLastWeek.Text = "最近一周";
            this.btnLastWeek.UseVisualStyleBackColor = true;
            this.btnLastWeek.Click += new System.EventHandler(this.btnLastWeek_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(65, 11);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(183, 21);
            this.dtpStartTime.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束时间";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(65, 38);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(183, 21);
            this.dtpEndTime.TabIndex = 2;
            // 
            // panelPcbNumber
            // 
            this.panelPcbNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPcbNumber.Controls.Add(this.panel3);
            this.panelPcbNumber.Controls.Add(this.cbPcbNumber);
            this.panelPcbNumber.Controls.Add(this.groupBox4);
            this.panelPcbNumber.Location = new System.Drawing.Point(3, 181);
            this.panelPcbNumber.Margin = new System.Windows.Forms.Padding(0);
            this.panelPcbNumber.Name = "panelPcbNumber";
            this.panelPcbNumber.Size = new System.Drawing.Size(263, 75);
            this.panelPcbNumber.TabIndex = 1;
            // 
            // panelPcbName
            // 
            this.panelPcbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPcbName.Controls.Add(this.cbPcbName);
            this.panelPcbName.Controls.Add(this.groupBox5);
            this.panelPcbName.Location = new System.Drawing.Point(3, 259);
            this.panelPcbName.Name = "panelPcbName";
            this.panelPcbName.Size = new System.Drawing.Size(263, 71);
            this.panelPcbName.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Location = new System.Drawing.Point(3, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(263, 162);
            this.panel5.TabIndex = 1;
            // 
            // QueryCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 586);
            this.Controls.Add(this.panel1);
            this.Name = "QueryCriteria";
            this.Text = "QueryCriteria";
            this.Load += new System.EventHandler(this.QueryCriteria_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelPcbNumber.ResumeLayout(false);
            this.panelPcbNumber.PerformLayout();
            this.panelPcbName.ResumeLayout(false);
            this.panelPcbName.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbPcbNumber;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbPcbName;
        private System.Windows.Forms.TextBox tbPcbNumber;
        private System.Windows.Forms.ComboBox combPcbName;
        private System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDay;
        private System.Windows.Forms.RadioButton rbSecond;
        private System.Windows.Forms.Button btnLastMonth;
        private System.Windows.Forms.Button btnLastWeek;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnWeek;
        private System.Windows.Forms.Button btnMonth;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnLastDay;
        private System.Windows.Forms.RadioButton rbAccurate;
        private System.Windows.Forms.RadioButton rbVague;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Panel panelPcbName;
        public System.Windows.Forms.Panel panelPcbNumber;
    }
}