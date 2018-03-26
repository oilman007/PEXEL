using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace GeoEdit
{
    public class GRDECLReader
    {



        const string remString = "--";
        const string tabString = "\t";
        const string singlSpace = " ";
        const string doubleSpace = "  ";
        const string terminator = "/";
        const char repeator = '*';


        public static string ClearLine(string line)
        {
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


        public static string[] ClearWords(string line)
        {
            string cline = ClearLine(line);
            if (cline == string.Empty)
                return new string[0];
            string[] clearwords = cline.Split();
            List<string> result = new List<string>();
            foreach(string word in clearwords)
            {
                if (!word.Contains(repeator))
                    result.Add(word);
                else
                {
                    string[] split = word.Split(repeator);
                    if (split.Count() == 1)
                        result.Add(word);
                    else
                    {
                        int count = int.Parse(split[0]);
                        for (int i = 0; i < count; ++i)
                            result.Add(split[1]);
                    }
                }
            }
            return result.ToArray();
        }



        public static double[] Array(string file, string kw)
        {
            try
            {
                List<double> result = new List<double>();
                using (StreamReader sr = new StreamReader(file))
                {
                    kw = ClearLine(kw);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (GRDECLReader.ClearLine(line) == kw)
                            break;
                    bool stop = false;
                    while (!stop && (line = sr.ReadLine()) != null)
                    {
                        string[] words = GRDECLReader.ClearWords(line);
                        foreach (string word in words)
                        {
                            if (word == terminator)
                            {
                                stop = true;
                                break;
                            }
                            else
                                result.Add(double.Parse(word));
                        }
                    }
                }
                return result.ToArray();
            }
            catch (Exception)
            {
                return new double[0];
            }
        }



        public static List<string> KWContains(string file)
        {
            List<string> r = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = ClearLine(line);
                        if (line == string.Empty)
                            continue;
                        string[] split = line.Split();
                        if (split.Count() != 1)
                            continue;
                        if (split[0] == "/")
                            continue;
                        r.Add(line);
                    }
                }
            }
            catch (Exception)
            {
                r.Clear();
                return r;
            }
            return r;
        }






        public List<string> ClearLines(string filename)
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



        List<string> Parse(string kw, List<string> lines)
        {
            List<string> r = new List<string>();
            int i = 0;
            while (lines[i] != kw)
                if (++i == lines.Count)
                    return r;
            for (; i < lines.Count; ++i)
            {
                string[] split = lines[i].Split();
                foreach (string word in split)
                {
                    if (word == terminator)
                        return r;
                    if (word.Contains(repeator))
                    {
                    }
                    else
                        r.Add(word);
                }
            }
            return r;
        }


    }
}
