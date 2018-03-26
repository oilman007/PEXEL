namespace GeoEdit
{
    partial class ModifierForm
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
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.valueLabel = new System.Windows.Forms.Label();
            this.textBox_radius = new System.Windows.Forms.TextBox();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.textBox_title = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_value
            // 
            this.textBox_value.Location = new System.Drawing.Point(58, 58);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(110, 20);
            this.textBox_value.TabIndex = 3;
            this.textBox_value.Text = "1";
            this.textBox_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_value.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_value_Validating);
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(9, 61);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(48, 13);
            this.valueLabel.TabIndex = 5;
            this.valueLabel.Text = "Multiplier";
            // 
            // textBox_radius
            // 
            this.textBox_radius.Location = new System.Drawing.Point(58, 32);
            this.textBox_radius.Name = "textBox_radius";
            this.textBox_radius.Size = new System.Drawing.Size(110, 20);
            this.textBox_radius.TabIndex = 2;
            this.textBox_radius.Text = "0";
            this.textBox_radius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_radius.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_radius_Validating);
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(9, 9);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(27, 13);
            this.radiusLabel.TabIndex = 4;
            this.radiusLabel.Text = "Title";
            // 
            // textBox_title
            // 
            this.textBox_title.Location = new System.Drawing.Point(58, 6);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Size = new System.Drawing.Size(110, 20);
            this.textBox_title.TabIndex = 1;
            this.textBox_title.Text = "mod_1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Radius";
            // 
            // ModifierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 91);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.textBox_title);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.textBox_radius);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radiusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ModifierForm";
            this.Text = "ModifierForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModifiersForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.TextBox textBox_radius;
        private System.Windows.Forms.TextBox textBox_title;
        private System.Windows.Forms.Label label3;
    }
}