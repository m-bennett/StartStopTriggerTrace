namespace StartStopTriggerTrace
{
    partial class Form1
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
            this.lbDcps = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateTraceDcp = new System.Windows.Forms.Button();
            this.btnEditDcp = new System.Windows.Forms.Button();
            this.btnDeleteDcp = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbDcps
            // 
            this.lbDcps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDcps.FormattingEnabled = true;
            this.lbDcps.HorizontalScrollbar = true;
            this.lbDcps.ItemHeight = 16;
            this.lbDcps.Location = new System.Drawing.Point(16, 36);
            this.lbDcps.Margin = new System.Windows.Forms.Padding(4);
            this.lbDcps.Name = "lbDcps";
            this.lbDcps.Size = new System.Drawing.Size(649, 340);
            this.lbDcps.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start and Stop Trigger Trace DCPs:";
            // 
            // btnCreateTraceDcp
            // 
            this.btnCreateTraceDcp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateTraceDcp.Location = new System.Drawing.Point(351, 384);
            this.btnCreateTraceDcp.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateTraceDcp.Name = "btnCreateTraceDcp";
            this.btnCreateTraceDcp.Size = new System.Drawing.Size(100, 28);
            this.btnCreateTraceDcp.TabIndex = 4;
            this.btnCreateTraceDcp.Text = "Create";
            this.btnCreateTraceDcp.UseVisualStyleBackColor = true;
            this.btnCreateTraceDcp.Click += new System.EventHandler(this.btnCreateTraceDcp_Click);
            // 
            // btnEditDcp
            // 
            this.btnEditDcp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditDcp.Location = new System.Drawing.Point(459, 384);
            this.btnEditDcp.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditDcp.Name = "btnEditDcp";
            this.btnEditDcp.Size = new System.Drawing.Size(100, 28);
            this.btnEditDcp.TabIndex = 5;
            this.btnEditDcp.Text = "Edit";
            this.btnEditDcp.UseVisualStyleBackColor = true;
            this.btnEditDcp.Click += new System.EventHandler(this.btnEditDcp_Click);
            // 
            // btnDeleteDcp
            // 
            this.btnDeleteDcp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteDcp.Location = new System.Drawing.Point(567, 384);
            this.btnDeleteDcp.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteDcp.Name = "btnDeleteDcp";
            this.btnDeleteDcp.Size = new System.Drawing.Size(100, 28);
            this.btnDeleteDcp.TabIndex = 6;
            this.btnDeleteDcp.Text = "Delete";
            this.btnDeleteDcp.UseVisualStyleBackColor = true;
            this.btnDeleteDcp.Click += new System.EventHandler(this.btnDeleteDcp_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 428);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Logs:";
            // 
            // tbLogs
            // 
            this.tbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogs.Location = new System.Drawing.Point(16, 448);
            this.tbLogs.Margin = new System.Windows.Forms.Padding(4);
            this.tbLogs.Multiline = true;
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLogs.Size = new System.Drawing.Size(649, 218);
            this.tbLogs.TabIndex = 9;
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLogs.Location = new System.Drawing.Point(565, 674);
            this.btnClearLogs.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(100, 28);
            this.btnClearLogs.TabIndex = 10;
            this.btnClearLogs.Text = "Clear";
            this.btnClearLogs.UseVisualStyleBackColor = true;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 713);
            this.Controls.Add(this.btnClearLogs);
            this.Controls.Add(this.tbLogs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDeleteDcp);
            this.Controls.Add(this.btnEditDcp);
            this.Controls.Add(this.btnCreateTraceDcp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbDcps);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "GEM Start and Stop Triggers App for Sapience";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lbDcps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateTraceDcp;
        private System.Windows.Forms.Button btnEditDcp;
        private System.Windows.Forms.Button btnDeleteDcp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLogs;
        private System.Windows.Forms.Button btnClearLogs;
    }
}

