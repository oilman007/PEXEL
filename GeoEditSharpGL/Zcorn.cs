using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


/*
 *  The keyword must be followed by
    2 * NDX * 2 * NDY * 2 * NDZ
    values, where NDX, NDY, NDZ are the dimensions of the current box. For cell i, the 8 ZCORN
    values are zi,1, zi,2, zi,3, zi,4, zi,5, zi,6, zi,7, zi,8. Here values 1-4 are for the top face, with zi,1 on
    the near left corner, zi,2 on the near right corner, zi,3 on the far left corner, and zi,4, on the far
    right corner. Values 5-8 have the same function for the bottom face. Then the arrangement of
    the ZCORN values within this range are:
        • for the first row of NDX cells, input the near top values z1,1, z1,2, z2,1, z2,2,..., zi,1, zi,2,...
        zNDX,1, zNDX,2, followed by the far top values z1,3, z1,4, z2,3, z2,4,..., zi,3, zi,4,... Z.NDX,3,
        zNDX,4.
        • Repeat for each subsequent row of NDX cells in the top plane.
        • Now repeat the last two steps for the bottom values of the top plane.
        • Finally, repeat all previous steps for each plane in the grid.
    The resulting data will lie on the grid constructed by the corner points in space, with “touching”
    corners of adjacent cells separated by a small margin. The separation is only for visualizing the
    grid, and does not affect the ZCORN values.
 * */


namespace GeoEdit
{
    [Serializable]
    public class Zcorn
    {



        public Zcorn()
        {
            Init();
        }



        public Zcorn(SpecGrid specGrid, string file, FileType type)
        {
            Init();
            Read(specGrid, file, type);
        }




        public Zcorn(int nx, int ny, int nz, double zSize, double depth)
        {
            Init(nx, ny, nz);
            double z = depth;
            for (int k = 0; k < nz; ++k)
            {
                double zTop = z;
                double zBottom = z + zSize;
                for (int j = 0; j < ny; ++j)
                {
                    for (int i = 0; i < nx; ++i)
                    {
                        Items[i, j, k] = new ZcornItem(zTop, zTop, zTop, zTop, zBottom, zBottom, zBottom, zBottom);
                    }
                }
                z += zSize;
            }
        }



        public Zcorn(ZcornItem[,,] items)
        {
            this.Items = items;
        }





        public ZcornItem[,,] Items { set; get; }






        const byte Version0 = 0;
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
                        Items[i, j, k].Write(writer);
        }



        public static Zcorn Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    {
                        int icount = reader.ReadInt32();
                        int jcount = reader.ReadInt32();
                        int kcount = reader.ReadInt32();
                        ZcornItem[,,] items = new ZcornItem[icount, jcount, kcount];
                        for (int i = 0; i < icount; ++i)
                            for (int j = 0; j < jcount; ++j)
                                for (int k = 0; k < kcount; ++k)
                                    items[i, j, k] = ZcornItem.Read(reader);
                        return new Zcorn(items);
                    }
                default:
                    return new Zcorn();
            }
        }




        void Init()
        {
            Items = new ZcornItem[0, 0, 0];
        }



        void Init(int nx, int ny, int nz)
        {
            Items = new ZcornItem[nx, ny, nz];
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Items[i, j, k] = new ZcornItem();
        }




        public bool Specified()
        {
            return NX() != 0 && NY() != 0 && NZ() != 0;
        }



        public int NX()
        {
            return Items.GetLength(0);
        }

        public int NY()
        {
            return Items.GetLength(1);
        }

        public int NZ()
        {
            return Items.GetLength(2);
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




        const string grdecl_kw_zcorn = "ZCORN";
        bool ReadFromGRDECL(SpecGrid specGrid, string file)
        {
            double[] values = GRDECLReader.Array(file, grdecl_kw_zcorn);
            if (values.Count() == 0)
                return false;
            if (values.Count() != 2 * specGrid.NX * 2 * specGrid.NY * 2 * specGrid.NZ)
                return false;
            Init(specGrid.NX, specGrid.NY, specGrid.NZ);
            int n = 0;
            int c_last = 2;
            int jloop = 1;
            int kloop = 1;
            for (int k = 0; k < specGrid.NZ; ++k)
            {
                for (int j = 0; j < specGrid.NY; ++j)
                {
                    for (int i = 0; i < specGrid.NX; ++i)
                    {
                        for (int c = c_last - 2; c < c_last; ++c)
                        {
                            Items[i, j, k].Corners[c] = values[n++];
                        }
                    }
                    if (jloop == 1)
                    {
                        jloop = 2;
                        c_last += 2;
                        --j;
                    }
                    else
                    {
                        jloop = 1;
                        c_last -= 2;
                    }
                }
                if (kloop == 1)
                {
                    kloop = 2;
                    c_last += 4;
                    --k;
                }
                else
                {
                    kloop = 1;
                    c_last -= 4;
                }
            }
            return true;
        }



        const string cmg_kw_zcorn = "*ZCORN";
        bool ReadFromCMG(SpecGrid specGrid, string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Items = new ZcornItem[specGrid.NX, specGrid.NY, specGrid.NZ];
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (CMGReader.ClearLine(line) == cmg_kw_zcorn)
                            break;
                    List<string> values = new List<string>();
                    int valuesNeeded = 2 * specGrid.NX;
                    int n = 0;
                    int c_last = 2;
                    int jloop = 1;
                    int kloop = 1;
                    for (int k = 0; k < specGrid.NZ; ++k)
                    {
                        for (int j = 0; j < specGrid.NY; ++j)
                        {
                            while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null) // add
                            {
                                line = CMGReader.ClearLine(line);
                                if (line != string.Empty)
                                    foreach (string word in line.Split())
                                        values.Add(word);
                            }
                            for (int i = 0; i < specGrid.NX; ++i)
                            {
                                for (int c = c_last - 2; c < c_last; ++c)
                                {
                                    Items[i, j, k].Corners[c] = double.Parse(values[n++]);
                                }
                            }
                            values.RemoveRange(0, valuesNeeded); // remove
                            n = 0;
                            if (jloop == 1)
                            {
                                jloop = 2;
                                c_last += 2;
                                --j;
                            }
                            else
                            {
                                jloop = 1;
                                c_last -= 2;
                            }
                        }
                        if (kloop == 1)
                        {
                            kloop = 2;
                            c_last += 4;
                            --k;
                        }
                        else
                        {
                            kloop = 1;
                            c_last -= 4;
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
        public bool Fill(int nx, int ny, int nz, List<string> data)
        {
            if (2 * nx * 2 * ny * 2 * nz != data.Count)
                return false;
            Values = new float[nx, ny, nz, 8];
            int n = 0;
            int c_last = 2;
            int jloop = 1;
            int kloop = 1;
            for (int k = 0; k < nz; ++k)
            {
                for (int j = 0; j < ny; ++j)
                {
                    for (int i = 0; i < nx; ++i)
                        for (int c = c_last - 2; c < c_last; ++c)
                            Values[i, j, k, c] = float.Parse(data[n++]);
                    if (jloop == 1)
                    {
                        jloop = 2;
                        c_last += 2;
                        --j;
                    }
                    else
                    {
                        jloop = 1;
                        c_last -= 2;
                    }
                }
                if (kloop == 1)
                {
                    kloop = 2;
                    c_last += 4;
                    --k;
                }
                else
                {
                    kloop = 1;
                    c_last -= 4;
                }
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
                using (StreamWriter sw = new StreamWriter(file, true))
                {
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(grdecl_kw_zcorn);
                    int nx = NX(), ny = NY(), nz = NZ();
                    int c_last = 2;
                    int jloop = 1;
                    int kloop = 1;
                    int column = 1;
                    string line = string.Empty;
                    for (int k = 0; k < nz; ++k)
                    {
                        for (int j = 0; j < ny; ++j)
                        {
                            for (int i = 0; i < nx; ++i)
                            {
                                for (int c = c_last - 2; c < c_last; ++c)
                                {
                                    line += " " + Items[i, j, k].Corners[c].ToString();
                                    if (++column > 4)
                                    {
                                        sw.WriteLine(line);
                                        column = 1;
                                        line = string.Empty;
                                    }
                                }
                            }
                            if (jloop == 1)
                            {
                                jloop = 2;
                                c_last += 2;
                                --j;
                            }
                            else
                            {
                                jloop = 1;
                                c_last -= 2;
                            }
                        }
                        if (kloop == 1)
                        {
                            kloop = 2;
                            c_last += 4;
                            --k;
                        }
                        else
                        {
                            kloop = 1;
                            c_last -= 4;
                        }
                    }
                    if (line.Count() > 0)
                        sw.WriteLine(line);
                    sw.WriteLine("/");
                }
            }
            catch
            {
                return false;
            }
            return true;
        }









    }
}
