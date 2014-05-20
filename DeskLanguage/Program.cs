using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace DeskLanguage
{
    public class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = string.Format("http://localhost:{0}/", args.Length > 0 ? args[0] : "9000");

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Now listening at " + baseAddress + " Press any key to exit");
                Console.ReadLine();
            }
        }
    }
}