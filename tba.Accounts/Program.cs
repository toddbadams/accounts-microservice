using System;
using Microsoft.Owin.Hosting;

namespace tba.Accounts
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:8123/";
            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(baseAddress);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseAddress);
            Console.ReadLine();
        }
    }
}
