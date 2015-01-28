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
        string comFilePath, incrFilePath;
        public FieldComparer(string comFilePath, string incrFilePath)
        {
            this.comFilePath = comFilePath;
            this.incrFilePath = incrFilePath;

        }

        public void commit(Dictionary<string, string> repository)
        {
            fileMiddleware fm = new fileMiddleware(incrFilePath);
            Dictionary<string, string> result = comFile.Keys.Intersect(incrFile.Keys).ToDictionary(t => t, comFile.Keys[t]);
            //..
        }

    }
}
