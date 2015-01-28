using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikingFS
{
    class fileMiddleware
    {
        private string filePath;
        private StringBuilder newFile;
        public fileMiddleware(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                System.IO.File.WriteAllText(filePath, "");
            this.filePath = filePath;
            newFile = new StringBuilder();
        }

        public Boolean dataExists(string data)
        {
            /*Lookup if the data already exists*/
            Boolean dataExists = false;
            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                if (line.Contains(data))
                {
                    dataExists = true;
                    break;
                }
            }
            return dataExists;
        }

        public void addNewData(string data) 
        {
            if(!dataExists(data))this.newFile.Append(data+"\n");
        }

        public void push()
        {
            using (System.IO.StreamWriter w = System.IO.File.AppendText(filePath))
            {
                w.Write(newFile);
            }
        }
    }
}
