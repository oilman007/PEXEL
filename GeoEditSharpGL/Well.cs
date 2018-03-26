using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace GeoEdit
{
    [Serializable]
    public class Well
    {
        public Well()
        {
            Clear();
        }


        public Well(string title, List<InclPoint> points, List<WellEvent> events, bool check = true)
        {
            Clear();
            Title = title;
            Points = points;
            Events = events;
            Checked = check;
        }




        public string Title { set; get; }
        public List<InclPoint> Points { set; get; }
        public List<WellEvent> Events { set; get; }
        public bool Checked { set; get; }

        static long Namber = 1;





        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Title);
            writer.Write(Points.Count());
            foreach (InclPoint p in Points) p.Write(writer);
            writer.Write(Events.Count());
            foreach(WellEvent e in Events) e.Write(writer);
            writer.Write(Checked);
        }


        public static Well Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    {
                        string title = reader.ReadString();
                        int pcount = reader.ReadInt32();
                        List<InclPoint> ps = new List<InclPoint>();
                        for (int p = 0; p < pcount; ++p) ps.Add(InclPoint.Read(reader));
                        int ecount = reader.ReadInt32();
                        List<WellEvent> es = new List<WellEvent>();
                        for (int e = 0; e < ecount; ++e) es.Add(WellEvent.Read(reader));
                        bool check = reader.ReadBoolean();
                        return new Well(title, ps, es, check);
                    }
                default:
                    return new Well();
            }
        }




        public void Clear()
        {
            Title = "Well_" + (Namber++).ToString();
            Points = new List<InclPoint>();
            Events = new List<WellEvent>();
            Checked = false;
        }



        public Point2D XY(double tvd)
        {
            for (int i = 1; i < Points.Count(); ++i)
            {
                if (Points[i].TVD >= tvd)
                {
                    InclPoint t = Points[i - 1];
                    InclPoint b = Points[i];
                    Pillar pillar = new Pillar(t.X, t.Y, t.TVD, b.X, b.Y, b.TVD);
                    return pillar.Point2D(tvd);
                }
            }
            return new Point2D();
        }


        public List<Point3D> Intersections(CellFace cellface)
        {
            List<Point3D> result = new List<Point3D>();
            for (int i = 1; i < Points.Count(); ++i)
            {
                Point3D inter;
                if (cellface.Intersect(Points[i - 1].XYZ(), Points[i].XYZ(), out inter)) result.Add(inter);
            }
            return result;
        }



        public Point2D[] Area()
        {
            double x_min = Points[0].X, x_max = Points[0].X, y_min = Points[0].Y, y_max = Points[0].Y;
            int count = Points.Count();
            for (int i = 1; i < count; ++i)
            {
                if (x_min > Points[i].X) x_min = Points[i].X;
                else
                if (x_max < Points[i].X) x_max = Points[i].X;
                if (y_min > Points[i].Y) y_min = Points[i].Y;
                else
                if (y_max < Points[i].Y) y_max = Points[i].Y;
            }
            return new Point2D[]
            {
                new Point2D(x_min, y_min),
                new Point2D(x_max, y_min),
                new Point2D(x_max, y_max),
                new Point2D(x_min, y_max)
            };
        }




        public List<Point3D> Intersections(Cell cell)
        {
            List<Point3D> result = new List<Point3D>();
            CellFace xyNear = cell.Face(CoordPlane.XY, FaceDistance.Near);
            CellFace xyFar = cell.Face(CoordPlane.XY, FaceDistance.Far);
            bool exit = true;
            foreach (Point2D p in Area())
                if (xyNear.Contain(p, CoordPlane.XY) || xyFar.Contain(p, CoordPlane.XY)) { exit = false; break; }
            if (exit) return result;            
            result.AddRange(Intersections(cell.Face(CoordPlane.XY, FaceDistance.Near)));
            //result.AddRange(Intersections(cell.Face(CoordPlane.XZ, FaceDistance.Near)));
            //result.AddRange(Intersections(cell.Face(CoordPlane.YZ, FaceDistance.Near)));
            //result.AddRange(Intersections(cell.Face(CoordPlane.YZ, FaceDistance.Far)));
            //result.AddRange(Intersections(cell.Face(CoordPlane.XZ, FaceDistance.Far)));
            result.AddRange(Intersections(cell.Face(CoordPlane.XY, FaceDistance.Far)));
            return result;
        }





        static public List<Well> Read(  string file,
                                        int n_col_X,
                                        int n_col_Y,
                                        int n_col_tvd,
                                        int n_col_md,
                                        string prefix = "",
                                        string ending = "/",
                                        string rem = "--")
        {
            List<Well> result = new List<Well>();
            try
            {
                prefix = prefix.ToUpper().Trim();
                ending = ending.ToUpper().Trim();
                rem = rem.ToUpper().Trim();
                string[] lines = System.IO.File.ReadAllLines(file, Encoding.GetEncoding(1251));
                List<string> words = new List<string>();
                foreach (string line in lines)
                {
                    string cline = ClearLine(line, rem);
                    if (cline == string.Empty) continue;
                    if (prefix != string.Empty) cline = ClearLine(cline.Replace(prefix, string.Empty), rem);
                    string[] split = cline.Split();
                    foreach (string word in split) words.Add(word);
                }
                int count = words.Count();
                for (int i = 0; i < count; i++)
                {
                    string title = words[i++];
                    List<InclPoint> points = new List<InclPoint>();
                    const int n_col = 4;
                    for (; words[i] != ending; i += n_col)
                    {
                        double x     = double.Parse(words[i + n_col_X     ]);
                        double y     = double.Parse(words[i + n_col_Y     ]);
                        double tvd   = double.Parse(words[i + n_col_tvd   ]);
                        double md    = double.Parse(words[i + n_col_md    ]);
                        points.Add(new InclPoint(x, y, tvd, md));
                    }
                    result.Add(new Well(title, points, new List<WellEvent>(), true));
                }
            }
            catch
            {
                return result;
            }
            return result;
        }








        static string ClearLine(string line, string remString)
        {
            const string tabString = "\t";
            const string singlSpace = " ";
            const string doubleSpace = "  ";

            int index = line.IndexOf(remString);
            if (index != -1)
                line = line.Remove(index);
            line = line.Replace(tabString, singlSpace);
            while (line.Contains(doubleSpace))
                line = line.Replace(doubleSpace, singlSpace);
            line = line.Trim();
            line = line.ToUpper();

            return line;
        }






        /*
        public bool Read(string file, FileType type)
        {
            try
            {
                switch (type)
                {
                    case FileType.RMSWell:
                        {
                            string[] lines = System.IO.File.ReadAllLines(file, Encoding.GetEncoding(1251));
                            string[] split = ClearLine(lines[2]).Split();
                            Title = split[0];
                            Top = new Point3D(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));
                            Incl.Clear();
                            int count = lines.Count();
                            for (int i = 7; i < count; ++i)
                            {
                                split = ClearLine(lines[i]).Split();
                                float x = float.Parse(split[0]);
                                float y = float.Parse(split[1]);
                                float tvd = float.Parse(split[2]);
                                float md = float.Parse(split[3]);
                                Incl.Add(new InclPoint(x, y, tvd, md));
                            }
                        }
                        break;
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        */



    }
}
