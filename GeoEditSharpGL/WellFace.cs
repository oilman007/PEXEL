using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace GeoEdit
{
    public class WellFace
    {
        public WellFace()
        {
            Title       = string.Empty;
            Trajectory  = new List<Point3D>();
            Checked = false;
        }
        public WellFace(string title, List<Point3D> trajectory, bool check)
        {
            Title = title;
            Trajectory = trajectory;
            Checked = check;
        }
        public string Title { set; get; }
        public List<Point3D> Trajectory { set; get; }
        public bool Checked { set; get; }
    }
}
