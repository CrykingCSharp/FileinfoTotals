﻿namespace FileinfoTotals
{
    partial class FormFileinfoTotals
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
            this.lbVolumns = new System.Windows.Forms.ListBox();
            this.btnAnaly = new System.Windows.Forms.Button();
            this.rtbOut = new System.Windows.Forms.RichTextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbVolumns
            // 
            this.lbVolumns.FormattingEnabled = true;
            this.lbVolumns.ItemHeight = 12;
            this.lbVolumns.Location = new System.Drawing.Point(27, 12);
            this.lbVolumns.Name = "lbVolumns";
            this.lbVolumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbVolumns.Size = new System.Drawing.Size(144, 124);
            this.lbVolumns.TabIndex = 0;
            // 
            // btnAnaly
            // 
            this.btnAnaly.Location = new System.Drawing.Point(211, 45);
            this.btnAnaly.Name = "btnAnaly";
            this.btnAnaly.Size = new System.Drawing.Size(129, 40);
            this.btnAnaly.TabIndex = 1;
            this.btnAnaly.Text = "分析";
            this.btnAnaly.UseVisualStyleBackColor = true;
            this.btnAnaly.Click += new System.EventHandler(this.btnAnaly_Click);
            // 
            // rtbOut
            // 
            this.rtbOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOut.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rtbOut.Location = new System.Drawing.Point(27, 142);
            this.rtbOut.Name = "rtbOut";
            this.rtbOut.ReadOnly = true;
            this.rtbOut.Size = new System.Drawing.Size(502, 296);
            this.rtbOut.TabIndex = 2;
            this.rtbOut.Text = "";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(400, 104);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(129, 32);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导出分析结果";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // FormFileinfoTotals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 450);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rtbOut);
            this.Controls.Add(this.btnAnaly);
            this.Controls.Add(this.lbVolumns);
            this.Name = "FormFileinfoTotals";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电脑文件分析工具";
            this.Load += new System.EventHandler(this.FormFileinfoTotals_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbVolumns;
        private System.Windows.Forms.Button btnAnaly;
        private System.Windows.Forms.RichTextBox rtbOut;
        private System.Windows.Forms.Button btnExport;
    }
}

