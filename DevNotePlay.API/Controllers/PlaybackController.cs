using LogApplication.Common.Config;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace DevNotePlay.API.Controllers
{
    [RoutePrefix("api/playback")]
    public class PlaybackController : ApiController
    {
        [Route("upload")]
        [HttpPost]
        public HttpResponseMessage Upload([FromBody]dynamic xmlScript)
        {
            try
            {
                ConfigManager config = new ConfigManager();

                string path = config.GetValue("Project2Folder");
                string playFile = Path.Combine(path, config.GetValue("PlayFile"));
                string recFile = Path.Combine(path, config.GetValue("RecXMLFile"));
                string content = xmlScript["content"];

                if (File.Exists(recFile)) File.Delete(recFile);
                if (File.Exists(playFile)) File.Delete(playFile);

                File.WriteAllText(recFile, content);
                Thread.Sleep(1000);
                File.WriteAllText(playFile, string.Empty);

                return Request.CreateResponse(HttpStatusCode.OK, "Script sent to DevPlay. Please wait for playback.");
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exc.Message);
            }
        }
    }
}
