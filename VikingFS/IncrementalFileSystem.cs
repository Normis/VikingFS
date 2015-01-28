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
        private string FileName {get; private set;}

        private const string extension = ".incb";
        private StringBuilder modifs;//Could/should be change with file access
        public IncrementalFileSystem(string _folderPath, string _fileName)
        {
            this.FolderPath = _folderPath;
            this.FileName = _fileName;
            if (!System.IO.File.Exists(GetFile()))
                System.IO.File.WriteAllLines(GetFile(), new string[] { "0" });
            modifs = new StringBuilder();
        }

        private string GetFile()
        {
            return FolderPath + "\\" + FileName + extension;
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
            long lastLine = Convert.ToInt64(System.IO.File.ReadLines(GetFile()).Last().Split(new string[]{ "<-" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(GetFile(), true))
            {
                file.Write(modifs.ToString());
                modifs.Clear();
                file.WriteLine((lastLine + 1).ToString());
            }
        }

        public Dictionary<string, string> GetValues(long revision = -1 )
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(GetFile()))
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

        public TaxprepT2Com2014V2.Taxprep2014T2Return Update(long revision = -1)
        {
            return Checkout(FileName, revision);
        }

        public TaxprepT2Com2014V2.Taxprep2014T2Return Checkout(string branch, long revision = -1)
        {
            TaxprepT2Com2014V2.Taxprep2014T2Return taxreturn = new TaxprepT2Com2014V2.Taxprep2014T2Return();

            foreach (var v in new IncrementalFileSystem(FolderPath, branch).GetValues(revision))
                taxreturn.SetCellValue(v.Key, v.Value);  //erh what if not string?

            return taxreturn;
        }

        public IncrementalFileSystem Branch(string branchName)
        {
            //Replace with something else later, ain't nobody got time for that!
            //System.Diagnostics.Debug.Assert(!System.IO.File.Exists(folderPath + "\\" + branchName + extension));
            long lastLine = Convert.ToInt64(System.IO.File.ReadLines(GetFile()).Last());//can't branch at 0, anyway, what kind of dumbass would do that?

            System.IO.File.WriteAllLines(FolderPath + "\\" + branchName + extension, new string[] { "0<-" + FileName + "<-" + lastLine });
            return new IncrementalFileSystem(FolderPath, branchName);
        }

    }
}
