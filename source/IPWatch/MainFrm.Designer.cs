namespace IPWatch
{
    partial class MainFrm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.txtIPList = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnIPSeach = new System.Windows.Forms.Button();
            this.btnWithHeadExp = new System.Windows.Forms.Button();
            this.btnNoHeadExp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCls = new System.Windows.Forms.Button();
            this.btnUpdateIPDB = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnLocalIP = new System.Windows.Forms.Button();
            this.btnIPSearchOnline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // txtIPList
            // 
            this.txtIPList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtIPList.Location = new System.Drawing.Point(117, 28);
            this.txtIPList.Multiline = true;
            this.txtIPList.Name = "txtIPList";
            this.txtIPList.Size = new System.Drawing.Size(128, 347);
            this.txtIPList.TabIndex = 0;
            this.txtIPList.Text = "127.0.0.1\r\n8.8.8.8\r\n192.168.1.1\r\n114.114.114.114\r\n";
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(251, 28);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(392, 347);
            this.txtResult.TabIndex = 1;
            // 
            // btnIPSeach
            // 
            this.btnIPSeach.Location = new System.Drawing.Point(13, 116);
            this.btnIPSeach.Name = "btnIPSeach";
            this.btnIPSeach.Size = new System.Drawing.Size(101, 23);
            this.btnIPSeach.TabIndex = 2;
            this.btnIPSeach.Text = "离线IP位置搜索";
            this.btnIPSeach.UseVisualStyleBackColor = true;
            this.btnIPSeach.Click += new System.EventHandler(this.btnIPSeach_Click);
            // 
            // btnWithHeadExp
            // 
            this.btnWithHeadExp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWithHeadExp.Location = new System.Drawing.Point(381, 2);
            this.btnWithHeadExp.Name = "btnWithHeadExp";
            this.btnWithHeadExp.Size = new System.Drawing.Size(128, 23);
            this.btnWithHeadExp.TabIndex = 3;
            this.btnWithHeadExp.Text = "带表头导出CSV";
            this.btnWithHeadExp.UseVisualStyleBackColor = true;
            this.btnWithHeadExp.Click += new System.EventHandler(this.btnWithHeadExp_Click);
            // 
            // btnNoHeadExp
            // 
            this.btnNoHeadExp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNoHeadExp.Location = new System.Drawing.Point(515, 2);
            this.btnNoHeadExp.Name = "btnNoHeadExp";
            this.btnNoHeadExp.Size = new System.Drawing.Size(128, 23);
            this.btnNoHeadExp.TabIndex = 4;
            this.btnNoHeadExp.Text = "不带表头导出CSV";
            this.btnNoHeadExp.UseVisualStyleBackColor = true;
            this.btnNoHeadExp.Click += new System.EventHandler(this.btnNoHeadExp_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "结果导出:";
            // 
            // btnCls
            // 
            this.btnCls.Location = new System.Drawing.Point(13, 28);
            this.btnCls.Name = "btnCls";
            this.btnCls.Size = new System.Drawing.Size(75, 23);
            this.btnCls.TabIndex = 6;
            this.btnCls.Text = "清空";
            this.btnCls.UseVisualStyleBackColor = true;
            this.btnCls.Click += new System.EventHandler(this.btnCls_Click);
            // 
            // btnUpdateIPDB
            // 
            this.btnUpdateIPDB.Location = new System.Drawing.Point(13, 87);
            this.btnUpdateIPDB.Name = "btnUpdateIPDB";
            this.btnUpdateIPDB.Size = new System.Drawing.Size(101, 23);
            this.btnUpdateIPDB.TabIndex = 7;
            this.btnUpdateIPDB.Text = "更新离线IP库";
            this.btnUpdateIPDB.UseVisualStyleBackColor = true;
            this.btnUpdateIPDB.Click += new System.EventHandler(this.btnUpdateIPDB_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 320);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbout.Location = new System.Drawing.Point(13, 349);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 9;
            this.btnAbout.Text = "关于";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnLocalIP
            // 
            this.btnLocalIP.Location = new System.Drawing.Point(13, 57);
            this.btnLocalIP.Name = "btnLocalIP";
            this.btnLocalIP.Size = new System.Drawing.Size(75, 23);
            this.btnLocalIP.TabIndex = 10;
            this.btnLocalIP.Text = "本机IP";
            this.btnLocalIP.UseVisualStyleBackColor = true;
            this.btnLocalIP.Click += new System.EventHandler(this.btnLocalIP_Click);
            // 
            // btnIPSearchOnline
            // 
            this.btnIPSearchOnline.Location = new System.Drawing.Point(13, 210);
            this.btnIPSearchOnline.Name = "btnIPSearchOnline";
            this.btnIPSearchOnline.Size = new System.Drawing.Size(100, 23);
            this.btnIPSearchOnline.TabIndex = 11;
            this.btnIPSearchOnline.Text = "在线IP位置搜索";
            this.btnIPSearchOnline.UseVisualStyleBackColor = true;
            this.btnIPSearchOnline.Click += new System.EventHandler(this.btnIPSearchOnline_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 387);
            this.Controls.Add(this.btnIPSearchOnline);
            this.Controls.Add(this.btnLocalIP);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdateIPDB);
            this.Controls.Add(this.btnCls);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNoHeadExp);
            this.Controls.Add(this.btnWithHeadExp);
            this.Controls.Add(this.btnIPSeach);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtIPList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrm";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.MainFrm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox txtIPList;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnIPSeach;
        private System.Windows.Forms.Button btnWithHeadExp;
        private System.Windows.Forms.Button btnNoHeadExp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCls;
        private System.Windows.Forms.Button btnUpdateIPDB;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnLocalIP;
        private System.Windows.Forms.Button btnIPSearchOnline;
    }
}

