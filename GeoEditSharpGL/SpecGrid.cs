using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;



namespace GeoEdit
{

    [Serializable]
    public class SpecGrid
    {
        public SpecGrid()
        {
            Init();
        }
        public SpecGrid(int nx, int ny, int nz, int numRes = 1, char coordType = CartesianCoordType)
        {
            Init();
            NX = nx;
            NY = ny;
            NZ = nz;
            NumRes = numRes;
            CoordType = coordType;
        }
        public SpecGrid(string file, FileType type)
        {
            Init();
            Read(file, type);
        }



        public int NX { set; get; }
        public int NY { set; get; }
        public int NZ { set; get; }
        public int NumRes { set; get; }
        public char CoordType { set; get; }






        const int defNX = 0;
        const int defNY = 0;
        const int defNZ = 0;
        const int defNumRes = 0;
        public const char DefCoordType = 'U';
        public const char RadialCoordType = 'T';
        public const char CartesianCoordType = 'F';






        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(NX);
            writer.Write(NY);
            writer.Write(NZ);
            writer.Write(NumRes);
            writer.Write(CoordType);
        }


        public static SpecGrid Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    {
                        int nx = reader.ReadInt32();
                        int ny = reader.ReadInt32();
                        int nz = reader.ReadInt32();
                        int numRes = reader.ReadInt32();
                        char coordType = reader.ReadChar();
                        return new SpecGrid(nx, ny, nz, numRes, coordType);
                    }
                default:
                    return new SpecGrid();
            }
        }
        







        void Init()
        {
            NX = defNX;
            NY = defNY;
            NZ = defNZ;
            NumRes = defNumRes;
            CoordType = DefCoordType;
        }




        public void Clear()
        {
            Init();
        }



        public bool Specified()
        {
            return NX != defNX &&
                   NY != defNY &&
                   NZ != defNZ &&
                   NumRes != defNumRes &&
                   CoordType != DefCoordType;
        }






        public bool Read(string file, FileType type)
        {
            switch (type)
            {
                case FileType.CMG_ASCII:
                    return ReadFromCMG(file);
                case FileType.GRDECL_ASCII:
                    return ReadFromGRDECL(file);
                default:
                    return false;
            }
        }

        



        const string grdecl_kw_specgrid = "SPECGRID";
        bool ReadFromGRDECL (string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (GRDECLReader.ClearLine(line) == grdecl_kw_specgrid)
                        {
                            line = sr.ReadLine();
                            line = GRDECLReader.ClearLine(line);
                            string[] split = line.Split();
                            NX = int.Parse(split[0]);
                            NY = int.Parse(split[1]);
                            NZ = int.Parse(split[2]);
                            NumRes = int.Parse(split[3]);
                            CoordType = char.Parse(split[4]);
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
            return false;
        }





        const string cmg_kw_specgrid = "*GRID";
        bool ReadFromCMG(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] split = CMGReader.ClearLine(line).Split();
                        if (split.Count() != 0 && split[0] == cmg_kw_specgrid)
                        {
                            NX = int.Parse(split[2]);
                            NY = int.Parse(split[3]);
                            NZ = int.Parse(split[4]);
                            NumRes = 1;
                            CoordType = CartesianCoordType;
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
            return false;
        }





        public bool Write(string file, FileType type)
        {
            switch (type)
            {
                /*
                case FileType.CMG_ASCII:
                    return WriteFromCMG(file);
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
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(grdecl_kw_specgrid);
                    sw.WriteLine(NX.ToString() + " " + NY.ToString() + " " + NZ.ToString() + " 1 F /");
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("COORDSYS");
                    sw.WriteLine("1 " + NZ + " /");
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
