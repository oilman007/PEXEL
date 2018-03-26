using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;

namespace GeoEdit
{
    public class GridPlane
    {

        public GridPlane()
        {
            Faces = new CellFace[0, 0];
            Act = new bool[0, 0];
            Values = new double[0, 0];
        }


        public GridPlane(int nx, int ny)
        {
            Faces = new CellFace[nx, ny];
            Act = new bool[nx, ny];
            Values = new double[nx, ny];
        }




        public GridPlane(Grid grid, Prop prop, int[] layers, bool iDirFlip, bool jDirFlip, bool kDirFlip)
        {
            int nx = grid.NX();
            int ny = grid.NY();
            int nz = grid.NZ();
            Faces = new CellFace[nx, ny];
            Act = new bool[nx, ny];
            Values = new double[nx, ny];
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                {
                    // faces
                    List<CellFace> faces = new List<CellFace>();
                    foreach (int k in layers)
                        faces.Add(grid.LocalCell(i, j, k).MiddleTopFace());
                    Faces[i, j] = AverageCellFace(faces);
                    // act
                    foreach (int k in layers)
                        if ((grid.Actnum.Values[i, j, k]) == true)
                        {
                            Act[i, j] = true;
                            break;
                        }
                    // values
                    int n = 0;
                    foreach (int k in layers)
                        if (grid.Actnum.Values[i, j, k] == true)
                        {
                            int iFlip = DirFlip(i, nx, iDirFlip);
                            int jFlip = DirFlip(j, ny, jDirFlip);
                            int kFlip = DirFlip(k, nz, kDirFlip);
                            Values[i, j] += prop.Values[iFlip, jFlip, kFlip];
                            ++n;
                        }
                    if (n == 0)
                        Values[i, j] = Prop.DefaultValue;
                    else
                        Values[i, j] /= n;
                }
        }


        int DirFlip(int i, int n, bool flip)
        {
            if (flip)
                return n - 1 - i;
            else
                return i;
        }





        /*
            Parallel.For(0, ny, j =>
            {
            });
        */


        public GridPlane(Grid grid, Prop prop, int[] layers)
        {
            int nx = grid.NX();
            int ny = grid.NY();
            Faces = new CellFace[nx, ny];
            Act = new bool[nx, ny];
            Values = new double[nx, ny];
            for (int i = 0; i < nx; ++i)
                Parallel.For(0, ny, j =>
                {
                    // faces
                    List<CellFace> faces = new List<CellFace>();
                    foreach (int k in layers)
                        faces.Add(grid.LocalCell(i, j, k).MiddleTopFace());
                    Faces[i, j] = AverageCellFace(faces);
                    // act
                    foreach (int k in layers)
                        if ((grid.Actnum.Values[i, j, k]) == true)
                        {
                            Act[i, j] = true;
                            break;
                        }
                    // values
                    int n = 0;
                    foreach (int k in layers)
                        if (grid.Actnum.Values[i, j, k] == true)
                        {
                            Values[i, j] += prop.Values[i, j, k];
                            ++n;
                        }
                    if (n == 0)
                        Values[i, j] = Prop.DefaultValue;
                    else
                        Values[i, j] /= n;
                });
        }




        public GridPlane(Grid grid, Prop prop, Prop weight, int[] layers)
        {
            int nx = grid.NX();
            int ny = grid.NY();
            Faces = new CellFace[nx, ny];
            Act = new bool[nx, ny];
            Values = new double[nx, ny];
            for (int i = 0; i < nx; ++i)
                Parallel.For(0, ny, j =>
                {
                    // faces
                    List<CellFace> faces = new List<CellFace>();
                    foreach (int k in layers)
                        faces.Add(grid.LocalCell(i, j, k).MiddleTopFace());
                    Faces[i, j] = AverageCellFace(faces);
                    // act
                    foreach (int k in layers)
                        if ((grid.Actnum.Values[i, j, k]) == true)
                        {
                            Act[i, j] = true;
                            break;
                        }
                    // values
                    double summ_w = 0f;
                    double summ_v_x_w = 0f;
                    foreach (int k in layers)
                    {
                        summ_v_x_w += prop.Values[i, j, k] * weight.Values[i, j, k];
                        summ_w += weight.Values[i, j, k];
                    }
                    double value = 0f;
                    if (summ_w != 0f)
                        value = summ_v_x_w / summ_w;
                    Values[i, j] = value;
                });
        }





        CellFace AverageCellFace(List<CellFace> faces)
        {
            int count = faces.Count();
            CellFace summ = faces[0];
            for (int i = 1; i < count; ++i)
                summ += faces[i];
            return summ / count;
        }






        public CellFace[,] Faces { set; get; }

        public bool[,] Act { set; get; }

        public double[,] Values { set; get; }



        public Point Index(Point2D point)
        {
            int nx = NX();
            int ny = NY();
            for (int j = 0; j < ny; ++j)
                for (int i = 0; i < nx; ++i)
                    if (Faces[i, j].Contain(point, CoordPlane.XY))
                        return new Point(i, j);
            return UndefIndex;
        }
        





        public int NX()
        {
            return Faces.GetLength(0);
        }

        public int NY()
        {
            return Faces.GetLength(1);
        }



        public const double UndefValue = 0f;



        public double MinX()
        {
            if (NX() == 0 || NY() == 0)
                return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            int count = perimeterIndexes.Count();
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MinX(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MinX(); //(!)
                if (result > value) //(!)
                    result = value;
            }
            return result;
        }



        public double MaxX()
        {
            if (NX() == 0 || NY() == 0)
                return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            int count = perimeterIndexes.Count();
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MaxX(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MaxX(); //(!)
                if (result < value) //(!)
                    result = value;
            }
            return result;
        }



        public double MinY()
        {
            if (NX() == 0 || NY() == 0)
                return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            int count = perimeterIndexes.Count();
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MinY(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MinY(); //(!)
                if (result > value) //(!)
                    result = value;
            }
            return result;
        }



        public double MaxY()
        {
            if (NX() == 0 || NY() == 0)
                return UndefValue;
            Point[] perimeterIndexes = PerimeterIndexes();
            int count = perimeterIndexes.Count();
            double result = Faces[perimeterIndexes[0].X, perimeterIndexes[0].Y].MaxY(); //(!)
            for (int i = 1; i < count; ++i)
            {
                double value = Faces[perimeterIndexes[i].X, perimeterIndexes[i].Y].MaxY(); //(!)
                if (result < value) //(!)
                    result = value;
            }
            return result;
        }




        Point[] PerimeterIndexes()
        {
            List<Point> result = new List<Point>();
            int nx = NX(), ny = NY();
            int i, j;
            // i = 0 -> nx-1       j = 0
            for (i = 0, j = 0; i < nx; ++i)
                result.Add(new Point(i, j));
            // i = nx-1            j = 1 -> ny-1
            for (i = nx - 1, j = 1; j < ny; ++j)
                result.Add(new Point(i, j));
            // i = nx-2 -> 0       j = ny-1
            for (i = nx - 2, j = ny - 1; i > -1; --i)
                result.Add(new Point(i, j));
            // i = 0               j = ny-2 -> 1
            for (i = 0, j = ny - 2; j > 0; --j)
                result.Add(new Point(i, j));
            return result.ToArray();
        }






        public static Point UndefIndex { get { return new Point(-1, -1); } }

    }
}
