using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikingFS
{
    class Program
    {
        public static void Tests()
        {
            var fs = new IncrementalFileSystem(@"C:\Users\Utilisateur\prog\test\truc.txt");
            fs.AddChange("IDENT.Ident4", "Banane");
            fs.AddChange("IDENT.Ident400", "135");
            fs.Commit();
            fs.AddChange("IDENT.Ident348", "Boom");
            fs.AddChange("IDENT.Ident349", "Chackalaka");
            fs.Commit();

            var dict = fs.GetValues();

            foreach(var banane in dict)
                Console.WriteLine(banane.Key + " " + banane.Value);

            Console.ReadKey();
        }

        static Dictionary<string, string> ModifiedList(TaxprepT2Com2014V2.Taxprep2014T2Return taxreturn)
        {
            var lst = new Dictionary<string,string>();

            foreach(var v in FieldList.fields)
            {
                var cell = taxreturn.GetCell(v);
                if(cell.HasInput)
                    lst[v] = cell.Value.ToString();
            }

            return lst;
        }

        static void Main(string[] args)
        {
            //Tests();
            TaxprepT2Com2014V2.Taxprep2014T2Return taxreturn = new TaxprepT2Com2014V2.Taxprep2014T2Return();
            
            if(!taxreturn.Open(@"C:\Users\Utilisateur\Desktop\bbb.214"))
                Console.Write("Yo bitch, something went wrong!");

            var dict = ModifiedList(taxreturn);
            foreach(var v in dict)
            {
                Console.WriteLine(v.Key + " " + v.Value);
            }
            Console.ReadKey();
            //taxreturn.Open()
            /*Console.Write("Enter the middleware's location : ");
            string middlewareFile = Console.ReadLine();
            Console.Write("Enter the data location : ");
            string dataFile = Console.ReadLine();
            var middleware = new fileMiddleware(middlewareFile);
            middleware.addNewData(middlewareFile);
            middleware.commit();*/
        }
    }
}
