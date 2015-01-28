using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikingFS
{
    class FieldComparer
    {
        Dictionary<string, string> comFile;
        Dictionary<string, string> incrFile;
        fileMiddleware fm;
        IncrementalFileSystem currentIFS;
        public FieldComparer(string comFilePath, string middlewareFilePath, string ifsFolderPath, string ifsFileName)
        {
            fm = new fileMiddleware(middlewareFilePath);
            currentIFS = new IncrementalFileSystem(ifsFolderPath, ifsFileName);
            fm.addNewFile(currentIFS.GetPath(), currentIFS.GetFullPath());
        }

        public void commit(long revision = -1)
        {
            incrFile = currentIFS.GetValues(revision);
            Dictionary<string, string> result = comFile.Keys.Intersect(incrFile.Keys).ToDictionary(t => t,t => comFile[t]);
            Dictionary<string, string> result1 = incrFile.Keys.Intersect(comFile.Keys).ToDictionary(t => t, t => incrFile[t]);
            foreach(KeyValuePair<string,string> pair in incrFile)
            {
                string value = comFile[pair.Key];
                if(comFile[pair.Key] != null)
                {

                }
            }
            //..
        }

        public void Branch(string branchName)
        {
            currentIFS = new IncrementalFileSystem(currentIFS.FolderPath, branchName, currentIFS.GetPath());
            fm.addNewFile(currentIFS.GetPath(), currentIFS.GetFullPath());
        }
    }
}
