namespace GeoEdit
{
    partial class AboutForm
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
            this.linkLabel_site = new System.Windows.Forms.LinkLabel();
            this.linkLabel_mail = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PEXEL v1.0";
            // 
            // linkLabel_site
            // 
            this.linkLabel_site.AutoSize = true;
            this.linkLabel_site.Location = new System.Drawing.Point(12, 41);
            this.linkLabel_site.Name = "linkLabel_site";
            this.linkLabel_site.Size = new System.Drawing.Size(44, 13);
            this.linkLabel_site.TabIndex = 1;
            this.linkLabel_site.TabStop = true;
            this.linkLabel_site.Text = "pexel.ru";
            this.linkLabel_site.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_site_LinkClicked);
            // 
            // linkLabel_mail
            // 
            this.linkLabel_mail.AutoSize = true;
            this.linkLabel_mail.Location = new System.Drawing.Point(74, 41);
            this.linkLabel_mail.Name = "linkLabel_mail";
            this.linkLabel_mail.Size = new System.Drawing.Size(135, 13);
            this.linkLabel_mail.TabIndex = 1;
            this.linkLabel_mail.TabStop = true;
            this.linkLabel_mail.Text = "aubakirov-artur@yandex.ru";
            this.linkLabel_mail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_mail_LinkClicked_1);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 72);
            this.Controls.Add(this.linkLabel_mail);
            this.Controls.Add(this.linkLabel_site);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel_site;
        private System.Windows.Forms.LinkLabel linkLabel_mail;
    }
}