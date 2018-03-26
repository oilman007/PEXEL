using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;




namespace GeoEdit
{

    [Serializable]
    public class Actnum
    {

        


        public Actnum()
        {
            Init();
        }



        public Actnum(SpecGrid specGrid, string file, FileType type)
        {
            Init();
            Read(specGrid, file, type);
        }




        public Actnum(int nx, int ny, int nz)
        {
            Init(nx, ny, nz);
        }



        public Actnum(bool[,,] values)
        {
            int nx = values.GetLength(0);
            int ny = values.GetLength(1);
            int nz = values.GetLength(2);
            Init(nx, ny, nz);
            Values = values;
        }





        public bool[,,] Values { set; get; }


        const byte Version0 = 0;
        Version v = new Version();
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            int icount = NX();
            int jcount = NY();
            int kcount = NZ();
            writer.Write(icount);
            writer.Write(jcount);
            writer.Write(kcount);
            for (int i = 0; i < icount; ++i)
                for (int j = 0; j < jcount; ++j)
                    for (int k = 0; k < kcount; ++k)
                        writer.Write(Values[i, j, k]);
        }



        public static Actnum Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch (version)
            {
                case Version0:
                    {
                        int nx = reader.ReadInt32();
                        int ny = reader.ReadInt32();
                        int nz = reader.ReadInt32();
                        bool[,,] values = new bool[nx, ny, nz];
                        for (int i = 0; i < nx; ++i)
                            for (int j = 0; j < ny; ++j)
                                for (int k = 0; k < nz; ++k)
                                    values[i, j, k] = reader.ReadBoolean();
                        return new Actnum(values);
                    }
                default:
                    return new Actnum();
            }
        }




        void Init()
        {
            Values = new bool[0, 0, 0];
        }




        void Init(int nx, int ny, int nz)
        {
            Values = new bool[nx, ny, nz];
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Values[i, j, k] = true;
        }




        public void Clear()
        {
            Init();
        }




        public bool Specified()
        {
            return NX() != 0 && NY() != 0 && NZ() != 0;
        }










        public bool Read(SpecGrid specGrid, string file, FileType type)
        {
            switch (type)
            {
                case FileType.CMG_ASCII:
                    return ReadFromCMG(specGrid, file);
                case FileType.GRDECL_ASCII:
                    return ReadFromGRDECL(specGrid, file);
                default:
                    return false;
            }
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






        const string grdecl_kw_actnum = "ACTNUM";
        bool ReadFromGRDECL(SpecGrid specGrid, string file)
        {
            double[] values = GRDECLReader.Array(file, grdecl_kw_actnum);
            if (values.Count() == 0)
                return false;
            if (values.Count() != specGrid.NX * specGrid.NY * specGrid.NZ)
                return false;
            Init(specGrid.NX, specGrid.NY, specGrid.NZ);
            int n = 0;
            for (int k = 0; k < specGrid.NZ; ++k)
            {
                for (int j = 0; j < specGrid.NY; ++j)
                {
                    for (int i = 0; i < specGrid.NX; ++i)
                    {
                        Values[i, j, k] = (values[n++] == 0f) ? false : true;
                    }
                }
            }
            return true;
        }


        const string cmg_kw_actnum = "*NULL *ALL";
        bool ReadFromCMG(SpecGrid specGrid, string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Values = new bool[specGrid.NX, specGrid.NY, specGrid.NZ];
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (CMGReader.ClearLine(line) == cmg_kw_actnum)
                            break;
                    List<string> values = new List<string>();
                    int valuesNeeded = specGrid.NX;
                    int n = 0;
                    for (int k = 0; k < specGrid.NZ; ++k)
                    {
                        for (int j = 0; j < specGrid.NY; ++j)
                        {
                            while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null)
                            {
                                line = CMGReader.ClearLine(line);
                                if (line != string.Empty)
                                    foreach (string word in line.Split())
                                        values.Add(word);
                            }
                            for (int i = 0; i < specGrid.NX; ++i)
                            {
                                int value = int.Parse(values[n++]);
                                Values[i, j, k] = (value == 0) ? false : true;
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
                Init();
                return false;
            }
        }

        /*
        public bool Fill(int dx, int dy, int dz, List<string> data)
        {
            if (dx * dy * dz != data.Count)
                return false;
            Values = new bool[dx, dy, dz];
            int n = 0;
            for (int k = 0; k < dz; ++k)
                for (int j = 0; j < dy; ++j)
                    for (int i = 0; i < dx; ++i)
                    {
                        int value = int.Parse(data[n++]);
                        Values[i, j, k] = (value == 0) ? false : true;
                    }
            return true;
        }
         * */







        public bool Write(string file, FileType type)
        {
            switch (type)
            {
                /*
                case FileType.CMG_ASCII:
                    return ReadFromCMG(specGrid, file);
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
                using (StreamWriter sw = new StreamWriter(file,true))
                {
                    int column = 1;
                    string line = string.Empty;
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(grdecl_kw_actnum);
                    int nx = NX(), ny = NY(), nz = NZ();
                    for (int k = 0; k < nz; ++k)
                        for (int j = 0; j < ny; ++j)
                            for (int i = 0; i < nx; ++i)
                            {
                                line += Values[i, j, k] ? " 1" : " 0";
                                if (++column > 20)
                                {
                                    sw.WriteLine(line);
                                    column = 1;
                                    line = string.Empty;
                                }
                            }
                    if (line.Count() > 0)
                        sw.WriteLine(line);
                    sw.WriteLine("/");
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }





    }
}
