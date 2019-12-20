namespace power_aoi.DockerPanal
{
    partial class PcbDetails
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnNG = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbPcb = new System.Windows.Forms.GroupBox();
            this.lvList = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colArea = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNgType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPcbId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIsFront = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPartImagePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbPcb.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbPcb, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.88889F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.button4, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnNG, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 367);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 80);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Location = new System.Drawing.Point(597, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(194, 74);
            this.button4.TabIndex = 3;
            this.button4.Tag = " ";
            this.button4.Text = "查 询";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(399, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 74);
            this.button3.TabIndex = 2;
            this.button3.Text = "统 计";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnNG
            // 
            this.btnNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNG.Location = new System.Drawing.Point(201, 3);
            this.btnNG.Name = "btnNG";
            this.btnNG.Size = new System.Drawing.Size(192, 74);
            this.btnNG.TabIndex = 1;
            this.btnNG.Text = "NG ← ↑ →";
            this.btnNG.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(192, 74);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK ↓";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbPcb
            // 
            this.gbPcb.Controls.Add(this.lvList);
            this.gbPcb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPcb.Location = new System.Drawing.Point(3, 3);
            this.gbPcb.Name = "gbPcb";
            this.gbPcb.Size = new System.Drawing.Size(794, 358);
            this.gbPcb.TabIndex = 2;
            this.gbPcb.TabStop = false;
            this.gbPcb.Text = "板号: ";
            // 
            // lvList
            // 
            this.lvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPcbId,
            this.colIsFront,
            this.colPartImagePath,
            this.colId,
            this.colArea,
            this.colNgType,
            this.colResult});
            this.lvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvList.FullRowSelect = true;
            this.lvList.HideSelection = false;
            this.lvList.Location = new System.Drawing.Point(3, 17);
            this.lvList.Name = "lvList";
            this.lvList.Size = new System.Drawing.Size(788, 338);
            this.lvList.TabIndex = 0;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            this.lvList.SelectedIndexChanged += new System.EventHandler(this.lvList_SelectedIndexChanged);
            // 
            // colId
            // 
            this.colId.Tag = "Id";
            this.colId.Text = "编号";
            this.colId.Width = 150;
            // 
            // colArea
            // 
            this.colArea.Text = "区域";
            this.colArea.Width = 200;
            // 
            // colNgType
            // 
            this.colNgType.Text = "NG种类";
            this.colNgType.Width = 200;
            // 
            // colResult
            // 
            this.colResult.Text = "检验结果";
            this.colResult.Width = 200;
            // 
            // colPcbId
            // 
            this.colPcbId.Width = 0;
            // 
            // colIsFront
            // 
            this.colIsFront.Width = 0;
            // 
            // colPartImagePath
            // 
            this.colPartImagePath.Width = 0;
            // 
            // PcbDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PcbDetails";
            this.Text = "PcbDetails";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.gbPcb.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.ColumnHeader colArea;
        public System.Windows.Forms.ColumnHeader colNgType;
        public System.Windows.Forms.ColumnHeader colResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.ListView lvList;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button btnNG;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.GroupBox gbPcb;
        public System.Windows.Forms.ColumnHeader colId;
        public System.Windows.Forms.ColumnHeader colPcbId;
        public System.Windows.Forms.ColumnHeader colIsFront;
        public System.Windows.Forms.ColumnHeader colPartImagePath;
    }
}