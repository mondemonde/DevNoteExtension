using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using Player.Models;

namespace Player.Services
{
    public class EventTagService
    {
        private readonly string _url;

        public EventTagService()
        {
            ConfigManager config = new ConfigManager();

            _url = config.GetValue("DevNoteFrontUrl_dev") + "/api/events";
        }

        public List<EventTag> GetEventTagLibraryFromServer()
        {
            try
            {
                var result = _url.GetJsonFromUrl().FromJson<List<EventTag>>();
                return result;
            }
            catch (Exception ex)
            {
                return new List<EventTag>();
            }
        }
    }
}
