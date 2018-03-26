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
    public partial class ScalePropForm : Form
    {
        public ScalePropForm()
        {
            InitializeComponent();
        }








        void Apply(object sender, EventArgs e)
        {
            if (ApplyEvent != null)
                ApplyEvent(sender, e);
        }



        private void button_apply_Click(object sender, EventArgs e)
        {
            Apply(sender, e);
        }


        private void button_ok_Click(object sender, EventArgs e)
        {
            Apply(sender, e);
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }



        public PropScale PropScale
        {
            set
            {
                this.textBox_minValue.Text = value.Min.ToString();
                this.textBox_maxValue.Text = value.Max.ToString();
                this.checkBox_auto.Checked = value.Auto;
            }
            get
            {
                return new PropScale(double.Parse(textBox_minValue.Text), 
                                 double.Parse(textBox_maxValue.Text),   
                                 this.checkBox_auto.Checked);
            }
        }



        public double MaxValue
        {
            set
            {
                this.textBox_maxValue.Text = value.ToString();
            }
            get
            {
                return double.Parse(textBox_maxValue.Text);
            }
        }



        public event EventHandler ApplyEvent;
        public event EventHandler GetFromPropEvent;


        void GetFromProp(object sender, EventArgs e)
        {
            if (GetFromPropEvent != null)
                GetFromPropEvent(sender, e);
        }


        private void button_getMinMax_Click(object sender, EventArgs e)
        {
            GetFromProp(sender, e);
        }










    }
}
