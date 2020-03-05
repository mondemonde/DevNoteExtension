using MyCommonLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface.Models
{
  public  class WFProfile:BaseModel
    {
        public string Domain { get; set; }
        public string Department { get; set; }

        //public string Alias { get; set; }

        public string Tag { get; set; }

        public string Descriptions{ get; set; }
        

        public string SourcePath { get; set; }

        public bool InActive { get; set; }

        public int VersionNo { get; set; }


        public string Name
        {
            get
            {
                return string.Format("{0}__{1}__{2}__{3}", Domain, Department,FileName,VersionNo.ToString());
            }

        }

        public string FileName
        {
            get
            {
                if (!string.IsNullOrEmpty(SourcePath))

                    return Path.GetFileNameWithoutExtension(SourcePath);
                else
                    return "";
            }

        }



    }
}
