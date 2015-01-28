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

        public void commit(Dictionary<string, string> repository)
        {
            Dictionary<string, string> result = comFile.Keys.Intersect(incrFile.Keys).ToDictionary(t => t, comFile.Keys[t]);
            //..
        }

        public void Branch(string branchName)
        {
            currentIFS = new IncrementalFileSystem(currentIFS.FolderPath, branchName, currentIFS.GetPath());
            fm.addNewFile(currentIFS.GetPath(), currentIFS.GetFullPath());
        }
    }
}
