using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GeoEdit
{
    public class Point2D
    {
        public Point2D()
        {
            X = 0.0f;
            Y = 0.0f;
        }
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point2D(Point2D point)
        {
            X = point.X;
            Y = point.Y;
        }
        public double X { set; get; }
        public double Y { set; get; }



        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(X);
            writer.Write(Y);
        }

        public static Point2D Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    return new Point2D(reader.ReadDouble(), reader.ReadDouble());
                default:
                    return new Point2D();
            }
        }






    }
}
