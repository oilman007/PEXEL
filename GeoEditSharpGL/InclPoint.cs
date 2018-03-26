using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace GeoEdit
{

    [Serializable]
    public class InclPoint
    {
        public InclPoint() { X = 0.0f; Y = 0.0f; TVD = 0.0f; MD = 0.0f; }
        public InclPoint(double x, double y, double tvd, double md) { X = x; Y = y; TVD = tvd; MD = md; }
        public double X { set; get; }
        public double Y { set; get; }
        public double TVD { set; get; }
        public double MD { set; get; }
        public Point3D XYZ() { return new Point3D(X, Y, TVD); }


        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(X);
            writer.Write(Y);
            writer.Write(TVD);
            writer.Write(MD);
        }


        public static InclPoint Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    {
                        return new InclPoint(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
                    }
                default:
                    return new InclPoint();
            }
            
        }





    }
}
