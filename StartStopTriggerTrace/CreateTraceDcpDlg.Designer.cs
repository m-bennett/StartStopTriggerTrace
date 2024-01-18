namespace StartStopTriggerTrace
{
    partial class CreateTraceDcpDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTraceDescription = new System.Windows.Forms.TextBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbParameters = new System.Windows.Forms.ListBox();
            this.lbStartTriggers = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbStopTriggers = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnDeleteStartTrigger = new System.Windows.Forms.Button();
            this.btnCreateStartTrigger = new System.Windows.Forms.Button();
            this.btnDeleteStopTrigger = new System.Windows.Forms.Button();
            this.btnCreateStopTrigger = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPeriod = new System.Windows.Forms.TextBox();
            this.tbSamples = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbGroupSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCreateDcp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trace Description:";
            // 
            // txtTraceDescription
            // 
            this.txtTraceDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTraceDescription.Location = new System.Drawing.Point(111, 12);
            this.txtTraceDescription.Name = "txtTraceDescription";
            this.txtTraceDescription.Size = new System.Drawing.Size(411, 20);
            this.txtTraceDescription.TabIndex = 1;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(111, 38);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(411, 20);
            this.txtFilter.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filter:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Parameters:";
            // 
            // lbParameters
            // 
            this.lbParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbParameters.FormattingEnabled = true;
            this.lbParameters.Location = new System.Drawing.Point(14, 92);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.Size = new System.Drawing.Size(508, 186);
            this.lbParameters.TabIndex = 5;
            // 
            // lbStartTriggers
            // 
            this.lbStartTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStartTriggers.FormattingEnabled = true;
            this.lbStartTriggers.Location = new System.Drawing.Point(2, 19);
            this.lbStartTriggers.Name = "lbStartTriggers";
            this.lbStartTriggers.Size = new System.Drawing.Size(249, 121);
            this.lbStartTriggers.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Start Triggers:";
            // 
            // lbStopTriggers
            // 
            this.lbStopTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStopTriggers.FormattingEnabled = true;
            this.lbStopTriggers.Location = new System.Drawing.Point(4, 19);
            this.lbStopTriggers.Name = "lbStopTriggers";
            this.lbStopTriggers.Size = new System.Drawing.Size(246, 121);
            this.lbStopTriggers.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Stop Triggers:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(11, 288);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteStartTrigger);
            this.splitContainer1.Panel1.Controls.Add(this.btnCreateStartTrigger);
            this.splitContainer1.Panel1.Controls.Add(this.lbStartTriggers);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnDeleteStopTrigger);
            this.splitContainer1.Panel2.Controls.Add(this.btnCreateStopTrigger);
            this.splitContainer1.Panel2.Controls.Add(this.lbStopTriggers);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Size = new System.Drawing.Size(512, 174);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 10;
            // 
            // btnDeleteStartTrigger
            // 
            this.btnDeleteStartTrigger.Location = new System.Drawing.Point(126, 146);
            this.btnDeleteStartTrigger.Name = "btnDeleteStartTrigger";
            this.btnDeleteStartTrigger.Size = new System.Drawing.Size(114, 23);
            this.btnDeleteStartTrigger.TabIndex = 9;
            this.btnDeleteStartTrigger.Text = "Delete Start Trigger";
            this.btnDeleteStartTrigger.UseVisualStyleBackColor = true;
            this.btnDeleteStartTrigger.Click += new System.EventHandler(this.btnDeleteStartTrigger_Click);
            // 
            // btnCreateStartTrigger
            // 
            this.btnCreateStartTrigger.Location = new System.Drawing.Point(6, 147);
            this.btnCreateStartTrigger.Name = "btnCreateStartTrigger";
            this.btnCreateStartTrigger.Size = new System.Drawing.Size(114, 23);
            this.btnCreateStartTrigger.TabIndex = 8;
            this.btnCreateStartTrigger.Text = "Create Start Trigger";
            this.btnCreateStartTrigger.UseVisualStyleBackColor = true;
            this.btnCreateStartTrigger.Click += new System.EventHandler(this.btnCreateStartTrigger_Click);
            // 
            // btnDeleteStopTrigger
            // 
            this.btnDeleteStopTrigger.Location = new System.Drawing.Point(124, 145);
            this.btnDeleteStopTrigger.Name = "btnDeleteStopTrigger";
            this.btnDeleteStopTrigger.Size = new System.Drawing.Size(114, 23);
            this.btnDeleteStopTrigger.TabIndex = 11;
            this.btnDeleteStopTrigger.Text = "Delete Stop Trigger";
            this.btnDeleteStopTrigger.UseVisualStyleBackColor = true;
            // 
            // btnCreateStopTrigger
            // 
            this.btnCreateStopTrigger.Location = new System.Drawing.Point(4, 146);
            this.btnCreateStopTrigger.Name = "btnCreateStopTrigger";
            this.btnCreateStopTrigger.Size = new System.Drawing.Size(114, 23);
            this.btnCreateStopTrigger.TabIndex = 10;
            this.btnCreateStopTrigger.Text = "Create Stop Trigger";
            this.btnCreateStopTrigger.UseVisualStyleBackColor = true;
            this.btnCreateStopTrigger.Click += new System.EventHandler(this.btnCreateStopTrigger_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 475);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Period (ms):";
            // 
            // tbPeriod
            // 
            this.tbPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbPeriod.Location = new System.Drawing.Point(76, 472);
            this.tbPeriod.Name = "tbPeriod";
            this.tbPeriod.Size = new System.Drawing.Size(71, 20);
            this.tbPeriod.TabIndex = 12;
            this.tbPeriod.Text = "5000";
            // 
            // tbSamples
            // 
            this.tbSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSamples.Location = new System.Drawing.Point(228, 472);
            this.tbSamples.Name = "tbSamples";
            this.tbSamples.Size = new System.Drawing.Size(71, 20);
            this.tbSamples.TabIndex = 14;
            this.tbSamples.Text = "0";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(172, 475);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Samples:";
            // 
            // tbGroupSize
            // 
            this.tbGroupSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbGroupSize.Location = new System.Drawing.Point(393, 472);
            this.tbGroupSize.Name = "tbGroupSize";
            this.tbGroupSize.Size = new System.Drawing.Size(71, 20);
            this.tbGroupSize.TabIndex = 16;
            this.tbGroupSize.Text = "1";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(325, 475);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Group Size:";
            // 
            // btnCreateDcp
            // 
            this.btnCreateDcp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateDcp.Location = new System.Drawing.Point(366, 506);
            this.btnCreateDcp.Name = "btnCreateDcp";
            this.btnCreateDcp.Size = new System.Drawing.Size(75, 23);
            this.btnCreateDcp.TabIndex = 17;
            this.btnCreateDcp.Text = "Create DCP";
            this.btnCreateDcp.UseVisualStyleBackColor = true;
            this.btnCreateDcp.Click += new System.EventHandler(this.btnCreateDcp_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(447, 506);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CreateTraceDcpDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 541);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateDcp);
            this.Controls.Add(this.tbGroupSize);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbSamples);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbPeriod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lbParameters);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTraceDescription);
            this.Controls.Add(this.label1);
            this.Name = "CreateTraceDcpDlg";
            this.Text = "CreateTraceDlg";
            this.Load += new System.EventHandler(this.CreateTraceDlg_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTraceDescription;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbParameters;
        private System.Windows.Forms.ListBox lbStartTriggers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbStopTriggers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnDeleteStartTrigger;
        private System.Windows.Forms.Button btnCreateStartTrigger;
        private System.Windows.Forms.Button btnDeleteStopTrigger;
        private System.Windows.Forms.Button btnCreateStopTrigger;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPeriod;
        private System.Windows.Forms.TextBox tbSamples;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbGroupSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCreateDcp;
        private System.Windows.Forms.Button btnCancel;
    }
}