﻿namespace power_aoi.DockerPanal
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
            this.btnNG = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbPcb = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvList = new System.Windows.Forms.ListView();
            this.colPcbId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIsBack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPcbPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPartImagePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colArea = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNgType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbPcbWidth = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSurfaceNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbPcbNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbPcbHeight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbPcbChildenNumber = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbPcb.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
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
            // btnNG
            // 
            this.btnNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNG.Location = new System.Drawing.Point(400, 3);
            this.btnNG.Name = "btnNG";
            this.btnNG.Size = new System.Drawing.Size(391, 74);
            this.btnNG.TabIndex = 1;
            this.btnNG.Text = "NG ← ↑ →";
            this.btnNG.UseVisualStyleBackColor = true;
            this.btnNG.Click += new System.EventHandler(this.btnNG_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(391, 74);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK ↓";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbPcb
            // 
            this.gbPcb.Controls.Add(this.panel2);
            this.gbPcb.Controls.Add(this.panel1);
            this.gbPcb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPcb.Location = new System.Drawing.Point(3, 3);
            this.gbPcb.Name = "gbPcb";
            this.gbPcb.Size = new System.Drawing.Size(794, 358);
            this.gbPcb.TabIndex = 2;
            this.gbPcb.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(148, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(643, 338);
            this.panel2.TabIndex = 2;
            // 
            // lvList
            // 
            this.lvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPcbId,
            this.colIsBack,
            this.colPcbPath,
            this.colPartImagePath,
            this.colId,
            this.colArea,
            this.colNgType,
            this.colResult});
            this.lvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvList.FullRowSelect = true;
            this.lvList.HideSelection = false;
            this.lvList.Location = new System.Drawing.Point(0, 0);
            this.lvList.Name = "lvList";
            this.lvList.Size = new System.Drawing.Size(643, 338);
            this.lvList.TabIndex = 0;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            this.lvList.SelectedIndexChanged += new System.EventHandler(this.lvList_SelectedIndexChanged);
            // 
            // colPcbId
            // 
            this.colPcbId.Width = 0;
            // 
            // colIsBack
            // 
            this.colIsBack.Width = 0;
            // 
            // colPcbPath
            // 
            this.colPcbPath.Width = 0;
            // 
            // colPartImagePath
            // 
            this.colPartImagePath.Width = 0;
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
            this.colResult.Text = "人工复验";
            this.colResult.Width = 200;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbPcbChildenNumber);
            this.panel1.Controls.Add(this.lbPcbHeight);
            this.panel1.Controls.Add(this.lbPcbWidth);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbSurfaceNumber);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbPcbNumber);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(145, 338);
            this.panel1.TabIndex = 1;
            // 
            // lbPcbWidth
            // 
            this.lbPcbWidth.AutoSize = true;
            this.lbPcbWidth.Location = new System.Drawing.Point(74, 63);
            this.lbPcbWidth.Name = "lbPcbWidth";
            this.lbPcbWidth.Size = new System.Drawing.Size(0, 12);
            this.lbPcbWidth.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "PCB宽：";
            // 
            // lbSurfaceNumber
            // 
            this.lbSurfaceNumber.AutoSize = true;
            this.lbSurfaceNumber.Location = new System.Drawing.Point(66, 35);
            this.lbSurfaceNumber.Name = "lbSurfaceNumber";
            this.lbSurfaceNumber.Size = new System.Drawing.Size(0, 12);
            this.lbSurfaceNumber.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "面数：";
            // 
            // lbPcbNumber
            // 
            this.lbPcbNumber.AutoSize = true;
            this.lbPcbNumber.Location = new System.Drawing.Point(66, 7);
            this.lbPcbNumber.Name = "lbPcbNumber";
            this.lbPcbNumber.Size = new System.Drawing.Size(0, 12);
            this.lbPcbNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "板号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "PCB长：";
            // 
            // lbPcbHeight
            // 
            this.lbPcbHeight.AutoSize = true;
            this.lbPcbHeight.Location = new System.Drawing.Point(74, 91);
            this.lbPcbHeight.Name = "lbPcbHeight";
            this.lbPcbHeight.Size = new System.Drawing.Size(0, 12);
            this.lbPcbHeight.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "子基板数：";
            // 
            // lbPcbChildenNumber
            // 
            this.lbPcbChildenNumber.AutoSize = true;
            this.lbPcbChildenNumber.Location = new System.Drawing.Point(83, 120);
            this.lbPcbChildenNumber.Name = "lbPcbChildenNumber";
            this.lbPcbChildenNumber.Size = new System.Drawing.Size(0, 12);
            this.lbPcbChildenNumber.TabIndex = 5;
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
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.ColumnHeader colArea;
        public System.Windows.Forms.ColumnHeader colNgType;
        public System.Windows.Forms.ColumnHeader colResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.ListView lvList;
        public System.Windows.Forms.Button btnNG;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.GroupBox gbPcb;
        public System.Windows.Forms.ColumnHeader colId;
        public System.Windows.Forms.ColumnHeader colPcbId;
        public System.Windows.Forms.ColumnHeader colIsBack;
        public System.Windows.Forms.ColumnHeader colPartImagePath;
        private System.Windows.Forms.ColumnHeader colPcbPath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lbPcbNumber;
        public System.Windows.Forms.Label lbSurfaceNumber;
        public System.Windows.Forms.Label lbPcbWidth;
        public System.Windows.Forms.Label lbPcbHeight;
        public System.Windows.Forms.Label lbPcbChildenNumber;
    }
}