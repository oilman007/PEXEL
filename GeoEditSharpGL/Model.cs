using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace GeoEdit
{
    [Serializable]
    public class Model
    {
        public Model()
        {
            Grids = new List<Grid>();
            //Wells = new List<Well>();
            Title = "Model_" + (Namber++).ToString();
            Position = new Point3D(0, 0, 1);
        }


        public Model(string title)
        {
            Grids = new List<Grid>();
            //Wells = new List<Well>();
            Position = new Point3D(0, 0, 1);
            Title = title;
        }

        public Model(string title, List<Grid> gs, Point3D pos)
        {
            Title = title;
            Grids = gs;
            //Wells = ws;
            Position = pos;
        }





        public string Title { set; get; }

        public List<Grid> Grids { set; get; }
        
        //public List<Well> Wells  { set; get; }

        public Point3D Position { set; get; }

        static long Namber = 1;




        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Title);
            writer.Write(Grids.Count);
            foreach (Grid g in Grids)
                g.Write(writer);
            /*
            writer.Write(Wells.Count);
            foreach (Well w in Wells)
                w.Write(writer);
            */
            Position.Write(writer);
        }



        public static Model Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    {
                        string title = reader.ReadString();
                        int ng = reader.ReadInt32();
                        List<Grid> gs = new List<Grid>();
                        for (int g = 0; g < ng; ++g)
                            gs.Add(Grid.Read(reader));
                        /*
                        int nw = reader.ReadInt32();
                        List<Well> ws = new List<Well>();
                        for (int w = 0; w < nw; ++w)
                            ws.Add(Well.Read(reader));
                        */
                        Point3D pos = Point3D.Read(reader);
                        return new Model(title, gs, pos);
                    }
                default:
                    return new Model();
            }
        }
        



    }
}
