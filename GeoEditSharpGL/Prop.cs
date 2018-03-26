using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;

namespace GeoEdit
{
    [Serializable]
    public class Prop
    {
        public Prop()
        {
            Clear();
        }

        public Prop(int nx, int ny, int nz, double value, string title = null)
        {
            Clear();
            if (title != null)
                Title = title;
            Values = new double[nx, ny, nz];
            //float randomValue = 0f;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                    {
                        Values[i, j, k] = value;
                        //Random rand = new Random();
                        //Values[i, j, k] = (float)(rand.NextDouble() * 100.0);
                        //Values[i, j, k] = randomValue++;
                    }
            UpdateScale();
        }

        public Prop(int nx, int ny, int nz, string kw, string file, FileType type)
        {
            Read(nx, ny, nz, kw, file, type);
        }


        public Prop(string title, double[,,] values, List<ModifiersGroup> mg, PropScale scale)
        {
            this.Title = title;
            this.Values = values;
            this.Groups = mg;
            this.Scale = scale;
        }








        public const double DefaultValue = -999f;
        public double[,,] Values { set; get; }
        public string Title { set; get; }
        public PropScale Scale { set; get; }
        public List<ModifiersGroup> Groups { set; get; }

        static long Namber = 1;






        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Title);
            int nx = NX(), ny = NY(), nz = NZ();
            writer.Write(nx);
            writer.Write(ny);
            writer.Write(nz);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                    for (int k = 0; k < nz; ++k)
                        writer.Write(Values[i, j, k]);
            writer.Write(Groups.Count);
            foreach(ModifiersGroup m in Groups)
                m.Write(writer);
            this.Scale.Write(writer);
        }




        public static Prop Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    {
                        string title = reader.ReadString();
                        int nx = reader.ReadInt32();
                        int ny = reader.ReadInt32();
                        int nz = reader.ReadInt32();
                        double[,,] values = new double[nx, ny, nz];
                        for (int i = 0; i < nx; ++i)
                            for (int j = 0; j < ny; ++j)
                                for (int k = 0; k < nz; ++k)
                                    values[i, j, k] = reader.ReadDouble();
                        int nm = reader.ReadInt32();
                        List<ModifiersGroup> mg = new List<ModifiersGroup>();
                        for (int m = 0; m < nm; ++m)
                            mg.Add(ModifiersGroup.Read(reader));
                        PropScale scale = PropScale.Read(reader);
                        return new Prop(title, values, mg, scale);
                    }
                default:
                    return new Prop();
            }
        }






        public void UpdateScale()
        {
            this.Scale.Min = Min();
            this.Scale.Max = Max();
        }









        void Clear()
        {
            Values = new double[0, 0, 0];
            Title = "Prop_" + (Namber++).ToString();
            Scale = new PropScale();
            Groups = new List<ModifiersGroup>();
        }

        


        public int NX()
        {
            return Values.GetLength(0);
        }
        public int NY()
        {
            return Values.GetLength(1);
        }
        public int NZ()
        {
            return Values.GetLength(2);
        }

        public double Min()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            if (nx == 0 || ny == 0 || nz == 0)
                return DefaultValue;
            double r = Values[0, 0, 0];
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (r > Values[i, j, k])
                            r = Values[i, j, k];
            return r;
        }

        public double Max()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            if (nx == 0 || ny == 0 || nz == 0)
                return DefaultValue;
            double r = Values[0, 0, 0];
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (r < Values[i, j, k])
                            r = Values[i, j, k];
            return r;
        }

        public bool Read(int nx, int ny, int nz, string kw, string file, FileType type)
        {
            switch (type)
            {
                case FileType.GRDECL_ASCII:
                    return ReadGRDECL(nx, ny, nz, kw, file);
                case FileType.CMG_ASCII:
                    return ReadCMG(nx, ny, nz, kw, file);
                default:
                    return false;
            }
        }


        bool ReadGRDECL(int nx, int ny, int nz, string kw, string file)
        {
            Title = kw;
            double[] values = GRDECLReader.Array(file, kw);
            if (values.Count() == 0)
                return false;
            if (values.Count() != nx * ny * nz)
                return false;
            Values = new double[nx, ny, nz];
            int n = 0;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Values[i, j, k] = values[n++];
            UpdateScale();
            return true;
        }


        bool ReadCMG(int nx, int ny, int nz, string kw, string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Title = kw;
                    Values = new double[nx, ny, nz];
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (CMGReader.ClearLine(line) == kw)
                            break;
                    List<string> values = new List<string>();
                    int valuesNeeded = nx;
                    int n = 0;
                    for (int k = 0; k < nz; ++k)
                    {
                        for (int j = 0; j < ny; ++j)
                        {
                            while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null)
                            {
                                line = CMGReader.ClearLine(line);
                                if (line != string.Empty)
                                    foreach (string word in line.Split())
                                        values.Add(word);
                            }
                            for (int i = 0; i < nx; ++i)
                            {
                                Values[i, j, k] = double.Parse(values[n++]);
                            }
                            values.RemoveRange(0, valuesNeeded);
                            n = 0;
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                Clear();
                return false;
            }
        }

        
        public bool Write(string kw, string file, FileType type)
        {
            switch (type)
            {
                case FileType.GRDECL_ASCII:
                    return WriteGRDECL(kw, file);
                case FileType.CMG_ASCII:
                    return WriteCMG(kw, file);
                default:
                    return false;
            }
        }


        bool WriteGRDECL(string kw, string file)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine(kw);
                    string line = string.Empty;
                    int c = 0;
                    int nx = NX(), ny = NY(), nz = NZ();
                    for (int k = 0; k < nz; ++k)
                        for (int j = 0; j < ny; ++j)
                            for (int i = 0; i < nx; ++i)
                            {
                                line += OutputStringFormat(Values[i, j, k]);
                                if (++c == nColumn)
                                {
                                    sw.WriteLine(line);
                                    line = string.Empty;
                                    c = 0;
                                }
                            }
                    if (line != string.Empty)
                        sw.WriteLine(line);
                    sw.WriteLine("/");
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        bool WriteCMG(string kw, string file)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine("*" + kw + " *ALL");
                    string line = string.Empty;
                    int c = 0;
                    int nx = NX(), ny = NY(), nz = NZ();
                    for (int k = 0; k < nz; ++k)
                        for (int j = 0; j < ny; ++j)
                            for (int i = 0; i < nx; ++i)
                            {
                                line += OutputStringFormat(Values[i, j, k]);
                                if (++c == nColumn)
                                {
                                    sw.WriteLine(line);
                                    line = string.Empty;
                                    c = 0;
                                }
                            }
                    if (line != string.Empty)
                        sw.WriteLine(line);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        const int nColumn = 4;

        string OutputStringFormat(double value)
        {
            return " " + value.ToString();
            //return " " + string.Format("{0,18}", value);
        }









        /*
            Parallel.For(0, ny, j =>
            {
            });
        */




        public void Apply(Grid grid, int index)
        {
            ModifiersGroup mgroup = this.Groups[index];
            int dz = mgroup.Modifiers.Count();
            mgroup.Applied = !mgroup.Applied;
            for (int k = 0; k < dz; ++k)
                if (mgroup.Modifiers[k].Use)
                    this.Apply(grid, mgroup.Modifiers[k], mgroup.Applied);
        }



        public void Apply(Grid grid, Modifier modifier, bool apply)
        {
            double c = (modifier.Value - 1) / modifier.Radius;
            int NX = grid.NX(), NY = grid.NY(), NZ = grid.NZ();
            double x = modifier.Location.X;
            double y = modifier.Location.Y;
            double r = modifier.Radius;
            int layer = modifier.Layer;
            for (int i = 0; i < NX; ++i)
            {
                Parallel.For(0, NY, j =>
                {
                    Point3D center = grid.LocalCell(i, j, 0).MiddleTopFace().Center();
                    double dx = x - center.X;
                    double dy = y - center.Y;
                    double d = Math.Pow(dx * dx + dy * dy, 0.5);
                    if (d <= r)
                    {
                        double value = (c * (r - d) + 1);
                        if (apply)  this.Values[i, j, layer] *= value;
                        else        this.Values[i, j, layer] /= value;
                    }
                });
            }
            if (this.Scale.Auto) UpdateScale();
        }



        /*
        public void Apply(Grid grid, int index)
        {
            Modifier modifier = this.Modifiers[index];
            float c = (modifier.Value - 1) / modifier.Radius;
            int NX = grid.NX(), NY = grid.NY(), NZ = grid.NZ();
            if (!modifier.Applied)
            {
                for (int i = 0; i < NX; ++i)
                    for (int j = 0; j < NY; ++j)
                    {
                        Point3D center = grid.LocalCell(i, j, 0).MiddleTopFace().Center();
                        double x = modifier.Location.X - center.X;
                        double y = modifier.Location.Y - center.Y;
                        float d = (float)Math.Pow(x * x + y * y, 0.5);
                        if (d <= modifier.Radius)
                            foreach (int k in modifier.Layers)
                                this.Values[i, j, k] *= (c * (modifier.Radius - d) + 1);
                    }
            }
            else
            {
                for (int i = 0; i < NX; ++i)
                    for (int j = 0; j < NY; ++j)
                    {
                        Point3D center = grid.LocalCell(i, j, 0).MiddleTopFace().Center();
                        double x = modifier.Location.X - center.X;
                        double y = modifier.Location.Y - center.Y;
                        float d = (float)Math.Pow(x * x + y * y, 0.5);
                        if (d <= modifier.Radius)
                            foreach (int k in modifier.Layers)
                                this.Values[i, j, k] /= (c * (modifier.Radius - d) + 1);
                    }
            }
            modifier.Applied = !modifier.Applied;
        }
        */



        public void AddAndApply(Grid grid, ModifiersGroup m)
        {
            this.Groups.Add(m);
            Apply(grid, Groups.Count - 1);
        }
        







    }
}
