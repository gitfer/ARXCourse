using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace MyKatanaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Microsoft.Owin.Hosting
            // Includere anche 
            // per evitare:
            // The server factory could not be located for the given input: Microsoft.Owin.Host.HttpListener
            const string baseUrl = "http://localhost:5000/";

            StartOptions options = new StartOptions();
            options.Urls.Add(baseUrl);

            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("...Microsoft.Owin.Hosting...");
                Console.WriteLine("...Microsoft.Owin.Host.HttpListener...");
                Console.WriteLine("Press Enter to quit.");
                Console.ReadKey();
            }
        }
    }
}
