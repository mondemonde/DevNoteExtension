using Microsoft.Owin.Hosting;
using System;

namespace DevNotePlay.API
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("DevNotePlay API running.");
                Console.WriteLine(String.Format("Listening on: {0}", baseAddress));
                Console.ReadLine();
            }
        }
    }
}
