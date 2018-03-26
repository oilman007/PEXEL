namespace GeoEdit
{
    partial class SelectLayersForm
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
            this.LayesListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // LayesListBox
            // 
            this.LayesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayesListBox.FormattingEnabled = true;
            this.LayesListBox.Location = new System.Drawing.Point(0, 0);
            this.LayesListBox.Name = "LayesListBox";
            this.LayesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LayesListBox.Size = new System.Drawing.Size(155, 529);
            this.LayesListBox.TabIndex = 0;
            // 
            // SelectLayersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(155, 529);
            this.Controls.Add(this.LayesListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SelectLayersForm";
            this.Text = "SelectLayersForm";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox LayesListBox;




    }
}