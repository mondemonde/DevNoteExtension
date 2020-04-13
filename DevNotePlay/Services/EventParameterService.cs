using LogApplication.Common.Config;
using Player.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Player.Services
{
    public class EventParameterService
    {
        public EventParameterService()
        {
            //ConfigManager config = new ConfigManager();
            //_url = config.GetValue("DevNoteFrontUrl_dev") + "/api/events/";
        }

        public ObservableCollection<EventParameter> GetEventParameters(int eventId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ObservableCollection<EventParameter> eventParameters = new ObservableCollection<EventParameter>();

                    var url = GetParameterUrl(eventId);

                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string eventParameterssAsString = response.Content.ReadAsStringAsync().Result;
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        eventParameters = javaScriptSerializer.Deserialize<ObservableCollection<EventParameter>>(eventParameterssAsString);
                    }
                    return eventParameters;
                }
            }
            catch (Exception ex)
            {
                return new ObservableCollection<EventParameter>();
            }
        }

        public async Task<string> UpdateEventParameter(int eventId, int parameterId, EventParameter eventParameter)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = GetParameterUrl(eventId) + "/" + parameterId.ToString();

                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    var payload = javaScriptSerializer.Serialize(eventParameter);
                    var buffer = Encoding.UTF8.GetBytes(payload);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PutAsync(url, byteContent);

                    string responseMessage = response.Content.ReadAsAsync<string>().Result;
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                return "Update Parameter error: " + ex.Message;
            }
        }

        public async Task<string> DeleteEventParameter(int eventId, int parameterId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = GetParameterUrl(eventId) + "/" + parameterId.ToString();
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    string responseMessage = response.Content.ReadAsAsync<string>().Result;
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                return "Delete Parameter error: " + ex.Message;
            }
        }

        private string GetParameterUrl(int eventId)
        {
            //TODO: Remove _dev upon release
            ConfigManager config = new ConfigManager();
            string root = config.GetValue("DevNoteFrontUrl_dev");
            return root + "/api/event/" + eventId.ToString() + "/parameters";
        }
    }
}
