using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikingFS
{
    class IncrementalFileSystem
    {
        private string filepath;
        private StringBuilder modifs;
        public IncrementalFileSystem(string _filePath)
        {
            if (!System.IO.File.Exists(_filePath))
                System.IO.File.WriteAllLines(_filePath, new string[] { "0" });
            this.filepath = _filePath;
            modifs = new StringBuilder();
        }

        public void AddChange(string s)
        {
            modifs.AppendLine(s);
        }

        public void AddChange(string key, string value)
        {
            modifs.AppendLine(key + "=" + value);
        }

        public void Commit()
        {
            long lastLine = Convert.ToInt64(System.IO.File.ReadLines(this.filepath).Last());
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(this.filepath, true))
            {
                file.Write(modifs.ToString());
                modifs.Clear();
                file.WriteLine((lastLine + 1).ToString());
            }
        }

        public Dictionary<string, string> GetValues()
        {
            Dictionary<string, string> dict = new Dictionary<string,string>();
            using (System.IO.StreamReader file = new System.IO.StreamReader(this.filepath))
            {
                while (true)
                {
                    string line = file.ReadLine();
                    if (line == null)
                        break;
                    
                    try { Convert.ToInt64(line); }
                    catch(Exception e) {
                        string[] liste = line.Split('=');
                        dict[liste[0]] = liste[1];
                    }
                }
            }
            return dict;
        }
    }
}
