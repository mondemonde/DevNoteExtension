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
        //private readonly HttpClient _client;

        public EventTagService()
        {
            ConfigManager config = new ConfigManager();
            //_client = new HttpClient();

            _url = config.GetValue("DevNoteFrontUrl") + "/api/events/";
        }

        public ObservableCollection<EventTag> GetEvents()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ObservableCollection<EventTag> eventTags = new ObservableCollection<EventTag>();
                    var response = client.GetAsync(_url).Result;
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

        public async Task<bool> CreateEvent(string eventFile)
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

                                return true;
                            }
                        }
                    }
                }
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

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PutAsync(_url, byteContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                using (HttpClient client = new HttpClient())
                {
                    string deleteUrl = _url + eventTag.Id.ToString();
                    HttpResponseMessage response = await client.DeleteAsync(deleteUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
