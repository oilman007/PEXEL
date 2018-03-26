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
    public partial class ModifierForm : Form
    {
        public ModifierForm()
        {
            InitializeComponent();
        }

        private void ModifiersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }



        public string Title
        {
            set
            {
                this.textBox_title.Text = value;
            }
            get
            {
                return this.textBox_title.Text;
            }
        }


        public double Radius
        {
            set
            {
                this.textBox_radius.Text = value.ToString();
            }
            get
            {
                double result;
                if (double.TryParse(this.textBox_radius.Text, out result))
                    return result;
                else return 0.0;
            }
        }


        public double Value
        {
            set
            {
                this.textBox_value.Text = value.ToString();
            }
            get
            {
                double result;
                if (double.TryParse(this.textBox_value.Text, out result))
                    return result;
                else return 1.0;
            }
        }





        bool IsValid(string txtvalue)
        {
            double value;
            return double.TryParse(txtvalue, out value);
            //return valid && value > 0;
        }





        private void textBox_radius_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValid(textBox_radius.Text.ToString());
        }

        private void textBox_value_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValid(textBox_value.Text.ToString());
        }












    }
}
