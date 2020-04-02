using LogApplication.Common.Config;
using System;
using Player.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Net;

namespace Player.Services
{
    public class EventTagService
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public EventTagService()
        {
            ConfigManager config = new ConfigManager();
            _client = new HttpClient();

            _url = config.GetValue("DevNoteFrontUrl") + "/api/events/";
        }

        public ObservableCollection<EventTag> GetEvents()
        {
            try
            {
                ObservableCollection<EventTag> eventTags = new ObservableCollection<EventTag>();
                var response = _client.GetAsync(_url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string eventTagsAsString = response.Content.ReadAsStringAsync().Result;
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    eventTags = javaScriptSerializer.Deserialize<ObservableCollection<EventTag>>(eventTagsAsString);
                }
                return eventTags;
            }
            catch (Exception ex)
            {
                return new ObservableCollection<EventTag>();
            }
        }

        public async Task<bool> UpdateEventTag(EventTag eventTag)
        {
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                var payload = javaScriptSerializer.Serialize(eventTag);
                var buffer = System.Text.Encoding.UTF8.GetBytes(payload);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PutAsync(_url, byteContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //return ex.Message;
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEventTag(EventTag eventTag)
        {
            try
            {
                string deleteUrl = _url + eventTag.Id.ToString();
                HttpResponseMessage response = await _client.DeleteAsync(deleteUrl);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
