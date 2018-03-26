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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void linkLabel_mail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel_site_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel_site.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://www.pexel.ru");
        }

        private void linkLabel_mail_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel_mail.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("mailto:aubakirov-artur@yandex.ru");
        }
    }
}
