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
    public partial class CreateGridForm : Form
    {
        public CreateGridForm()
        {
            InitializeComponent();
            Add = false;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Add = true;
            Close();
        }

        public bool Add { protected set; get; }

        public int NX
        {
            set
            {
                nxTextBox.Text = value.ToString();
            }
            get
            {
                return int.Parse(nxTextBox.Text);
            }
        }
        public int NY
        {
            set
            {
                nyTextBox.Text = value.ToString();
            }
            get
            {
                return int.Parse(nyTextBox.Text);
            }
        }
        public int NZ
        {
            set
            {
                nzTextBox.Text = value.ToString();
            }
            get
            {
                return int.Parse(nzTextBox.Text);
            }
        }

        public double XSize
        {
            set
            {
                xSizeTextBox.Text = value.ToString();
            }
            get
            {
                return double.Parse(xSizeTextBox.Text);
            }
        }
        public double YSize
        {
            set
            {
                ySizeTextBox.Text = value.ToString();
            }
            get
            {
                return double.Parse(ySizeTextBox.Text);
            }
        }
        public double ZSize
        {
            set
            {
                zSizeTextBox.Text = value.ToString();
            }
            get
            {
                return double.Parse(zSizeTextBox.Text);
            }
        }
        public double Depth
        {
            set
            {
                this.textBox_depth.Text = value.ToString();
            }
            get
            {
                return double.Parse(this.textBox_depth.Text);
            }
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





        private void CreateGridForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }
    }
}
