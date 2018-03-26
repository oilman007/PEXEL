namespace GeoEdit
{
    partial class EditForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_maxValue = new System.Windows.Forms.TextBox();
            this.textBox_minValue = new System.Windows.Forms.TextBox();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.checkBox_maxValue = new System.Windows.Forms.CheckBox();
            this.checkBox_minValue = new System.Windows.Forms.CheckBox();
            this.radiusNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_useViewLayers = new System.Windows.Forms.CheckBox();
            this.listBox_layers = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiusNumericUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_maxValue);
            this.groupBox2.Controls.Add(this.textBox_minValue);
            this.groupBox2.Controls.Add(this.textBox_value);
            this.groupBox2.Controls.Add(this.checkBox_maxValue);
            this.groupBox2.Controls.Add(this.checkBox_minValue);
            this.groupBox2.Controls.Add(this.radiusNumericUpDown);
            this.groupBox2.Controls.Add(this.radiusLabel);
            this.groupBox2.Controls.Add(this.valueLabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 141);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Multiplier";
            // 
            // textBox_maxValue
            // 
            this.textBox_maxValue.Location = new System.Drawing.Point(94, 111);
            this.textBox_maxValue.Name = "textBox_maxValue";
            this.textBox_maxValue.Size = new System.Drawing.Size(100, 20);
            this.textBox_maxValue.TabIndex = 8;
            this.textBox_maxValue.Text = "1";
            this.textBox_maxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_maxValue.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_maxValue_Validating);
            // 
            // textBox_minValue
            // 
            this.textBox_minValue.Location = new System.Drawing.Point(94, 85);
            this.textBox_minValue.Name = "textBox_minValue";
            this.textBox_minValue.Size = new System.Drawing.Size(100, 20);
            this.textBox_minValue.TabIndex = 8;
            this.textBox_minValue.Text = "1";
            this.textBox_minValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_minValue.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_minValue_Validating);
            // 
            // textBox_value
            // 
            this.textBox_value.Location = new System.Drawing.Point(94, 16);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(100, 20);
            this.textBox_value.TabIndex = 8;
            this.textBox_value.Text = "1";
            this.textBox_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_value.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_value_Validating);
            // 
            // checkBox_maxValue
            // 
            this.checkBox_maxValue.AutoSize = true;
            this.checkBox_maxValue.Location = new System.Drawing.Point(12, 113);
            this.checkBox_maxValue.Name = "checkBox_maxValue";
            this.checkBox_maxValue.Size = new System.Drawing.Size(76, 17);
            this.checkBox_maxValue.TabIndex = 7;
            this.checkBox_maxValue.Text = "Max Value";
            this.checkBox_maxValue.UseVisualStyleBackColor = true;
            // 
            // checkBox_minValue
            // 
            this.checkBox_minValue.AutoSize = true;
            this.checkBox_minValue.Location = new System.Drawing.Point(12, 87);
            this.checkBox_minValue.Name = "checkBox_minValue";
            this.checkBox_minValue.Size = new System.Drawing.Size(73, 17);
            this.checkBox_minValue.TabIndex = 7;
            this.checkBox_minValue.Text = "Min Value";
            this.checkBox_minValue.UseVisualStyleBackColor = true;
            // 
            // radiusNumericUpDown
            // 
            this.radiusNumericUpDown.Location = new System.Drawing.Point(94, 43);
            this.radiusNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.radiusNumericUpDown.Name = "radiusNumericUpDown";
            this.radiusNumericUpDown.Size = new System.Drawing.Size(100, 20);
            this.radiusNumericUpDown.TabIndex = 6;
            this.radiusNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.radiusNumericUpDown.ValueChanged += new System.EventHandler(this.radiusNumericUpDown_ValueChanged);
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(32, 45);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(40, 13);
            this.radiusLabel.TabIndex = 4;
            this.radiusLabel.Text = "Radius";
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(33, 19);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(34, 13);
            this.valueLabel.TabIndex = 5;
            this.valueLabel.Text = "Value";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox_useViewLayers);
            this.groupBox3.Controls.Add(this.listBox_layers);
            this.groupBox3.Location = new System.Drawing.Point(12, 159);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 452);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Layers";
            // 
            // checkBox_useViewLayers
            // 
            this.checkBox_useViewLayers.AutoSize = true;
            this.checkBox_useViewLayers.Location = new System.Drawing.Point(8, 19);
            this.checkBox_useViewLayers.Name = "checkBox_useViewLayers";
            this.checkBox_useViewLayers.Size = new System.Drawing.Size(105, 17);
            this.checkBox_useViewLayers.TabIndex = 1;
            this.checkBox_useViewLayers.Text = "Use View Layers";
            this.checkBox_useViewLayers.UseVisualStyleBackColor = true;
            // 
            // listBox_layers
            // 
            this.listBox_layers.FormattingEnabled = true;
            this.listBox_layers.Location = new System.Drawing.Point(6, 46);
            this.listBox_layers.Name = "listBox_layers";
            this.listBox_layers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_layers.Size = new System.Drawing.Size(200, 394);
            this.listBox_layers.TabIndex = 0;
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 623);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "EditForm";
            this.Text = "Edit Form";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiusNumericUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown radiusNumericUpDown;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.CheckBox checkBox_maxValue;
        private System.Windows.Forms.CheckBox checkBox_minValue;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBox_layers;
        private System.Windows.Forms.CheckBox checkBox_useViewLayers;
        private System.Windows.Forms.TextBox textBox_maxValue;
        private System.Windows.Forms.TextBox textBox_minValue;
        private System.Windows.Forms.TextBox textBox_value;
    }
}