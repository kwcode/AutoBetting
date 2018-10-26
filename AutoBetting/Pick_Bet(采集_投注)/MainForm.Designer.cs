namespace Ku.Forms
{
    partial class MainForm
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
            this.txtOutputMsg = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnokTime = new System.Windows.Forms.Button();
            this.lbCountdown = new System.Windows.Forms.Label();
            this.lbissueNo = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutputMsg
            // 
            this.txtOutputMsg.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtOutputMsg.Location = new System.Drawing.Point(344, 110);
            this.txtOutputMsg.Multiline = true;
            this.txtOutputMsg.Name = "txtOutputMsg";
            this.txtOutputMsg.ReadOnly = true;
            this.txtOutputMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutputMsg.Size = new System.Drawing.Size(348, 241);
            this.txtOutputMsg.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnokTime);
            this.panel1.Controls.Add(this.lbCountdown);
            this.panel1.Controls.Add(this.lbissueNo);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 107);
            this.panel1.TabIndex = 0;
            // 
            // btnokTime
            // 
            this.btnokTime.Location = new System.Drawing.Point(141, 54);
            this.btnokTime.Name = "btnokTime";
            this.btnokTime.Size = new System.Drawing.Size(75, 23);
            this.btnokTime.TabIndex = 4;
            this.btnokTime.Text = "校对时间";
            this.btnokTime.UseVisualStyleBackColor = true;
            this.btnokTime.Click += new System.EventHandler(this.btnokTime_Click);
            // 
            // lbCountdown
            // 
            this.lbCountdown.AutoSize = true;
            this.lbCountdown.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCountdown.ForeColor = System.Drawing.Color.Red;
            this.lbCountdown.Location = new System.Drawing.Point(31, 54);
            this.lbCountdown.Name = "lbCountdown";
            this.lbCountdown.Size = new System.Drawing.Size(89, 19);
            this.lbCountdown.TabIndex = 3;
            this.lbCountdown.Text = "00:00:03";
            // 
            // lbissueNo
            // 
            this.lbissueNo.AutoSize = true;
            this.lbissueNo.Location = new System.Drawing.Point(5, 23);
            this.lbissueNo.Name = "lbissueNo";
            this.lbissueNo.Size = new System.Drawing.Size(119, 12);
            this.lbissueNo.TabIndex = 2;
            this.lbissueNo.Text = "距N期投注截止还有：";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(595, 55);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(91, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "结束服务";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(593, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(91, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开启服务";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(703, 380);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtOutputMsg);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(695, 354);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Service";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 380);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "任务调度查看器";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutputMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lbCountdown;
        private System.Windows.Forms.Label lbissueNo;
        private System.Windows.Forms.Button btnokTime;
    }
}