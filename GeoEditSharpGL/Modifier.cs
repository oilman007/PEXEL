using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace GeoEdit
{
    [Serializable]
    public class Modifier
    {
        public Modifier()
        {
            Value = 1;
            Radius = 0;
            Layer = 0;
            Use = true;
            Location = new Point2D();
        }
        public Modifier(Point2D location, int layer, double value, double radius, bool use = true)
        {
            Radius = radius;
            Value = value;
            Layer = layer;
            Use = use;
            Location = new Point2D(location.X, location.Y);
        }
        Modifier(string[] split)
        {
            Value = double.Parse(split[1]);
            Radius = double.Parse(split[2]);
            Layer = int.Parse(split[5]);
            Use = false;
        }



        public bool Use { set; get; }
        public double Value { set; get; }
        public double Radius { set; get; }
        public int Layer { set; get; }
        public Point2D Location { set; get; }





        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Location.X);
            writer.Write(Location.Y);
            writer.Write(Layer);
            writer.Write(Value);
            writer.Write(Radius);
            writer.Write(Use);
        }



        static public Modifier Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    {
                        Point2D location = new Point2D(reader.ReadDouble(), reader.ReadDouble());
                        int layer = reader.ReadInt32();
                        double value = reader.ReadDouble();
                        double radius = reader.ReadDouble();
                        bool use = reader.ReadBoolean();
                        return new Modifier(location, layer, value, radius, use);
                    }
                default:
                    return new Modifier();
            }
        }



        public string TextForm()
        {
            return  Layer.ToString() + '\t' +
                    Value.ToString() + '\t' +
                    Radius.ToString();
        }


        public const string TextHeader = " --name\tvalue\trad\tx\ty\tk";

        




    }
}
