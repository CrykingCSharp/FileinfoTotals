namespace FileinfoTotals
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
            this.SuspendLayout();
            // 
            // lbVolumns
            // 
            this.lbVolumns.FormattingEnabled = true;
            this.lbVolumns.ItemHeight = 12;
            this.lbVolumns.Location = new System.Drawing.Point(116, 12);
            this.lbVolumns.Name = "lbVolumns";
            this.lbVolumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbVolumns.Size = new System.Drawing.Size(144, 124);
            this.lbVolumns.TabIndex = 0;
            // 
            // btnAnaly
            // 
            this.btnAnaly.Location = new System.Drawing.Point(376, 58);
            this.btnAnaly.Name = "btnAnaly";
            this.btnAnaly.Size = new System.Drawing.Size(75, 23);
            this.btnAnaly.TabIndex = 1;
            this.btnAnaly.Text = "分析";
            this.btnAnaly.UseVisualStyleBackColor = true;
            this.btnAnaly.Click += new System.EventHandler(this.btnAnaly_Click);
            // 
            // rtbOut
            // 
            this.rtbOut.Location = new System.Drawing.Point(116, 159);
            this.rtbOut.Name = "rtbOut";
            this.rtbOut.ReadOnly = true;
            this.rtbOut.Size = new System.Drawing.Size(355, 238);
            this.rtbOut.TabIndex = 2;
            this.rtbOut.Text = "";
            // 
            // FormFileinfoTotals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}

