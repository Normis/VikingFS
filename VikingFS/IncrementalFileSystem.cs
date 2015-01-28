using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikingFS
{
    class IncrementalFileSystem
    {
        public string FolderPath {get; private set;}
        public string FileName {get; private set;}

        private const string extension = ".incb";
        private StringBuilder modifs;//Could/should be change with file access
        public IncrementalFileSystem(string _folderPath, string _fileName, string previousBranch = "", long previousRevision = -1)
        {
            this.FolderPath = _folderPath;
            this.FileName = _fileName;
            if (!System.IO.File.Exists(GetFullPath()))
            {
                if(previousBranch == "")
                    System.IO.File.WriteAllLines(GetFullPath(), new string[] { "0" });
                else
                {
                    if(previousRevision == -1)
                        previousRevision = Convert.ToInt64(System.IO.File.ReadLines(GetFullPath()).Last());//can't branch at 0, anyway, what kind of dumbass would do that?
                    System.IO.File.WriteAllLines(GetFullPath(), new string[] { "0<-" + FileName + "<-" + previousRevision });
                }
            }
                
            modifs = new StringBuilder();
        }

        public string GetFullPath()
        {
            return FolderPath + "\\" + FileName + extension;
        }

        public string GetPath()
        {
            return FileName + extension;
        }

        public void Commit(string s)
        {
            modifs.AppendLine(s);
        }

        public void Commit(string key, string value)
        {
            modifs.AppendLine(key + "=" + value);
        }

        public void Push()
        {
            long lastLine = Convert.ToInt64(System.IO.File.ReadLines(GetFullPath()).Last().Split(new string[]{ "<-" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(GetFullPath(), true))
            {
                file.Write(modifs.ToString());
                modifs.Clear();
                file.WriteLine((lastLine + 1).ToString());
            }
        }

        public Dictionary<string, string> GetValues(long revision = -1 )
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(GetFullPath()))
            {
                string first = file.ReadLine();//did not check if empty, I should have
                string[] s = first.Split(new string[] { "<-" }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> dict = (s.Length > 1)
                            ? (new IncrementalFileSystem(FolderPath, s[1])).GetValues(Convert.ToInt64(s[2]))
                            : new Dictionary<string, string>();

                while (true)
                {
                    string line = file.ReadLine();
                    if (line == null)
                        break;

                    try 
                    {
                        long v = Convert.ToInt64(line);
                        if (revision > 0 && v > revision)
                            break;
                    }
                    catch (Exception e)
                    {
                        string[] liste = line.Split('=');
                        dict[liste[0]] = liste[1];
                    }
                }

                return dict;
            }
        }
    }
}
