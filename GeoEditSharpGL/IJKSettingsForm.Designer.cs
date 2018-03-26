namespace GeoEdit
{
    partial class IJKSettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.i0RadioButton = new System.Windows.Forms.RadioButton();
            this.iMaxRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.jMaxRadioButton = new System.Windows.Forms.RadioButton();
            this.j0RadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.kMaxRadioButton = new System.Windows.Forms.RadioButton();
            this.k0RadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.iMaxRadioButton);
            this.groupBox1.Controls.Add(this.i0RadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "I-direction";
            // 
            // i0RadioButton
            // 
            this.i0RadioButton.AutoSize = true;
            this.i0RadioButton.Location = new System.Drawing.Point(6, 19);
            this.i0RadioButton.Name = "i0RadioButton";
            this.i0RadioButton.Size = new System.Drawing.Size(40, 17);
            this.i0RadioButton.TabIndex = 2;
            this.i0RadioButton.TabStop = true;
            this.i0RadioButton.Text = "I=0";
            this.i0RadioButton.UseVisualStyleBackColor = true;
            this.i0RadioButton.CheckedChanged += new System.EventHandler(this.i0RadioButton_CheckedChanged);
            // 
            // iMaxRadioButton
            // 
            this.iMaxRadioButton.AutoSize = true;
            this.iMaxRadioButton.Location = new System.Drawing.Point(88, 19);
            this.iMaxRadioButton.Name = "iMaxRadioButton";
            this.iMaxRadioButton.Size = new System.Drawing.Size(53, 17);
            this.iMaxRadioButton.TabIndex = 3;
            this.iMaxRadioButton.TabStop = true;
            this.iMaxRadioButton.Text = "I=max";
            this.iMaxRadioButton.UseVisualStyleBackColor = true;
            this.iMaxRadioButton.CheckedChanged += new System.EventHandler(this.iMaxRadioButton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.jMaxRadioButton);
            this.groupBox2.Controls.Add(this.j0RadioButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 45);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "J-direction";
            // 
            // jMaxRadioButton
            // 
            this.jMaxRadioButton.AutoSize = true;
            this.jMaxRadioButton.Location = new System.Drawing.Point(88, 19);
            this.jMaxRadioButton.Name = "jMaxRadioButton";
            this.jMaxRadioButton.Size = new System.Drawing.Size(55, 17);
            this.jMaxRadioButton.TabIndex = 3;
            this.jMaxRadioButton.TabStop = true;
            this.jMaxRadioButton.Text = "J=max";
            this.jMaxRadioButton.UseVisualStyleBackColor = true;
            this.jMaxRadioButton.CheckedChanged += new System.EventHandler(this.jMaxRadioButton_CheckedChanged);
            // 
            // j0RadioButton
            // 
            this.j0RadioButton.AutoSize = true;
            this.j0RadioButton.Location = new System.Drawing.Point(6, 19);
            this.j0RadioButton.Name = "j0RadioButton";
            this.j0RadioButton.Size = new System.Drawing.Size(42, 17);
            this.j0RadioButton.TabIndex = 2;
            this.j0RadioButton.TabStop = true;
            this.j0RadioButton.Text = "J=0";
            this.j0RadioButton.UseVisualStyleBackColor = true;
            this.j0RadioButton.CheckedChanged += new System.EventHandler(this.j0RadioButton_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.kMaxRadioButton);
            this.groupBox3.Controls.Add(this.k0RadioButton);
            this.groupBox3.Location = new System.Drawing.Point(12, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(147, 45);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "K-direction";
            // 
            // kMaxRadioButton
            // 
            this.kMaxRadioButton.AutoSize = true;
            this.kMaxRadioButton.Location = new System.Drawing.Point(88, 19);
            this.kMaxRadioButton.Name = "kMaxRadioButton";
            this.kMaxRadioButton.Size = new System.Drawing.Size(57, 17);
            this.kMaxRadioButton.TabIndex = 3;
            this.kMaxRadioButton.TabStop = true;
            this.kMaxRadioButton.Text = "K=max";
            this.kMaxRadioButton.UseVisualStyleBackColor = true;
            this.kMaxRadioButton.CheckedChanged += new System.EventHandler(this.kMaxRadioButton_CheckedChanged);
            // 
            // k0RadioButton
            // 
            this.k0RadioButton.AutoSize = true;
            this.k0RadioButton.Location = new System.Drawing.Point(6, 19);
            this.k0RadioButton.Name = "k0RadioButton";
            this.k0RadioButton.Size = new System.Drawing.Size(44, 17);
            this.k0RadioButton.TabIndex = 2;
            this.k0RadioButton.TabStop = true;
            this.k0RadioButton.Text = "K=0";
            this.k0RadioButton.UseVisualStyleBackColor = true;
            this.k0RadioButton.CheckedChanged += new System.EventHandler(this.k0RadioButton_CheckedChanged);
            // 
            // IJKSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 176);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "IJKSettingsForm";
            this.Text = "IJK Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IJKSettingsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton iMaxRadioButton;
        private System.Windows.Forms.RadioButton i0RadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton jMaxRadioButton;
        private System.Windows.Forms.RadioButton j0RadioButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton kMaxRadioButton;
        private System.Windows.Forms.RadioButton k0RadioButton;
    }
}