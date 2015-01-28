using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikingFS
{
    class FieldComparer
    {
        fileMiddleware fm;
        IncrementalFileSystem currentIFS;
        TaxprepT2Com2014V2.Taxprep2014T2Return taxreturn;

        public FieldComparer(string comFilePath, string middlewareFilePath, string ifsFolderPath, string ifsFileName)
        {
            fm = new fileMiddleware(middlewareFilePath);
            currentIFS = new IncrementalFileSystem(ifsFolderPath, ifsFileName);
            fm.addNewFile(currentIFS.GetPath(), currentIFS.GetFullPath());
            taxreturn = new TaxprepT2Com2014V2.Taxprep2014T2Return();
            taxreturn.Open(comFilePath);
        }

        public void ClearEverything(string file)
        {
            taxreturn.CloseFileHandle();
            System.IO.File.Delete(file);
            taxreturn = new TaxprepT2Com2014V2.Taxprep2014T2Return();
            taxreturn.SaveAs(file);
        }

        public void commit()
        {
            Dictionary<string, string> fromCOM = ModifiedList();
            Dictionary<string, string> fromText = currentIFS.GetValues();

            var intersect = fromCOM.Keys.Intersect(fromText.Keys);// override values if change

            foreach (var key in fromCOM.Keys.Union(fromText.Keys).Except(intersect))
                this.currentIFS.Commit(key, fromCOM[key]);
            
            foreach (var key in intersect)
                if(fromCOM[key] != fromText[key])
                    this.currentIFS.Commit(key, fromCOM[key]);
        }

        public void Branch(string branchName)
        {
            currentIFS = new IncrementalFileSystem(currentIFS.FolderPath, branchName, currentIFS.GetPath());
            fm.addNewFile(currentIFS.GetPath(), currentIFS.GetFullPath());
        }

        public Dictionary<string, string> ModifiedList()
        {
            var lst = new Dictionary<string, string>();

            foreach (var v in FieldList.fields)
            {
                var cell = taxreturn.GetCell(v);
                if (cell.HasInput)
                {
                    lst[v] = cell.Value.ToString();
                }
            }
            return lst;
        }
        
        public void Checkout(string branchName, long revision = -1)
        {
            ClearEverything(currentIFS.GetFullPath());

            foreach (var v in new IncrementalFileSystem(currentIFS.FolderPath, branchName).GetValues(revision))
                taxreturn.SetCellValue(v.Key, v.Value);  //erh what if not string?
        }
        
        public void Update(long revision = -1)
        {
            ClearEverything(currentIFS.GetFullPath());
            Checkout(currentIFS.FileName, revision);
        }

        public void Push()
        {
            this.currentIFS.Push();
        }
    }
}
