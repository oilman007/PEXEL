using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace GeoEdit
{
    [Serializable]
    public class Project
    {
        public Project()
        {
            Models = new List<Model>();
        }

        public List<Model> Models { set; get; }
        //public List<Well> Wells { set; get; }






        void Init(Project other)
        {
            this.Models = other.Models;
        }


        /*
        public bool Save(string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Create))
                {
                    BinaryFormatter formater = new BinaryFormatter();
                    formater.Serialize(stream, this);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        
        public bool Load(string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Open))
                {
                    BinaryFormatter formater = new BinaryFormatter();
                    Project other = (Project)formater.Deserialize(stream);
                    Init(other);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        */






        
        const int Magic = 210566;
        const byte Version0 = 0;

        public bool Save(string file)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(file, FileMode.Create)))
                {
                    writer.Write(Magic);
                    writer.Write(Version0);
                    writer.Write(Models.Count());
                    foreach (Model m in Models)
                        m.Write(writer);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }






        public bool Load(string file)
        {
            Project project = new Project();
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    int magic = reader.ReadInt32();
                    if (magic != Magic)
                        return false;
                    byte version = reader.ReadByte();
                    switch(version)
                    {
                        case Version0:
                            {
                                int nm = reader.ReadInt32();
                                for (int m = 0; m < nm; ++m) project.Models.Add(Model.Read(reader));
                                this.Init(project);
                                return true;
                            }
                        default:
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        

        

    }
}
