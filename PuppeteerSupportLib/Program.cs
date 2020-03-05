using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuppetSupportLib
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Press ENTER to continue...");
            // Console.ReadLine();
            KatalonInterpreter it = new KatalonInterpreter();
            it.ReadXmlFile();       
            Console.ReadLine();
        }
    }
}
