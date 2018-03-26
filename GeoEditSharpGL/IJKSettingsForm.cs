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
    public partial class IJKSettingsForm : Form
    {
        public IJKSettingsForm()
        {
            InitializeComponent();
            IFlip = false;
            JFlip = false;
            KFlip = false;
        }


        public IJKSettingsForm(bool iFlip, bool jFlip, bool kFlip)
        {
            InitializeComponent();
            IFlip = iFlip;
            JFlip = jFlip;
            KFlip = kFlip;
        }



        public bool IFlip
        {
            set
            {
                if(value)
                {
                    iMaxRadioButton.Checked = true;
                    i0RadioButton.Checked = false;
                }
                else
                {
                    iMaxRadioButton.Checked = false;
                    i0RadioButton.Checked = true;
                }
            }
            get
            {
                if (iMaxRadioButton.Checked)
                    return true;
                else
                    return false;
            }
        }



        public bool JFlip
        {
            set
            {
                if (value)
                {
                    jMaxRadioButton.Checked = true;
                    j0RadioButton.Checked = false;
                }
                else
                {
                    jMaxRadioButton.Checked = false;
                    j0RadioButton.Checked = true;
                }
            }
            get
            {
                if (jMaxRadioButton.Checked)
                    return true;
                else
                    return false;
            }
        }



        public bool KFlip
        {
            set
            {
                if (value)
                {
                    kMaxRadioButton.Checked = true;
                    k0RadioButton.Checked = false;
                }
                else
                {
                    kMaxRadioButton.Checked = false;
                    k0RadioButton.Checked = true;
                }
            }
            get
            {
                if (kMaxRadioButton.Checked)
                    return true;
                else
                    return false;
            }
        }






        private void IJKSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Parent = null;
        }



        public event EventHandler SettingsChanged;



        private void i0RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsChanged != null)
                SettingsChanged(sender, e);
        }

        private void iMaxRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsChanged != null)
                SettingsChanged(sender, e);
        }

        private void j0RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsChanged != null)
                SettingsChanged(sender, e);
        }

        private void jMaxRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsChanged != null)
                SettingsChanged(sender, e);
        }

        private void k0RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsChanged != null)
                SettingsChanged(sender, e);
        }

        private void kMaxRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsChanged != null)
                SettingsChanged(sender, e);
        }




    }
}
