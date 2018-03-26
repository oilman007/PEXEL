using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace GeoEdit
{
    public class WellsPlane
    {
        public WellsPlane()
        {
            Faces = new List<WellFace>();
        }


        /*
            Parallel.For(0, ny, j =>
            {
                lock(new object())
                {
                }
            });
        */

        public WellsPlane(Grid grid, int[] layers)
        {
            Faces = new List<WellFace>();
            int nx = grid.NX(), ny = grid.NY(), nz = grid.NZ(), wcount = grid.Wells.Count();
            List<int> list = layers.ToList();
            //for (int w = 0; w < wcount; ++w)
            Parallel.For(0, wcount, w =>
            {
                //if (grid.Wells[w].Checked)
                //{
                    List<Point3D> intersections = new List<Point3D>();
                    foreach (Index3D con in grid.Wells[w].Connections)
                        if (list.Contains(con.K)) intersections.Add(grid.LocalCell(con.I, con.J, con.K).MiddleTopFace().Center());
                    if (intersections.Count() > 0)
                        lock (Faces) { Faces.Add(new WellFace(grid.Wells[w].Title, intersections, grid.Wells[w].Checked)); }
                //}
            });
        }

        public List<WellFace> Faces { set; get; }

    }
}
