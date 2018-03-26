using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;



namespace GeoEdit
{

    [Serializable]
    public class Coord
    {

        public Coord()
        {
            Init();
        }
        public Coord(MapAxes mapAxes, Pillar[,] pillars)
        {
            Init(mapAxes, pillars);
        }
        public Coord(SpecGrid specGrid, MapAxes mapAxes, string file, FileType type)
        {
            Init();
            Read(specGrid, mapAxes, file, type);
        }
        public Coord(int nx, int ny, double xSize, double ySize)
        {
            Init(nx, ny, xSize, ySize);
        }






        public Pillar[,] Pillars { set; get; }


        public MapAxes MapAxes { set; get; }




        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            MapAxes.Write(writer);
            int icount = NPillarsX();
            int jcount = NPillarsY();
            writer.Write(icount);
            writer.Write(jcount);
            for (int i = 0; i < icount; ++i)
                for (int j = 0; j < jcount; ++j)
                    Pillars[i, j].Write(writer);
        }



        public static Coord Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch (version)
            {
                case Version0:
                    {
                        MapAxes ma = MapAxes.Read(reader);
                        int icount = reader.ReadInt32();
                        int jcount = reader.ReadInt32();
                        Pillar[,] ps = new Pillar[icount, jcount];
                        for (int i = 0; i < icount; ++i)
                            for (int j = 0; j < jcount; ++j)
                                ps[i, j] = Pillar.Read(reader);
                        return new Coord(ma, ps);
                    }
                default:
                    return new Coord();
            }
        }









        public int NPillarsX()
        {
            return Pillars.GetLength(0);
        }




        public int NPillarsY()
        {
            return Pillars.GetLength(1);
        }



        void Init(int nx, int ny, double xSize, double ySize)
        {
            MapAxes = new MapAxes();
            const double z = 0f;
            Pillars = new Pillar[nx + 1, ny + 1];
            double y = 0.0f;
            for (int j = 0; j < ny + 1; ++j)
            {
                double x = 0.0f;
                for (int i = 0; i < nx + 1; ++i)
                {
                    Pillars[i, j] = new Pillar(x, y, z, x, y, z);
                    x += xSize;
                }
                y += ySize;
            }
        }




        void Init()
        {
            Pillars = new Pillar[0, 0];
            MapAxes = new MapAxes();
        }


        void Init(MapAxes mapAxes, Pillar[,] pillars)
        {
            Pillars = pillars;
            MapAxes = mapAxes;
        }




        public void Clear()
        {
            Init();
        }



        public bool Specified()
        {
            return NPillarsX() != 0 && NPillarsY() != 0;
        }





        double[,] Matrix(MapAxes mapAxes)
        {
            if (mapAxes.X1 == mapAxes.X2 && mapAxes.X2 == mapAxes.X3 &&
                mapAxes.Y1 == mapAxes.Y2 && mapAxes.Y2 == mapAxes.Y3)
                return new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            // http://htmlbook.ru/blog/matritsa-preobrazovanii
            /*
            x' = ax+cy+tx
            y' = bx+dy+ty
            */
            //float tx = -X2;
            //float ty = -Y2;
            //float a = X1 == X2 ? 1f : X1 - X2 / Math.Abs(X1 - X2);
            //float d = Y3 == Y2 ? 1f : Y3 - Y2 / Math.Abs(Y3 - Y2);
            double tx = 0;
            double ty = 0;
            double a = -1;
            double d = 1;
            double b = 0f;// переделать
            double c = 0f;// переделать
            /*
            0,0  0,1  0,2            a   b   0
            1,0  1,1  1,2            c   d   0
            2,0  2,1  2,2            tx  ty  1
            */
            return new double[,] { { a, b, 0 }, { c, d, 0 }, { tx, ty, 1 } };
        }



        public Pillar GlobalPillar(int i, int j)
        {
            const double zTop = 0f, zBottom = 10000f;
            Point3D top = Pillars[i, j].Point3D(zTop);
            Point3D bottom = Pillars[i, j].Point3D(zBottom);
            Pillar result = new Pillar(GlobalPoint3D(top), GlobalPoint3D(bottom));
            return result;
        }




        Point3D GlobalPoint3D(Point3D local)
        {
            //http://roboforum.ru/wiki/%D0%9F%D1%80%D0%B8%D0%BA%D0%BB%D0%B0%D0%B4%D0%BD%D0%B0%D1%8F_%D0%B3%D0%B5%D0%BE%D0%BC%D0%B5%D1%82%D1%80%D0%B8%D1%8F
            Point3D global = new Point3D();
            double distance = Distance(this.MapAxes.X1, this.MapAxes.Y1, this.MapAxes.X2, this.MapAxes.Y2);
            double cos = (this.MapAxes.Y1 - this.MapAxes.Y2) / distance;
            double sin = (this.MapAxes.X1 - this.MapAxes.X2) / distance;
            global.X = this.MapAxes.X2 + local.X * cos - local.Y * sin;
            global.Y = this.MapAxes.Y2 + local.Y * cos + local.X * sin;
            global.Z = local.Z;
            return global;
        }



        /*
        Point3D GlobalPoint3D(Point3D local)
        {
            //http://roboforum.ru/wiki/%D0%9F%D1%80%D0%B8%D0%BA%D0%BB%D0%B0%D0%B4%D0%BD%D0%B0%D1%8F_%D0%B3%D0%B5%D0%BE%D0%BC%D0%B5%D1%82%D1%80%D0%B8%D1%8F
            Point3D global = new Point3D();
            float cos = (this.MapAxes.Y1 - this.MapAxes.Y2) / Distance(this.MapAxes.X1, this.MapAxes.Y1, this.MapAxes.X2, this.MapAxes.Y2);
            float sin = (this.MapAxes.X3 - this.MapAxes.X2) / Distance(this.MapAxes.X2, this.MapAxes.Y2, this.MapAxes.X3, this.MapAxes.Y3);
            global.X = this.MapAxes.X2 + local.X * cos - local.Y * sin;
            global.Y = this.MapAxes.Y2 + local.Y * cos + local.X * sin;
            global.Z = local.Z;
            return global;
        }
        */



        double Distance(double x1, double y1, double x2, double y2)
        {
            double dx = x1 - x2;
            double dy = y1 - y2;
            double result = Math.Pow(dx * dx + dy * dy, 0.5);
            return result;
        }





        public bool Read(SpecGrid specGrid, MapAxes mapAxes, string file, FileType type)
        {
            switch (type)
            {
                case FileType.CMG_ASCII:
                    return ReadFromCMG(specGrid, file);
                case FileType.GRDECL_ASCII:
                    return ReadFromGRDECL(specGrid, mapAxes, file);
                default:
                    return false;
            }
        }




        const string grdecl_kw_coord = "COORD";
        bool ReadFromGRDECL(SpecGrid specGrid, MapAxes mapAxes, string file)
        {
            double[] values = GRDECLReader.Array(file, grdecl_kw_coord);
            if (values.Count() == 0)
                return false;
            if (values.Count() != 2 * 3 * (specGrid.NX + 1) * (specGrid.NY + 1))
                return false;
            //Init(specGrid.NX, specGrid.NX, 50f, 50f);
            Pillars = new Pillar[specGrid.NX + 1, specGrid.NY + 1];
            int n = 0;
            for (int j = 0; j < specGrid.NY + 1; ++j)
            {
                for (int i = 0; i < specGrid.NX + 1; ++i)
                {
                    double xt = values[n++];
                    double yt = values[n++];
                    double zt = values[n++];
                    double xb = values[n++];
                    double yb = values[n++];
                    double zb = values[n++];
                    Pillars[i, j] = new Pillar(xt, yt, zt, xb, yb, zb);
                }
            }
            MapAxes = mapAxes;
            return true;
        }


        /*
        public bool FillFromGRDECL(SpecGrid specGrid, string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Pillars = new Pillar[specGrid.NX + 1, specGrid.NY + 1];
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (GRDECLReader.ClearLine(line) == grdecl_kw_coord)
                            break;
                    List<string> values = new List<string>();
                    int valuesNeeded = 6 * (specGrid.NX + 1);
                    int n = 0;
                    for (int j = 0; j < specGrid.NY + 1; ++j)
                    {
                        while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null) // add
                        {
                            string[] words = GRDECLReader.ClearWords(line);
                            foreach (string word in words)
                                values.Add(word);
                        }
                        for (int i = 0; i < specGrid.NX + 1; ++i)
                        {
                            float xt = float.Parse(values[n++]);
                            float yt = float.Parse(values[n++]);
                            float zt = float.Parse(values[n++]);
                            float xb = float.Parse(values[n++]);
                            float yb = float.Parse(values[n++]);
                            float zb = float.Parse(values[n++]);
                            Pillars[i, j] = new Pillar(xt, yt, zt, xb, yb, zb);
                        }
                        values.RemoveRange(0, valuesNeeded); // remove
                        n = 0;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
        }
        */




        const string cmg_kw_coord = "*COORD";
        bool ReadFromCMG(SpecGrid specGrid, string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Pillars = new Pillar[specGrid.NX + 1, specGrid.NY + 1];
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (CMGReader.ClearLine(line) == cmg_kw_coord)
                            break;
                    List<string> values = new List<string>();
                    int valuesNeeded = 6 * (specGrid.NX + 1);
                    int n = 0;
                    for (int j = 0; j < specGrid.NY + 1; ++j)
                    {
                        while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null)// add
                        {
                            line = CMGReader.ClearLine(line);
                            if (line != string.Empty)
                                foreach (string word in line.Split())
                                    values.Add(word);
                        }
                        for (int i = 0; i < specGrid.NX + 1; ++i)
                        {
                            double xt = double.Parse(values[n++]);
                            double yt = double.Parse(values[n++]);
                            double zt = double.Parse(values[n++]);
                            double xb = double.Parse(values[n++]);
                            double yb = double.Parse(values[n++]);
                            double zb = double.Parse(values[n++]);
                            Pillars[i, j] = new Pillar(xt, yt, zt, xb, yb, zb);
                        }
                        values.RemoveRange(0, valuesNeeded); // remove
                        n = 0;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
        }








        public bool Write(string file, FileType type)
        {
            switch (type)
            {
                /*
                case FileType.CMG_ASCII:
                    return ReadFromCMG(file);
                    */
                case FileType.GRDECL_ASCII:
                    return WriteFromGRDECL(file);
                default:
                    return false;
            }
        }






        bool WriteFromGRDECL(string file)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file, true))
                {
                    int nx = NPillarsX(), ny = NPillarsY();
                    // this.MapAxes.Write(file, FileType.GRDECL_ASCII);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(grdecl_kw_coord);
                    for (int j = 0; j < ny; ++j)
                        for (int i = 0; i < nx; ++i)
                            sw.WriteLine(Pillars[i, j].ToString());
                    sw.WriteLine("/");
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
            return true;
        }









    }
}
