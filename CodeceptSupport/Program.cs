using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport
{
    class Program
    {
        static void Main(string[] args)
        {

            // Console.WriteLine("Press ENTER to continue...");
            // Console.ReadLine();
            Interpreter it = new Interpreter();
            it.ReadXmlFile();

            Console.ReadLine();
        }
    }
}
