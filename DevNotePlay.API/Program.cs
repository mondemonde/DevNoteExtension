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
                //Create HttpClient and make a request to api/values 
                //HttpClient client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "api/").Result;

                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("API app started.");
                Console.WriteLine(String.Format("Listening on: {0}", baseAddress));
                Console.ReadLine();
            }
        }
    }
}
