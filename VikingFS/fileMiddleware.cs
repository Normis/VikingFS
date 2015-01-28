using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace VikingFS
{
    public class fileMiddleware
    {
        private string filePath;
        public fileMiddleware(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                System.IO.File.WriteAllText(filePath, "");
            this.filePath = filePath;
        }

        private Dictionary<string, string> deserializeData()
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            if (fs.Length == 0)
            {
                fs.Close();
                return new Dictionary<string, string>();
            }

            BinaryFormatter formatter = new BinaryFormatter();
            Dictionary<string, string> dict = null;
            dict = (Dictionary<string, string>)formatter.Deserialize(fs);
            fs.Close();
            return dict;
        }

        private void serializeData(Dictionary<string, string> data)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, data);
            fs.Close();
        }

        public string getPath(string id)
        {
            var dict = deserializeData();
            return dict[id];
        }

        public void addNewFile(string id, string fullPath) 
        {
            var dict = deserializeData();
            dict[id] = fullPath;
            serializeData(dict);
        }
    }
}
