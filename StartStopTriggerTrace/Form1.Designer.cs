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
            this.label1 = new System.Windows.Forms.Label();
            this.cbEquipment = new System.Windows.Forms.ComboBox();
            this.lbDcps = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateDcp = new System.Windows.Forms.Button();
            this.btnEditDcp = new System.Windows.Forms.Button();
            this.btnDeleteDcp = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbLogs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Equipment:";
            // 
            // cbEquipment
            // 
            this.cbEquipment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEquipment.FormattingEnabled = true;
            this.cbEquipment.Location = new System.Drawing.Point(89, 12);
            this.cbEquipment.Name = "cbEquipment";
            this.cbEquipment.Size = new System.Drawing.Size(411, 21);
            this.cbEquipment.TabIndex = 1;
            // 
            // lbDcps
            // 
            this.lbDcps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDcps.FormattingEnabled = true;
            this.lbDcps.Location = new System.Drawing.Point(12, 68);
            this.lbDcps.Name = "lbDcps";
            this.lbDcps.Size = new System.Drawing.Size(488, 238);
            this.lbDcps.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start and Stop Trigger Trace DCPs:";
            // 
            // btnCreateDcp
            // 
            this.btnCreateDcp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateDcp.Location = new System.Drawing.Point(263, 312);
            this.btnCreateDcp.Name = "btnCreateDcp";
            this.btnCreateDcp.Size = new System.Drawing.Size(75, 23);
            this.btnCreateDcp.TabIndex = 4;
            this.btnCreateDcp.Text = "Create";
            this.btnCreateDcp.UseVisualStyleBackColor = true;
            this.btnCreateDcp.Click += new System.EventHandler(this.btnCreateDcp_Click);
            // 
            // btnEditDcp
            // 
            this.btnEditDcp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditDcp.Location = new System.Drawing.Point(344, 312);
            this.btnEditDcp.Name = "btnEditDcp";
            this.btnEditDcp.Size = new System.Drawing.Size(75, 23);
            this.btnEditDcp.TabIndex = 5;
            this.btnEditDcp.Text = "Edit";
            this.btnEditDcp.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDcp
            // 
            this.btnDeleteDcp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteDcp.Location = new System.Drawing.Point(425, 312);
            this.btnDeleteDcp.Name = "btnDeleteDcp";
            this.btnDeleteDcp.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteDcp.TabIndex = 6;
            this.btnDeleteDcp.Text = "Delete";
            this.btnDeleteDcp.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 348);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Logs:";
            // 
            // lbLogs
            // 
            this.lbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLogs.FormattingEnabled = true;
            this.lbLogs.Location = new System.Drawing.Point(12, 367);
            this.lbLogs.Name = "lbLogs";
            this.lbLogs.Size = new System.Drawing.Size(488, 186);
            this.lbLogs.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 579);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbLogs);
            this.Controls.Add(this.btnDeleteDcp);
            this.Controls.Add(this.btnEditDcp);
            this.Controls.Add(this.btnCreateDcp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbDcps);
            this.Controls.Add(this.cbEquipment);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbEquipment;
        private System.Windows.Forms.ListBox lbDcps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateDcp;
        private System.Windows.Forms.Button btnEditDcp;
        private System.Windows.Forms.Button btnDeleteDcp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbLogs;
    }
}

