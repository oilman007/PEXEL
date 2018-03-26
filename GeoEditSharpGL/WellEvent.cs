using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace GeoEdit
{


    [Serializable]
    public class WellEvent
    {

        public WellEvent()
        {
            Type = "NONE";
            Top = 0;
            Bottom = 0;
            Diametr = 0;
            SkinFactor = 0;
        }


        public WellEvent(string type, double top, double bottom, double diametr, double skinFactor)
        {
            Type = type;
            Top = top;
            Bottom = bottom;
            Diametr = diametr;
            SkinFactor = skinFactor;
        }







        public string Type { set; get; }
        public double Top { set; get; }
        public double Bottom { set; get; }
        public double Diametr { set; get; }
        public double SkinFactor { set; get; }





        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Type);
            writer.Write(Top);
            writer.Write(Bottom);
            writer.Write(Diametr);
            writer.Write(SkinFactor);
        }


        public static WellEvent Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    return new WellEvent(reader.ReadString(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
                default:
                    return new WellEvent();
            }
        }



    }
}
