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

        static void Main(string[] args)
        {
            Tests();
            TaxprepT2Com2014V2.Taxprep2014T2Return taxreturn = new TaxprepT2Com2014V2.Taxprep2014T2Return();
            //taxreturn.Open()
        }
    }
}
