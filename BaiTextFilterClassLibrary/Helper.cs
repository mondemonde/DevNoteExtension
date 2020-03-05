using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTextFilterClassLibrary
{
   public static class Helper
    {
        public static string GetNumbersOnly(string input)
        {
            string output = string.Empty;
            foreach(char c in input)
            {
                if(char.IsNumber(c) || c=='.' )
                     output += c;
            }
            return output;
        }

        public static string GetValidFileName(string illegal)
        {
            //string illegal = "\"M\"\\a/ry/ h**ad:>> a\\/:*?\"| li*tt|le|| la\"mb.?";
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            invalid += "=";
            foreach (char c in invalid)
            {
                illegal = illegal.Replace(c.ToString(), "");
            }

            return illegal;
        }

        public static string ToTitleCase(this string s) =>
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());

        
        public static String XpathHandleQuotes(String input)
        {
            if (input.Contains("'"))
            {
                string prefix = "";
                var elements = input.Split('\'');

                //string output = "concat(";
                string output = "";

                foreach (var s in elements)
                {
                    output += $"{prefix}'{s}'";
                    //prefix = ",\"'\",";
                    prefix = "+\"'\"+";

                }

                //if (output.EndsWith(","))
                if (output.EndsWith("+"))

                {
                    output = output.Substring(0, output.Length - 2);
                }

               // output += ")";

                return output;
            }
            else
            {
               return $"'{input}'";
               
            }
        }

    }
}
