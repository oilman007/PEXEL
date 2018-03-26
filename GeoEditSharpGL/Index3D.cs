using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;



namespace GeoEdit
{
    public class Index3D
    {
        public Index3D()
        {
            I = 0;
            J = 0;
            K = 0;
        }

        public Index3D(int i, int j, int k)
        {
            I = i;
            J = j;
            K = k;
        }

        /*
        static public Index3D Undef
        {
            get
            {
                return new Index3D(0, 0, 0);
            }
        }
         * */

        public int I { set; get; }
        public int J { set; get; }
        public int K { set; get; }






        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(I);
            writer.Write(J);
            writer.Write(K);
        }


        public static Index3D Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch (version)
            {
                case Version0:
                        return new Index3D(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                default:
                    return new Index3D();
            }
        }







    }
}
