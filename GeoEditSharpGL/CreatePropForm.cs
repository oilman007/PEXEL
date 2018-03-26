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
    public partial class CreatePropForm : Form
    {
        public CreatePropForm()
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
        
        public double Value
        {
            set
            {
                valueTextBox.Text = value.ToString();
            }
            get
            {
                return double.Parse(valueTextBox.Text);
            }
        }

        public string Title
        {
            set
            {
                titleTextBox.Text = value;
            }
            get
            {
                return titleTextBox.Text;
            }
        }

    }
}
