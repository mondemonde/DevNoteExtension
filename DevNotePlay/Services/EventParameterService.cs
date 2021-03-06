﻿using Common;
using LogApplication.Common.Config;
using Player.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
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

        }
        
        public async Task<(ObservableCollection<EventParameter>, ObservableCollection<EventScriptFile>)> GetEventParameters(int eventId)
        { // old return type: Task<ObservableCollection<EventParameter>>
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ObservableCollection<EventParameter> eventParameters = new ObservableCollection<EventParameter>();
                    ObservableCollection<EventScriptFile> eventScriptFiles = new ObservableCollection<EventScriptFile>();

                    var url = GetParameterUrl(eventId);

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        //string rawResponseData = await response.Content.ReadAsStringAsync();
                        var responseData = await response.Content.ReadAsAsync<dynamic>();
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

                        string eventParams = responseData["eventParameters"];
                        string scriptfiles = responseData["eventScripts"];

                        eventParameters = javaScriptSerializer.Deserialize<ObservableCollection<EventParameter>>(eventParams);
                        eventScriptFiles = javaScriptSerializer.Deserialize<ObservableCollection<EventScriptFile>>(scriptfiles);
                    }
                    return (eventParameters, eventScriptFiles);
                }
            }
            catch (Exception ex)
            {
                return (null, null);
            }
        }

        public async Task<string> CreateEventParameter(int eventId, EventParameter eventParameter)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = GetParameterUrl(eventId);

                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    var payload = javaScriptSerializer.Serialize(eventParameter);
                    var buffer = Encoding.UTF8.GetBytes(payload);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PostAsync(url, byteContent);

                    string responseMessage = await response.Content.ReadAsAsync<string>();
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                return "Create Parameter error: " + ex.Message;
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

                    string responseMessage = await response.Content.ReadAsAsync<string>();
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

                    string responseMessage = await response.Content.ReadAsAsync<string>();
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                return "Delete Parameter error: " + ex.Message;
            }
        }

        public async Task<string> DownloadScriptFromServer(int eventId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string url = GetParameterUrl(eventId, true) + scriptId;
                    string url = GetParameterUrl(eventId, true);
                    HttpResponseMessage response = await client.GetAsync(url);

                    string responseMessage = string.Empty;
                    if (!response.IsSuccessStatusCode)
                    {
                        responseMessage = await response.Content.ReadAsAsync<string>();
                    }
                    else
                    {
                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        string fileName = FileEndPointManager.Project2Folder + @"\" + response.Content.Headers.ContentDisposition.FileName;
                        using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            await stream.WriteAsync(bytes, 0, bytes.Length);
                        }
                        responseMessage = "";
                    }
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                return "Download script error: " + ex.Message;
            }
        }

        private string GetParameterUrl(int eventId, bool isScript = false)
        {
            //TODO: Remove _dev upon release
            ConfigManager config = new ConfigManager();
            string root = config.GetValue("DevNoteFrontUrl_dev");
            if (isScript) return root + "/api/event/" + eventId.ToString() + "/script/";
            return root + "/api/event/" + eventId.ToString() + "/parameters";
        }
    }
}
