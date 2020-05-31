using Common;
using DevNote.Web.Recorder;
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

                string path = FileEndPointManager.Project2EndPointFolder; //config.GetValue("Project2Folder");
                //string playFile = Path.Combine(path, "play.txt");// config.GetValue("PlayFile"));
                string latestXML = Path.Combine(path, "latest.xml"); //config.GetValue("RecXMLFile"));


                if (File.Exists(latestXML))
                    File.Delete(latestXML);




                string content = xmlScript["content"];
                if (File.Exists(latestXML)) File.Delete(latestXML);
                //if (File.Exists(playFile)) File.Delete(playFile);

                File.WriteAllText(latestXML, content);
                Thread.Sleep(1000);
                //File.WriteAllText(playFile, string.Empty);

                RecFileWatcher.Play();

              

                return Request.CreateResponse(HttpStatusCode.OK, "Script sent to DevPlay. Please wait for playback.");
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exc.Message);
            }
        }
    }
}
