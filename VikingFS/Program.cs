﻿using System;
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
           /* var fs = new IncrementalFileSystem(@"C:\Users\Utilisateur\prog\test\", "banane");
            fs.Commit("IDENT.Ident4", "Banane");
            fs.Commit("IDENT.Ident400", "135");
            fs.Push();
            fs.Commit("IDENT.Ident348", "Boom");
            fs.Commit("IDENT.Ident349", "Chackalaka");
            fs.Push();

            fs = fs.Branch("erpderp");
            fs.Commit("IDENT.Ident348", "Bam");
            fs.Commit("Woot", "Woot!");
            fs.Push();

            var dict = fs.GetValues();

            foreach(var banane in dict)
                Console.WriteLine(banane.Key + " " + banane.Value);

            Console.ReadKey();*/
        }

        

        static void TestFileMiddleware()
        {
            var fm = new fileMiddleware(@"C:\Users\Utilisateur\Desktop\middleware.txt");
            fm.addNewFile("TechnoViking.dat", @"C:\Users\Utilisateur\Desktop\Technoviking.dat");
            fm.addNewFile("Ropt.banane", @"C:\Users\Utilisateur\Desktop\Technoviking.dat");
            Console.WriteLine(fm.getPath("TechnoViking.dat"));
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            //TestFileMiddleware();
           // Tests();
            //string middlewareFile = @"C:\middleware.txt";
            //fileMiddleware middleware = new fileMiddleware(middlewareFile);
            string dataFile = @"C:\Users\Utilisateur\Desktop\bbb.214";

            /*if (!taxreturn.Open(dataFile))
                Console.Write("Yo bitch, something went wrong!");

            var dict = ModifiedList(taxreturn);
            foreach(var v in dict)
            {
                Console.WriteLine(v.Key + " " + v.Value);
            }
            Console.ReadKey();*/
            
            
            //taxreturn.Open()
            /*      
            string middlewareFile = @"C:\middleware.txt";
            Console.Write("Enter the data location : ");
            string dataFile = Console.ReadLine();
            fileMiddleware middleware = new fileMiddleware(middlewareFile);
            middleware.addNewData(middlewareFile);
            middleware.push();
             */
        }
    }
}
