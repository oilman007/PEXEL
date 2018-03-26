using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;



namespace GeoEdit
{
    [Serializable]
    public class ZcornItem
    {
        public ZcornItem()
        {
            Corners = new double[8] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
        }



        public ZcornItem(double c0, double c1, double c2, double c3, double c4, double c5, double c6, double c7)
        {
            Corners = new double[8] { c0, c1, c2, c3, c4, c5, c6, c7 };
        }





        public double[] Corners { set; get; }






        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            for (int i = 0; i < 8; ++i)
                writer.Write(Corners[i]);
        }

        public static ZcornItem Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    return new ZcornItem(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(),
                                         reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
                default:
                    return new ZcornItem();
            }
        }


    }
}
