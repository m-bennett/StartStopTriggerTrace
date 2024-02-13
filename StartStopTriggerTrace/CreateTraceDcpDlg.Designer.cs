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
            this.btnCreateDcp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbEquipment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKafkaTopic = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trace Description:";
            // 
            // txtTraceDescription
            // 
            this.txtTraceDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTraceDescription.Location = new System.Drawing.Point(148, 51);
            this.txtTraceDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtTraceDescription.Name = "txtTraceDescription";
            this.txtTraceDescription.Size = new System.Drawing.Size(574, 22);
            this.txtTraceDescription.TabIndex = 1;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(148, 83);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(471, 22);
            this.txtFilter.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filter Parameters:";
            // 
            // lbParameters
            // 
            this.lbParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbParameters.FormattingEnabled = true;
            this.lbParameters.HorizontalScrollbar = true;
            this.lbParameters.ItemHeight = 16;
            this.lbParameters.Location = new System.Drawing.Point(13, 120);
            this.lbParameters.Margin = new System.Windows.Forms.Padding(4);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbParameters.Size = new System.Drawing.Size(709, 244);
            this.lbParameters.TabIndex = 5;
            this.lbParameters.SelectedIndexChanged += new System.EventHandler(this.lbParameters_SelectedIndexChanged);
            // 
            // lbStartTriggers
            // 
            this.lbStartTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStartTriggers.FormattingEnabled = true;
            this.lbStartTriggers.HorizontalScrollbar = true;
            this.lbStartTriggers.ItemHeight = 16;
            this.lbStartTriggers.Location = new System.Drawing.Point(4, 28);
            this.lbStartTriggers.Margin = new System.Windows.Forms.Padding(4);
            this.lbStartTriggers.Name = "lbStartTriggers";
            this.lbStartTriggers.Size = new System.Drawing.Size(341, 164);
            this.lbStartTriggers.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Start Triggers:";
            // 
            // lbStopTriggers
            // 
            this.lbStopTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStopTriggers.FormattingEnabled = true;
            this.lbStopTriggers.HorizontalScrollbar = true;
            this.lbStopTriggers.ItemHeight = 16;
            this.lbStopTriggers.Location = new System.Drawing.Point(5, 28);
            this.lbStopTriggers.Margin = new System.Windows.Forms.Padding(4);
            this.lbStopTriggers.Name = "lbStopTriggers";
            this.lbStopTriggers.Size = new System.Drawing.Size(346, 164);
            this.lbStopTriggers.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Stop Triggers:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(13, 380);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
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
            this.splitContainer1.Size = new System.Drawing.Size(709, 242);
            this.splitContainer1.SplitterDistance = 349;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 10;
            // 
            // btnDeleteStartTrigger
            // 
            this.btnDeleteStartTrigger.Location = new System.Drawing.Point(165, 198);
            this.btnDeleteStartTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteStartTrigger.Name = "btnDeleteStartTrigger";
            this.btnDeleteStartTrigger.Size = new System.Drawing.Size(152, 28);
            this.btnDeleteStartTrigger.TabIndex = 9;
            this.btnDeleteStartTrigger.Text = "Delete Start Trigger";
            this.btnDeleteStartTrigger.UseVisualStyleBackColor = true;
            this.btnDeleteStartTrigger.Click += new System.EventHandler(this.btnDeleteStartTrigger_Click);
            // 
            // btnCreateStartTrigger
            // 
            this.btnCreateStartTrigger.Location = new System.Drawing.Point(5, 198);
            this.btnCreateStartTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateStartTrigger.Name = "btnCreateStartTrigger";
            this.btnCreateStartTrigger.Size = new System.Drawing.Size(152, 28);
            this.btnCreateStartTrigger.TabIndex = 8;
            this.btnCreateStartTrigger.Text = "Create Start Trigger";
            this.btnCreateStartTrigger.UseVisualStyleBackColor = true;
            this.btnCreateStartTrigger.Click += new System.EventHandler(this.btnCreateStartTrigger_Click);
            // 
            // btnDeleteStopTrigger
            // 
            this.btnDeleteStopTrigger.Location = new System.Drawing.Point(164, 199);
            this.btnDeleteStopTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteStopTrigger.Name = "btnDeleteStopTrigger";
            this.btnDeleteStopTrigger.Size = new System.Drawing.Size(152, 28);
            this.btnDeleteStopTrigger.TabIndex = 11;
            this.btnDeleteStopTrigger.Text = "Delete Stop Trigger";
            this.btnDeleteStopTrigger.UseVisualStyleBackColor = true;
            this.btnDeleteStopTrigger.Click += new System.EventHandler(this.btnDeleteStopTrigger_Click);
            // 
            // btnCreateStopTrigger
            // 
            this.btnCreateStopTrigger.Location = new System.Drawing.Point(4, 198);
            this.btnCreateStopTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateStopTrigger.Name = "btnCreateStopTrigger";
            this.btnCreateStopTrigger.Size = new System.Drawing.Size(152, 28);
            this.btnCreateStopTrigger.TabIndex = 10;
            this.btnCreateStopTrigger.Text = "Create Stop Trigger";
            this.btnCreateStopTrigger.UseVisualStyleBackColor = true;
            this.btnCreateStopTrigger.Click += new System.EventHandler(this.btnCreateStopTrigger_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 636);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Period (ms):";
            // 
            // tbPeriod
            // 
            this.tbPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbPeriod.Location = new System.Drawing.Point(115, 635);
            this.tbPeriod.Margin = new System.Windows.Forms.Padding(4);
            this.tbPeriod.Name = "tbPeriod";
            this.tbPeriod.Size = new System.Drawing.Size(93, 22);
            this.tbPeriod.TabIndex = 12;
            this.tbPeriod.Text = "5000";
            // 
            // btnCreateDcp
            // 
            this.btnCreateDcp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateDcp.Location = new System.Drawing.Point(502, 681);
            this.btnCreateDcp.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateDcp.Name = "btnCreateDcp";
            this.btnCreateDcp.Size = new System.Drawing.Size(100, 28);
            this.btnCreateDcp.TabIndex = 17;
            this.btnCreateDcp.Text = "Create DCP";
            this.btnCreateDcp.UseVisualStyleBackColor = true;
            this.btnCreateDcp.Click += new System.EventHandler(this.btnCreateDcp_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(610, 681);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbEquipment
            // 
            this.cbEquipment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEquipment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEquipment.FormattingEnabled = true;
            this.cbEquipment.Location = new System.Drawing.Point(148, 19);
            this.cbEquipment.Margin = new System.Windows.Forms.Padding(4);
            this.cbEquipment.Name = "cbEquipment";
            this.cbEquipment.Size = new System.Drawing.Size(574, 24);
            this.cbEquipment.TabIndex = 22;
            this.cbEquipment.SelectedIndexChanged += new System.EventHandler(this.cbEquipment_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 21;
            this.label3.Text = "Equipment:";
            // 
            // txtKafkaTopic
            // 
            this.txtKafkaTopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKafkaTopic.Location = new System.Drawing.Point(322, 635);
            this.txtKafkaTopic.Margin = new System.Windows.Forms.Padding(4);
            this.txtKafkaTopic.Name = "txtKafkaTopic";
            this.txtKafkaTopic.Size = new System.Drawing.Size(400, 22);
            this.txtKafkaTopic.TabIndex = 24;
            this.txtKafkaTopic.Text = "CIM-EQUIPMENT-DATA-COLLECTION-REPORTS";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 638);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Kafka Topic:";
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyFilter.Location = new System.Drawing.Point(626, 81);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(96, 26);
            this.btnApplyFilter.TabIndex = 25;
            this.btnApplyFilter.Text = "Apply Filter";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // CreateTraceDcpDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 722);
            this.Controls.Add(this.btnApplyFilter);
            this.Controls.Add(this.txtKafkaTopic);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbEquipment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateDcp);
            this.Controls.Add(this.tbPeriod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lbParameters);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTraceDescription);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Button btnCreateDcp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbEquipment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKafkaTopic;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnApplyFilter;
    }
}