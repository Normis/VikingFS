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
        string comFilePath, incrFilePath,
               comFileName, incrFileName;
        public FieldComparer(string comFilePath, string comFileName, string incrFilePath, string incrFileName)
        {
            this.comFilePath = comFilePath;
            this.comFileName = comFileName;
            this.incrFilePath = incrFilePath;
            this.incrFileName = incrFileName;

        }

        public void commit(long revision = -1)
        {
            IncrementalFileSystem ifs = new IncrementalFileSystem(incrFilePath,incrFileName);
            incrFile = ifs.GetValues(revision);
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

    }
}
