using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace GeoEdit
{
    public class Compdat
    {
        public Compdat()
        {
            Title = string.Empty;
            Connections = new List<Index3D>();
            Checked = false;
        }
        public Compdat(string title, List<Index3D> connections, bool check)
        {
            Title = title;
            Connections = connections;
            Checked = check;
        }


        public string Title { set; get; }
        public List<Index3D> Connections { set; get; }
        public bool Checked { set; get; }






        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Title);
            writer.Write(Connections.Count());
            foreach (Index3D c in Connections)
                c.Write(writer);
            writer.Write(Checked);
        }


        public static Compdat Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch (version)
            {
                case Version0:
                    {
                        string title = reader.ReadString();
                        int count = reader.ReadInt32();
                        List<Index3D> cs = new List<Index3D>();
                        for (int c = 0; c < count; ++c)
                            cs.Add(Index3D.Read(reader));
                        bool check = reader.ReadBoolean();
                        return new Compdat(title, cs, check);
                    }
                default:
                    return new Compdat();
            }
        }











        static public List<Compdat> Read(string file)
        {
            Dictionary<string, Compdat> result = new Dictionary<string, Compdat>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(file, Encoding.GetEncoding(1251));                
                foreach (string line in lines)
                {
                    string cline = ClearLine(line, "--");
                    if (cline == string.Empty) continue;
                    string[] split = cline.Split();
                    if (split.Count() != 4) continue;
                    string title = split[0];
                    int i = int.Parse(split[1]) - 1;
                    int j = int.Parse(split[2]) - 1;
                    int k = int.Parse(split[3]) - 1;
                    if (result.ContainsKey(title)) result[title].Connections.Add(new Index3D(i, j, k));
                    else result.Add(title, new Compdat(title, new List<Index3D>() { new Index3D(i, j, k) }, true));
                }
            }
            catch
            {
                return result.Values.ToList();
            }
            return result.Values.ToList();
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













    }
}
