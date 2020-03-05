using Newtonsoft.Json;
using SeleniumSupportLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumSupportLib
{
   public class Interpreter
    {


        public string DefaultSourceFolder
        {
            get
            {
                var baseDir = LogApplication.Agent.GetCurrentDir() + "\\Selenium\\";//AppDomain.CurrentDomain.BaseDirectory;
                baseDir = baseDir.Replace("file:\\", "");
                return baseDir;
            }
        }


        public StringBuilder Convert(string fullFileName="")
        {


            //_HACK safe to delete 
            #region---DEFAULT??           
            //todo: test.side
            if (string.IsNullOrEmpty(fullFileName))
            {
                fullFileName = DefaultSourceFolder + "test.side";
            }

            #endregion //////////////END TEST

            //string FullFileName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\JS\GoTo.js";
            StringBuilder builder = new StringBuilder(File.ReadAllText(fullFileName));
            //JsonSerializer serializer = new JsonSerializer();
            //RootObject obj = serializer.Deserialize<RootObject>((File.ReadAllText(fullFileName));

            RootObject selenium;

            string json = builder.ToString();
            selenium = JsonConvert.DeserializeObject<RootObject>(json);

            //get first
            var sel = selenium;//.FirstOrDefault();
            foreach(var t in sel.tests)
            {
                foreach(var c in t.commands)
                {
                   Console.WriteLine(c.command);

                }

            }




                StringBuilder result = new StringBuilder();



            return result;
        }
    }
}
