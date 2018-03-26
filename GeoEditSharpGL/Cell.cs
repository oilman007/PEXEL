using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;




/*
 *  The keyword must be followed by
    2 * NDX * 2 * NDY * 2 * NDZ
    values, where NDX, NDY, NDZ are the dimensions of the current box. For cell i, the 8 ZCORN
    values are zi,1, zi,2, zi,3, zi,4, zi,5, zi,6, zi,7, zi,8. Here values 1-4 are for the top face, with zi,1 on
    the near left corner, zi,2 on the near right corner, zi,3 on the far left corner, and zi,4, on the far
    right corner. Values 5-8 have the same function for the bottom face. Then the arrangement of
    the ZCORN values within this range are:
        • for the first row of NDX cells, input the near top values z1,1, z1,2, z2,1, z2,2,..., zi,1, zi,2,...
        zNDX,1, zNDX,2, followed by the far top values z1,3, z1,4, z2,3, z2,4,..., zi,3, zi,4,... Z.NDX,3,
        zNDX,4.
        • Repeat for each subsequent row of NDX cells in the top plane.
        • Now repeat the last two steps for the bottom values of the top plane.
        • Finally, repeat all previous steps for each plane in the grid.
    The resulting data will lie on the grid constructed by the corner points in space, with “touching”
    corners of adjacent cells separated by a small margin. The separation is only for visualizing the
    grid, and does not affect the ZCORN values.
 * */


namespace GeoEdit
{
    public enum CoordPlane { XY, XZ, YZ }
    public enum FaceDistance { Near, Middle, Far }

    [Serializable]
    public class Cell
    {
        public Cell()
        {
            Corners = new Point3D[8];
            Corners[0] = new Point3D();
            Corners[1] = new Point3D();
            Corners[2] = new Point3D();
            Corners[3] = new Point3D();
            Corners[4] = new Point3D();
            Corners[5] = new Point3D();
            Corners[6] = new Point3D();
            Corners[7] = new Point3D();
            Act = true;
        }




        public Point3D[] Corners { private set; get; }
        public bool Act { set; get; }
        



        

        public Point3D Center()
        {
            double x = (Corners[0].X + Corners[1].X + Corners[2].X + Corners[3].X + Corners[4].X + Corners[5].X + Corners[6].X + Corners[7].X) / 8;
            double y = (Corners[0].Y + Corners[1].Y + Corners[2].Y + Corners[3].Y + Corners[4].Y + Corners[5].Y + Corners[6].Y + Corners[7].Y) / 8;
            double z = (Corners[0].Z + Corners[1].Z + Corners[2].Z + Corners[3].Z + Corners[4].Z + Corners[5].Z + Corners[6].Z + Corners[7].Z) / 8;
            return new Point3D(x, y, z);
        }


        public double MinX()
        {
            double min = Corners[0].X;
            for (int c = 1; c < 8; ++c)
                if (min > Corners[c].X)
                    min = Corners[c].X;
            return min;
        }


        public double MinY()
        {
            double min = Corners[0].Y;
            for (int c = 1; c < 8; ++c)
                if (min > Corners[c].Y)
                    min = Corners[c].Y;
            return min;
        }


        public double MinZ()
        {
            double min = Corners[0].Z;
            for (int c = 1; c < 8; ++c)
                if (min > Corners[c].Z)
                    min = Corners[c].Z;
            return min;
        }


        public double MaxX()
        {
            double max = Corners[0].X;
            for (int c = 1; c < 8; ++c)
                if (max < Corners[c].X)
                    max = Corners[c].X;
            return max;
        }


        public double MaxY()
        {
            double max = Corners[0].Y;
            for (int c = 1; c < 8; ++c)
                if (max < Corners[c].Y)
                    max = Corners[c].Y;
            return max;
        }


        public double MaxZ()
        {
            double max = Corners[0].Z;
            for (int c = 1; c < 8; ++c)
                if (max < Corners[c].Z)
                    max = Corners[c].Z;
            return max;
        }



        public CellFace TopFace()
        {
            return new CellFace(Corners[0], Corners[1], Corners[2], Corners[3]);
        }



        public CellFace BottomFace()
        {
            return new CellFace(Corners[4], Corners[5], Corners[6], Corners[7]);
        }



        public CellFace LeftFace()
        {
            return new CellFace(Corners[0], Corners[2], Corners[4], Corners[6]);
        }



        public CellFace RightFace()
        {
            return new CellFace(Corners[1], Corners[3], Corners[5], Corners[7]);
        }



        public CellFace NearFace()
        {
            return new CellFace(Corners[0], Corners[1], Corners[4], Corners[5]);
        }



        public CellFace FarFace()
        {
            return new CellFace(Corners[2], Corners[3], Corners[6], Corners[7]);
        }



        public CellFace MiddleFrontFace()
        {
            CellFace nearFace = NearFace();
            CellFace farFace = FarFace();
            return new CellFace((nearFace.Corners[0] + farFace.Corners[0]) / 2f, (nearFace.Corners[1] + farFace.Corners[1]) / 2f,
                                (nearFace.Corners[2] + farFace.Corners[2]) / 2f, (nearFace.Corners[3] + farFace.Corners[3]) / 2f);
        }



        public CellFace MiddleTopFace()
        {
            CellFace topFace = TopFace();
            CellFace bottomFace = BottomFace();
            return new CellFace((topFace.Corners[0] + bottomFace.Corners[0]) / 2f, (topFace.Corners[1] + bottomFace.Corners[1]) / 2f,
                                (topFace.Corners[2] + bottomFace.Corners[2]) / 2f, (topFace.Corners[3] + bottomFace.Corners[3]) / 2f);
        }



        public CellFace MiddleLeftFace()
        {
            CellFace leftFace = LeftFace();
            CellFace rightFace = RightFace();
            return new CellFace((leftFace.Corners[0] + rightFace.Corners[0]) / 2f, (leftFace.Corners[1] + rightFace.Corners[1]) / 2f,
                                (leftFace.Corners[2] + rightFace.Corners[2]) / 2f, (leftFace.Corners[3] + rightFace.Corners[3]) / 2f);
        }



        public CellFace Face(CoordPlane plane, FaceDistance distance)
        {
            switch (plane)
            {
                case CoordPlane.XY:
                    switch (distance)
                    {
                        case FaceDistance.Near:         return TopFace();
                        case FaceDistance.Middle:       return MiddleTopFace();
                        default: /*FaceDistance.Far:*/  return BottomFace();
                    }
                case CoordPlane.XZ:
                    switch (distance)
                    {
                        case FaceDistance.Near:         return LeftFace();
                        case FaceDistance.Middle:       return MiddleLeftFace();
                        default: /*FaceDistance.Far:*/  return RightFace();
                    }
                default: // CoordPlane.YZ:
                    switch (distance)
                    {
                        case FaceDistance.Near:         return NearFace();
                        case FaceDistance.Middle:       return MiddleFrontFace();
                        default: /*FaceDistance.Far:*/  return FarFace();
                    }
            }
        }



        public double Thickness()
        {
            return BottomFace().Center().Z - TopFace().Center().Z;
        }

        public double Length()
        {
            return NearFace().Center().Y - FarFace().Center().Y;
        }

        public double Width()
        {
            return RightFace().Center().X - LeftFace().Center().X;
        }


        public double Volume()
        {
            return Thickness() * Length() * Width();
        }

        
        public bool Contain(Point3D p)
        {
            return ((TopFace().Contain(p.Point2D(CoordPlane.XY), CoordPlane.XY)   || (BottomFace().Contain(p.Point2D(CoordPlane.XY), CoordPlane.XY))) &&
                    (RightFace().Contain(p.Point2D(CoordPlane.YZ), CoordPlane.YZ) || (LeftFace().Contain(p.Point2D(CoordPlane.YZ), CoordPlane.YZ))  ) &&
                    (NearFace().Contain(p.Point2D(CoordPlane.XZ), CoordPlane.XZ)  || (FarFace().Contain(p.Point2D(CoordPlane.XZ), CoordPlane.XZ))   ) );
        }

        

    }
}
