using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace GeoEdit
{
    [Serializable]
    public class Point3D
    {
        public Point3D()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
        }
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point3D(Point3D point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }
        public PointF ToPointF(CoordPlane plane)
        {
            switch(plane)
            {
                case CoordPlane.XY:
                    return new PointF((float)X, (float)Y);
                case CoordPlane.XZ:
                    return new PointF((float)X, (float)Z);
                default: //CoordPlane.XY:
                    return new PointF((float)Y, (float)Z);
            }
        }
        public Point2D Point2D(CoordPlane plane)
        {
            switch (plane)
            {
                case CoordPlane.XY:
                    return new Point2D(X, Y);
                case CoordPlane.XZ:
                    return new Point2D(X, Z);
                default: //CoordPlane.XY:
                    return new Point2D(Y, Z);
            }
        }
        static public Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D((p1.X + p2.X), (p1.Y + p2.Y), (p1.Z + p2.Z));
        }
        static public Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D((p1.X - p2.X), (p1.Y - p2.Y), (p1.Z - p2.Z));
        }
        static public Point3D operator /(Point3D p1, double div)
        {
            return new Point3D(p1.X / div, p1.Y / div, p1.Z / div);
        }
        static public Point3D operator *(Point3D p1, double mult)
        {
            return new Point3D(p1.X * mult, p1.Y * mult, p1.Z * mult);
        }


        override public string ToString()
        {
            return X + " " + Y + " " + Z;
        }


        public bool Use(double[,] matrix)
        {
            if (matrix.GetLength(0) != 3 && matrix.GetLength(1) != 3)
                return false;
            X = matrix[0, 0] * X + matrix[1, 0] * Y + matrix[2, 0];
            Y = matrix[0, 1] * X + matrix[1, 1] * Y + matrix[2, 1];
            Z = matrix[0, 2] * X + matrix[1, 2] * Y + matrix[2, 2];
            return true;
        }



        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }

        public static Point3D Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    return new Point3D(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
                default:
                    return new Point3D();
            }
        }





    }
}
