using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;




namespace GeoEdit
{

    public enum EditMode { None, Multiply, Set }



    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
        }


        public const EditMode DefaultMode = EditMode.None;


        public int Radius
        {
            get
            {
                return (int)radiusNumericUpDown.Value;
            }
            set
            {
                decimal v = (decimal)value;
                decimal min = radiusNumericUpDown.Minimum;
                decimal max = radiusNumericUpDown.Maximum;
                radiusNumericUpDown.Value = Math.Max(Math.Min(v, max), min);
            }
        }


        public float Value
        {
            get
            {
                return float.Parse(textBox_value.Text);
            }
            set
            {
                textBox_value.Text = value.ToString();
            }
        }


       
        public bool UseMaxValue
        {
            set
            {
                checkBox_maxValue.Checked = value;
            }
            get
            {
                return checkBox_maxValue.Checked;
            }
        }


        public bool UseMinValue
        {
            set
            {
                checkBox_minValue.Checked = value;
            }
            get
            {
                return checkBox_minValue.Checked;
            }
        }


        public float MaxValue
        {
            get
            {
                return float.Parse(textBox_maxValue.Text);
            }
            set
            {
                textBox_maxValue.Text = value.ToString();
            }
        }


        public float MinValue
        {
            get
            {
                return float.Parse(textBox_minValue.Text);
            }
            set
            {
                textBox_minValue.Text = value.ToString();
            }
        }



        /*
        private void valueTextBox_Validating(object sender, CancelEventArgs e)
        {
            float r;
            e.Cancel = !float.TryParse(valueTextBox.Text, out r);
        }
        */


        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }




        private void textBox_value_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidValue(textBox_value.Text);
        }



        bool IsValidValue(string txtValue)
        {
            float value;
            return float.TryParse(txtValue, out value);
        }

        private void textBox_minValue_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidValue(textBox_minValue.Text);
        }

        private void textBox_maxValue_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidValue(textBox_maxValue.Text);
        }




        private void radiusNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.radiusNumericUpDown.Increment = (decimal) 0.05 * radiusNumericUpDown.Value;
        }



    }
}
