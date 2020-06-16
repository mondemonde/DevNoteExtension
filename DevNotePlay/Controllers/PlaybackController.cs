using Common;
using DevNote.Web.Recorder;
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
        public HttpResponseMessage Upload([FromBody]dynamic payload)
        {
            try
            {
                string latestXML = FileEndPointManager.DefaultLatestXMLFile;
                string latestHtml = FileEndPointManager.DefaultLatestHtmlFile;
                string xmlContent = payload["xml"];
                string htmlContent = payload["html"];

                if (File.Exists(latestXML))
                    File.Delete(latestXML);
                if (File.Exists(latestHtml))
                    File.Delete(latestHtml);

                File.WriteAllText(latestXML, xmlContent);
                File.WriteAllText(latestHtml, htmlContent);
                Thread.Sleep(1000);

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
