using LogApplication.Common.Config;
using System;
using Player.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace Player.Services
{
    public class EventTagService
    {
        private readonly string _url;

        public EventTagService()
        {
            ConfigManager config = new ConfigManager();
            _url = config.GetValue("DevNoteFrontUrl_dev") + "/api/events/";
        }

        public async Task<ObservableCollection<EventTag>> GetEvents()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ObservableCollection<EventTag> eventTags = new ObservableCollection<EventTag>();
                    var response = await client.GetAsync(_url);
                    if (response.IsSuccessStatusCode)
                    {
                        string eventTagsAsString = response.Content.ReadAsStringAsync().Result;
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        eventTags = javaScriptSerializer.Deserialize<ObservableCollection<EventTag>>(eventTagsAsString);
                    }
                    return eventTags;
                }
            }
            catch (Exception ex)
            {
                return new ObservableCollection<EventTag>();
            }
        }

        public async Task<string> CreateEvent(string eventFile)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        using (var file = File.OpenRead(eventFile))
                        {
                            using (var streamContent = new StreamContent(file))
                            {
                                using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                                {
                                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                                    content.Add(fileContent, "file", Path.GetFileName(eventFile));

                                    HttpResponseMessage response = await client.PostAsync(_url, content);

                                    string responseMessage = response.Content.ReadAsAsync<string>().Result;
                                    return responseMessage;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Upload Event error: " + ex.Message;
            }
        }

        public async Task<string> UpdateEventTag(EventTag eventTag)
        {
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                var payload = javaScriptSerializer.Serialize(eventTag);
                var buffer = System.Text.Encoding.UTF8.GetBytes(payload);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync(_url, byteContent);

                    string responseMessage = response.Content.ReadAsAsync<string>().Result;
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                return "Update Event error: " + ex.Message;
            }
        }

        public async Task<string> DeleteEventTag(EventTag eventTag)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string deleteUrl = _url + eventTag.Id.ToString();
                    HttpResponseMessage response = await client.DeleteAsync(deleteUrl);

                    string responseMessage = response.Content.ReadAsAsync<string>().Result;
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                return "Delete Event error: " + ex.Message;
            }
        }
    }
}
