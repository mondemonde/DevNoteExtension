using MyCommonLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface.Models
{
    public class TableConfig : BaseModel
    {
        // public  DateTime BaiCalendarLastUpdate { get; set; }
        // public string BaiAccessToken { get; set; }

        public string ApplicationName { get; set; }

        public int BatchNo { get; set; }
        public int RecPerBatch { get; set; }


    }

}
