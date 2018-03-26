﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;




namespace GeoEdit
{
    [Serializable]
    public class ModifiersGroup
    {
        public ModifiersGroup()
        {
            Title = "Modifier_" + (Number++).ToString();
            Modifiers = new Modifier[0];
            Applied = false;
        }
        public ModifiersGroup(string title, Point2D location, double radius, double value, int nz, bool applied = false)
        {
            Title = title;
            Applied = applied;
            Modifiers = new Modifier[nz];
            for (int k = 0; k < nz; ++k)
                Modifiers[k] = new Modifier(location, k, value, radius);
        }
        public ModifiersGroup(string title, Modifier[] modifiers, bool applied = false)
        {
            Title = title;
            Applied = applied;
            Modifiers = modifiers;
        }
        /*
        ModifiersGroup(string[] split)
        {
            Title = split[0];
            Value = float.Parse(split[1]);
            Radius = float.Parse(split[2]);
            Location = new PointF(float.Parse(split[3]), float.Parse(split[4]));
            string array = string.Empty;
            for (int i = 5; i < split.Count(); ++i) array += split[i];
            Layers = IntArray(array);
            Applied = false;
        }
        */



        public string Title { set; get; }
        public Modifier[] Modifiers { set; get; }
        public bool Applied { set; get; }







        static long Number = 1;



        const byte Version0 = 0;
        public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Title);
            writer.Write(Applied);
            writer.Write(Modifiers.Count());
            foreach (Modifier m in Modifiers)
                m.Write(writer);
        }



        static public ModifiersGroup Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch (version)
            {
                case Version0:
                    {
                        string title = reader.ReadString();
                        bool applied = reader.ReadBoolean();
                        int count = reader.ReadInt32();
                        Modifier[] modifiers = new Modifier[count];
                        for (int i = 0; i < count; ++i)
                            modifiers[i] = Modifier.Read(reader);
                        return new ModifiersGroup(title, modifiers, applied);
                    }
                default:
                    return new ModifiersGroup();
            }
        }


        
        public const string TextHeader = " --name\tvalue\trad\tx\ty\tk";


        string TextLayers(int[] array)
        {
            string result = string.Empty;
            foreach(int i in array)
            {
                result += (i + 1).ToString() + ',';
            }
            return result.Substring(0, result.Count() - 1);
        }

        

        int[] IntArray(string text)
        {
            List<int> result = new List<int>();
            string clear = string.Empty;
            foreach (char ch in text) { if (ch != ' ') clear += ch; }
            foreach (string part in clear.Split(','))
            {
                string[] interval = part.Split('-');
                if (interval.Count() == 1)
                {
                    result.Add(int.Parse(interval[0]) - 1);
                }
                else if (interval.Count() == 2)
                {
                    int k1 = int.Parse(interval[0]) - 1, k2 = int.Parse(interval[1]);
                    for (int k = k1; k < k2; ++k)
                        result.Add(k);
                }
            }
            result.Sort();
            return result.ToArray();
        }


        /*
        public static List<ModifiersGroup> Read(string filename)
        {
            List<ModifiersGroup> result = new List<ModifiersGroup>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(filename, Encoding.GetEncoding(1251));
                foreach (string line in lines)
                {
                    string clearline = GRDECLReader.ClearLine(line);
                    if (clearline == string.Empty)
                        continue;
                    string[] split = clearline.Split(' ');
                    if (split.Count() < 6)
                        continue;
                    result.Add(new ModifiersGroup(split));
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        */



    }
}
