using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;




namespace GeoEdit
{
    [Serializable]
    public class MapAxes
    {


        public MapAxes()
        {
            Init();
        }



        public MapAxes(string file, FileType type)
        {
            Init();
            Read(file, type);
        }



        public MapAxes(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            X3 = x3;
            Y3 = y3;
        }


        public double X1 { set; get; }
        public double Y1 { set; get; }
        public double X2 { set; get; }
        public double Y2 { set; get; }
        public double X3 { set; get; }
        public double Y3 { set; get; }



        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(X1);
            writer.Write(Y1);
            writer.Write(X2);
            writer.Write(Y2);
            writer.Write(X3);
            writer.Write(Y3);
        }



        public static MapAxes Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch(version)
            {
                case Version0:
                    return new MapAxes(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(),
                               reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
                default:
                    return new MapAxes();
            }
            
        }





        void Init()
        {
            X1 = 0;
            Y1 = 0;
            X2 = 0;
            Y2 = 0;
            X3 = 0;
            Y3 = 0;
        }




        public void Clear()
        {
            Init();
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





        const string grdecl_kw_mapaxes = "MAPAXES";
        bool ReadFromGRDECL(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (GRDECLReader.ClearLine(line) == grdecl_kw_mapaxes)
                            break;
                    List<string> values = new List<string>();
                    const int valuesNeeded = 6;
                    while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null)
                    {
                        line = GRDECLReader.ClearLine(line);
                        if (line != string.Empty)
                            foreach (string word in line.Split())
                                values.Add(word);
                    }
                    X1 = double.Parse(values[0]);
                    Y1 = double.Parse(values[1]);
                    X2 = double.Parse(values[2]);
                    Y2 = double.Parse(values[3]);
                    X3 = double.Parse(values[4]);
                    Y3 = double.Parse(values[5]);
                    return true;
                }
            }
            catch (Exception)
            {
                Init();
                return false;
            }
        }





        bool ReadFromCMG(string file)
        {
            Init();
            return true;
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
                using (StreamWriter sw = new StreamWriter(file,true))
                {
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(grdecl_kw_mapaxes);
                    sw.WriteLine(X1.ToString() + " " + Y1.ToString() + " " + 
                                 X2.ToString() + " " + Y2.ToString() + " " + 
                                 X3.ToString() + " " + Y3.ToString() + " /");
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
