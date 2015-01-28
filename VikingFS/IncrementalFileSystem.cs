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
        public IncrementalFileSystem(string filePath)
        {
            if (!System.IO.File.Exists(filepath))
                System.IO.File.WriteAllText(filepath, "0\n");
            this.filepath = filepath;
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
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt"))
            {
                file.Write(modifs.ToString());
                modifs.Clear();
                file.WriteLine(lastLine.ToString());
            }   
        }
    }
}
