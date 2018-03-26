using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace GeoEdit
{


    public enum ViewPlane { XY, XZ, YZ }




    class Interface2DView : SharpGL.OpenGLControl
    {
        public Grid grid = null;
        public Grid Grid 
        {
            set
            {
                grid = value;
            }
            get
            {
                return grid;
            }
        }


        public Prop prop = null;
        public Prop Prop
        {
            set
            {
                prop = value;
            }
            get
            {
                return prop;
            }
        }



        const ViewPlane defaultViewPlane = ViewPlane.XY;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
        public ViewPlane plane = defaultViewPlane;
        public ViewPlane Plane
        {
            set
            {
                plane = value;
            }
            get
            {
                return plane;
            }
        }

        private void InitializeComponent()
        {
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Location = new System.Drawing.Point(300, 232);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(120, 20);
            this.domainUpDown1.TabIndex = 0;
            this.domainUpDown1.Text = "domainUpDown1";
            // 
            // Interface2DView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.domainUpDown1);
            this.Name = "Interface2DView";
            this.Size = new System.Drawing.Size(493, 548);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }




}
