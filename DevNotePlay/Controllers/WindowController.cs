using Common;
using Player.Extensions;
using Player.Views;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Windows;

namespace Player.Controllers
{
    [RoutePrefix("api/window")]
    public class WindowController : ApiController
    {
        [Route("create-event")]
        [HttpPost]
        public HttpResponseMessage OpenCreateEventWindow([FromBody]dynamic payload)
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

                Application.Current.Dispatcher.Invoke((Action)delegate {
                    WindowHelper.OpenWindow<AddEventWindow>();
                });

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exc.Message);
            }
        }
    }
}
