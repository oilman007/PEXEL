using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace GeoEdit
{
    [Serializable]
    public class Grid
    {
        public Grid() 
        {
            Clear();
        }




        public Grid(string title, int nx, int ny, int nz, double xSize, double ySize, double zSize, double depth)
        {
            Init(title, nx, ny, nz, xSize, ySize, zSize, depth);
        }


        public Grid(string title, SpecGrid sg, Coord c, Zcorn z, Actnum a, List<Prop> ps, List<Compdat> ws)
        {
            Init(title, sg, c, z, a, ps, ws);
        }




        void Clear()
        {
            SpecGrid = new SpecGrid();
            //MapAxes = new MapAxes();
            Coord = new Coord();
            Zcorn = new Zcorn();
            Actnum = new Actnum();
            Props = new List<Prop>();
            Title = "Grid_" + (Namber++).ToString();
            Wells = new List<Compdat>();
        }



        public string Title { set; get; }
        public SpecGrid SpecGrid { set; get; }
        //public MapAxes MapAxes { set; get; }
        public Coord Coord { set; get; }
        public Zcorn Zcorn { set; get; }
        public Actnum Actnum { set; get; }
        public List<Prop> Props { set; get; }
        public List<Compdat> Wells { set; get; }

        static long Namber = 1;





        public void AddProp(string title, double value)
        {
            this.Props.Add(new Prop(NX(), NY(), NZ(), value, title));
        }



        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Title);
            SpecGrid.Write(writer);
            Coord.Write(writer);
            Zcorn.Write(writer);
            Actnum.Write(writer);
            writer.Write(Props.Count);
            foreach (Prop p in Props)
                p.Write(writer);
            writer.Write(Wells.Count);
            foreach (Compdat well in Wells)
                well.Write(writer);
        }


        public static Grid Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch (version)
            {
                case Version0:
                    {
                        string title = reader.ReadString();
                        SpecGrid sg = SpecGrid.Read(reader);
                        Coord c = Coord.Read(reader);
                        Zcorn z = Zcorn.Read(reader);
                        Actnum a = Actnum.Read(reader);
                        int np = reader.ReadInt32();
                        List<Prop> ps = new List<Prop>();
                        for (int p = 0; p < np; ++p)
                            ps.Add(Prop.Read(reader));
                        int nw = reader.ReadInt32();
                        List<Compdat> ws = new List<Compdat>();
                        for (int w = 0; w < nw; ++w)
                            ws.Add(Compdat.Read(reader));
                        return new Grid(title, sg, c, z, a, ps, ws);
                    }
                default:
                    return new Grid();
            }
        }





        


        void Init(string title, int nx, int ny, int nz, double xSize, double ySize, double zSize, double depth)
        {
            Clear();
            Title = title;
            SpecGrid = new GeoEdit.SpecGrid(nx, ny, nz);
            Coord = new GeoEdit.Coord(nx, ny, xSize, ySize);
            Zcorn = new GeoEdit.Zcorn(nx, ny, nz, zSize, depth);
            Actnum = new GeoEdit.Actnum(nx, ny, nz);
        }



        void Init(string title, SpecGrid sg, Coord c, Zcorn z, Actnum a, List<Prop> ps, List<Compdat> ws)
        {
            Clear();
            Title = title;
            SpecGrid = sg;
            Coord = c;
            Zcorn = z;
            Actnum = a;
            Props = ps;
            Wells = ws;
        }






        public int NX()
        {
            return Zcorn.NX();
        }

        public int NY()
        {
            return Zcorn.NY();
        }

        public int NZ()
        {
            return Zcorn.NZ();
        }





        public Cell LocalCell(int i, int j, int k)
        {
            //float[,] matrix = MapAxes.Matrix();
            Cell cell = new Cell();
            cell.Act = Actnum.Values[i, j, k];
            cell.Corners[0] = Coord.Pillars[i + 0, j + 0].Point3D(Zcorn.Items[i, j, k].Corners[0]);
            cell.Corners[1] = Coord.Pillars[i + 1, j + 0].Point3D(Zcorn.Items[i, j, k].Corners[1]);
            cell.Corners[2] = Coord.Pillars[i + 0, j + 1].Point3D(Zcorn.Items[i, j, k].Corners[2]);
            cell.Corners[3] = Coord.Pillars[i + 1, j + 1].Point3D(Zcorn.Items[i, j, k].Corners[3]);
            cell.Corners[4] = Coord.Pillars[i + 0, j + 0].Point3D(Zcorn.Items[i, j, k].Corners[4]);
            cell.Corners[5] = Coord.Pillars[i + 1, j + 0].Point3D(Zcorn.Items[i, j, k].Corners[5]);
            cell.Corners[6] = Coord.Pillars[i + 0, j + 1].Point3D(Zcorn.Items[i, j, k].Corners[6]);
            cell.Corners[7] = Coord.Pillars[i + 1, j + 1].Point3D(Zcorn.Items[i, j, k].Corners[7]);
            return cell;
        }



        /*
        public Cell GlobalCell(int i, int j, int k)
        {
            //float[,] matrix = MapAxes.Matrix();
            Cell cell = new Cell();
            cell.Act = Actnum.Values[i, j, k];
            cell.Corners[0] = Coord.GlobalPillar(i + 0, j + 0).Point3D(Zcorn.Items[i, j, k].Corners[0]);
            cell.Corners[1] = Coord.GlobalPillar(i + 1, j + 0).Point3D(Zcorn.Items[i, j, k].Corners[1]);
            cell.Corners[2] = Coord.GlobalPillar(i + 0, j + 1).Point3D(Zcorn.Items[i, j, k].Corners[2]);
            cell.Corners[3] = Coord.GlobalPillar(i + 1, j + 1).Point3D(Zcorn.Items[i, j, k].Corners[3]);
            cell.Corners[4] = Coord.GlobalPillar(i + 0, j + 0).Point3D(Zcorn.Items[i, j, k].Corners[4]);
            cell.Corners[5] = Coord.GlobalPillar(i + 1, j + 0).Point3D(Zcorn.Items[i, j, k].Corners[5]);
            cell.Corners[6] = Coord.GlobalPillar(i + 0, j + 1).Point3D(Zcorn.Items[i, j, k].Corners[6]);
            cell.Corners[7] = Coord.GlobalPillar(i + 1, j + 1).Point3D(Zcorn.Items[i, j, k].Corners[7]);
            return cell;
        }
        */







        public bool Specified()
        {
            return SpecGrid.Specified() && Coord.Specified() && Zcorn.Specified() && Actnum.Specified();
        }



        



        public bool Read(string file, FileType type)
        {
            SpecGrid = new SpecGrid(file, type);
            MapAxes ma = new MapAxes(file, type);
            Coord = new Coord(SpecGrid, ma, file, type);
            Zcorn = new Zcorn(SpecGrid, file, type);
            Actnum = new Actnum(SpecGrid, file, type);
            if (Specified())
                return true;
            Clear();
            return false;
        }



        public bool Write(string file, FileType type)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine(string.Empty);
            }
            return 
            this.SpecGrid.Write(file, type) && 
            this.Coord.MapAxes.Write(file, type) &&
            this.Coord.Write(file, type) &&
            this.Zcorn.Write(file, type) &&
            this.Actnum.Write(file, type);
        }




        /*
        public float Height()
        {
            if (!Specified())
                return 0f;
            float min = Cells[0, 0, 0].Corners[0].Y;
            float max = Cells[0, 0, 0].Corners[0].Y;
            for (int k = 0; k < NZ(); ++k)
            {
                for (int j = 0; j < NY(); ++j)
                {
                    for (int i = 0; i < NX(); ++i)
                    {
                        for (int c = 0; c < 8; ++c)
                        {
                            if (min > Cells[i, j, k].Corners[c].Y)
                                min = Cells[i, j, k].Corners[c].Y;
                            if (max < Cells[i, j, k].Corners[c].Y)
                                max = Cells[i, j, k].Corners[c].Y;
                        }
                    }
                }
            }
            return max - min;
        }


        public float Width()
        {
            if (!Specified())
                return 0f;
            float min = Cells[0, 0, 0].Corners[0].X;
            float max = Cells[0, 0, 0].Corners[0].X;
            for (int k = 0; k < NZ(); ++k)
            {
                for (int j = 0; j < NY(); ++j)
                {
                    for (int i = 0; i < NX(); ++i)
                    {
                        for (int c = 0; c < 8; ++c)
                        {
                            if (min > Cells[i, j, k].Corners[c].X)
                                min = Cells[i, j, k].Corners[c].X;
                            if (max < Cells[i, j, k].Corners[c].X)
                                max = Cells[i, j, k].Corners[c].X;
                        }
                    }
                }
            }
            return max - min;
        }


        public PointF Location()
        {
            if (!Specified())
                return new PointF(0f, 0f);
            float minX = Cells[0, 0, 0].Corners[0].X;
            float minY = Cells[0, 0, 0].Corners[0].Y;
            for (int k = 0; k < NZ(); ++k)
            {
                for (int j = 0; j < NY(); ++j)
                {
                    for (int i = 0; i < NX(); ++i)
                    {
                        for (int c = 0; c < 8; ++c)
                        {
                            if (minX > Cells[i, j, k].Corners[c].X)
                                minX = Cells[i, j, k].Corners[c].X;
                            if (minY > Cells[i, j, k].Corners[c].Y)
                                minY = Cells[i, j, k].Corners[c].Y;
                        }
                    }
                }
            }
            return new PointF(minX, minY);
        }


        
        const string kw_specgrid = "SPECGRID";
        const string kw_mapaxes = "MAPAXES";
        const string kw_coord = "COORD";
        const string kw_zcorn = "ZCORN";
        const string kw_actnum = "ACTNUM";
         * 
         * */




        /*
        const string remString = "--";
        const string tabString = "\t";
        const string singlSpace = " ";
        const string doubleSpace = "  ";
        List<string> ClearLines(string filename)
        {
            List<string> r = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                int index = line.IndexOf(remString);
                string temp = line;
                if (index != -1)
                    temp = temp.Remove(index);
                temp = temp.Replace(tabString, singlSpace);
                while (temp.Contains(doubleSpace))
                    temp = temp.Replace(doubleSpace, singlSpace);
                temp = temp.Trim();
                temp = temp.ToUpper();
                if (temp.Count() > 0)
                    r.Add(temp);
            }
            return r;
        }


        const string terminator = "/";
        const char repeator = '*';
        List<string> KeyWordData(string kw, List<string> lines)
        {
            List<string> r = new List<string>();
            int i = 0;
            while (lines[i++] != kw)
                if (i == lines.Count)
                    return r;
            for (; i < lines.Count; ++i)
            {
                string[] split = lines[i].Split();
                foreach (string word in split)
                {
                    if (word == terminator)
                        return r;
                    if (word.Contains(repeator))
                        foreach (string value in RepeatedValues(word))
                            r.Add(value);
                    else
                        r.Add(word);
                }
            }
            r.Clear();
            return r;
        }


        const string valbydef = "1*";
        List<string> RepeatedValues(string expression)
        {
            List<string> r = new List<string>();
            string[] split = expression.Split(repeator);
            switch (split.Length)
            {
                case 1:
                    for (int i = int.Parse(split[0]); i > 0; --i)
                        r.Add(valbydef);
                    break;
                case 2:
                    for (int i = int.Parse(split[0]); i > 0; --i)
                        r.Add(split[1]);
                    break;
            }
            return r;
        }


        bool Fill(MapAxes mapAxes, SpecGrid specGrid, Coord coord, Zcorn zcorn, Actnum actnum)
        {
            if (!specGrid.Specified() || !coord.Specified() || !zcorn.Specified() || !actnum.Specified())
                return false;
            Cells = new Cell[specGrid.NX, specGrid.NY, specGrid.NZ];
            for (int k = 0; k < specGrid.NZ; ++k)
                for (int j = 0; j < specGrid.NY; ++j)
                    for (int i = 0; i < specGrid.NX; ++i)
                    {
                        Cell cell = new Cell();
                        cell.Act = actnum.Values[i, j, k];
                        cell.Corners[0] = coord.Pillars[i + 0, j + 0].Point3D(zcorn.Values[i, j, k, 0]);
                        cell.Corners[1] = coord.Pillars[i + 1, j + 0].Point3D(zcorn.Values[i, j, k, 1]);
                        cell.Corners[2] = coord.Pillars[i + 0, j + 1].Point3D(zcorn.Values[i, j, k, 2]);
                        cell.Corners[3] = coord.Pillars[i + 1, j + 1].Point3D(zcorn.Values[i, j, k, 3]);
                        cell.Corners[4] = coord.Pillars[i + 0, j + 0].Point3D(zcorn.Values[i, j, k, 4]);
                        cell.Corners[5] = coord.Pillars[i + 1, j + 0].Point3D(zcorn.Values[i, j, k, 5]);
                        cell.Corners[6] = coord.Pillars[i + 0, j + 1].Point3D(zcorn.Values[i, j, k, 6]);
                        cell.Corners[7] = coord.Pillars[i + 1, j + 1].Point3D(zcorn.Values[i, j, k, 7]);
                        Cells[i, j, k] = cell;
                    }
            return true;
        }
        */


        /*
        public bool ReadCMG(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;

                    const string kw_grid = "*GRID";
                    const string kw_coord = "*COORD";
                    const string kw_zcorn = "*ZCORN";
                    const string kw_null = "*NULL";
                    // grid
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = CMGClearLine(line);
                        if (line == string.Empty)
                            continue;
                        string[] split = line.Split();
                        if (split[0].ToUpper() == kw_grid)
                        {
                            int nx = int.Parse(split[2]);
                            int ny = int.Parse(split[3]);
                            int nz = int.Parse(split[4]);
                            const float cellSize = 0f;
                            Init(nx, ny, nz, cellSize, cellSize, cellSize);
                            break;
                        }
                    }
                    // coord
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (CMGClearLine(line) == kw_coord)
                            break;
                    }
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = CMGClearLine(line);
                        if (line == string.Empty)
                            continue;
                        if (line == kw_zcorn)
                            break;
                        foreach (string word in line.Split())
                        {
                            float value = float.Parse(word);

                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        void GetGridDim(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if(line == string.Empty)
                        continue;
                    string [] split = line.Split();
                    if (split[0] == "*GRID")
                    {
                        int nx = int.Parse(split[2]);
                        int ny = int.Parse(split[3]);
                        int nz = int.Parse(split[4]);
                        const float cellSize = 0f;
                        Init(nx, ny, nz, cellSize, cellSize, cellSize);
                        return;
                    }
                }
            }
        }

        void GetCoord(string file)
        {
        }

        void GetZcorn(string file)
        {
        }

        void GetNull(string file)
        {
        }


        string CMGClearLine(string line)
        {
            const string dblSpace = "  ";
            const string snglSpace = " ";
            const string tab = "\t";
            const string remSymbol = "**";
            int index = line.IndexOf(remSymbol);
            if (index != -1)
                line = line.Remove(index);
            line = line.Trim();
            line = line.Replace(tab, snglSpace);
            while (line.Contains(dblSpace))
                line = line.Replace(dblSpace, snglSpace);
            return line;
        }
         * */

    }
}
