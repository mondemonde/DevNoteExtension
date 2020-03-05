using Common.COMMANDS;
using DevNote.Interface;
using IntegrationEvents.Events.DevNote;
using LogApplication.Common.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   public static class BotHttpClient
    {
        #region GENERAL USE

        [Obsolete]
        public static async Task<HttpResponseMessage> TaskHttpGetToChrome(string action, string controller = "chrome" , int? chromePort= 7200)
        {
            HttpResponseMessage respond = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            int targetPort = chromePort ?? DefaultApiPort.ChromePort;
            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            string targetAddress = string.Format("http://localhost:{0}/browser/{1}/{2}", targetPort, controller,action);

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(targetAddress);
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(cmdParam), Encoding.UTF8, "application/json");
                    //var response =   client.GetAsync(targetAddress).Result;
                    //response.EnsureSuccessStatusCode();
                    //var responseContent = await response.Content.ReadAsStringAsync();
                    //dynamic json = JsonConvert.DeserializeObject(responseContent);
                    //dynamic r = json.result;
                    // result = r.someproperty.ToString().Equals("resultdata");
                    respond = await client.GetAsync(targetAddress); ;



                }
            }
            catch (Exception err)
            {

                LogApplication.Agent.LogError(err);
            }
            
            

            return respond;


        }
        [Obsolete]
        public static async Task<dynamic> TaskPostToChrome(CmdParam cmdParam,string action, string controller = "chrome", int? chromePort = 7200)
        {
            //int basePort = DefaultApiPort.ChromePort;
            int targetPort = chromePort ?? DefaultApiPort.ChromePort;

            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            string targetAddress = string.Format("http://localhost:{0}/browser/{1}/{2}", targetPort, controller, action);

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(targetAddress);

                try
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(cmdParam), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("", stringContent);

                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();

                    dynamic json = JsonConvert.DeserializeObject(responseContent);
                    dynamic r = json.result;

                    // result = r.someproperty.ToString().Equals("resultdata");
                    return r;
                }
                catch (Exception err)
                {

                    LogApplication.Agent.LogError(err);
                }

                return null;


            }


        }

        public static async Task<HttpResponseMessage> TaskHttpGetToArmAPI(string action, string controller = "right", int? ArmPort = 7100)
        {
            HttpResponseMessage respond = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            int targetPort = ArmPort ?? DefaultApiPort.MainPort;
            //http://localhost:7100/arm/right/IsCodeCeptReady
            //http://localhost:7100/arm/right/IsCodeCeptReady
            string targetAddress = string.Format("http://localhost:{0}/arm/{1}/{2}", targetPort, controller, action);

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(targetAddress);
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(cmdParam), Encoding.UTF8, "application/json");
                    //var response =   client.GetAsync(targetAddress).Result;
                    //response.EnsureSuccessStatusCode();
                    //var responseContent = await response.Content.ReadAsStringAsync();
                    //dynamic json = JsonConvert.DeserializeObject(responseContent);
                    //dynamic r = json.result;
                    // result = r.someproperty.ToString().Equals("resultdata");
                    respond = await client.GetAsync(targetAddress); ;



                }
            }
            catch (Exception err)
            {

                LogApplication.Agent.LogError(err);
            }



            return respond;


        }




        //}
        /// <summary>
        /// Arm.API/RightController/post =>  MyDevBot.HandleCmd(value);
        /// </summary>
        /// <param name="cmdParam"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostToArmAPI(CmdParam cmdParam,string controller="right")
        {
            //int basePort = DefaultApiPort.ChromePort;
            int targetPort = DefaultApiPort.MainPort;

            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            string targetAddress = string.Format("http://localhost:{0}/arm/{1}", targetPort,controller);

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(targetAddress);

                try
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(cmdParam), Encoding.UTF8, "application/json");

                    //step# 13.2 
                    var response = await client.PostAsync("", stringContent);
                    response.EnsureSuccessStatusCode();

                    //var responseContent = await response.Content.ReadAsStringAsync();

                    //dynamic json = JsonConvert.DeserializeObject(responseContent);
                    //dynamic r = json.result;

                    // result = r.someproperty.ToString().Equals("resultdata");
                    //return r;
                    return response;
                }
                catch (Exception err)
                {

                    LogApplication.Agent.LogError(err);
                }

                return null;
             

            }


        }

        #region DEVNOTE

        public static async Task<HttpResponseMessage> TaskHttpGetToDesigner(string action, string controller = "devnote", int? devPort = 9000)
        {
            HttpResponseMessage respond = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            int targetPort = devPort ?? DefaultApiPort.DesignerPort;
            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            //http://localhost:9000/bot/devnote/hi
            string targetAddress = string.Format("http://localhost:{0}/bot/{1}/{2}", targetPort, controller, action);
            string baseAddress = string.Format("http://localhost:{0}/", targetPort);
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(baseAddress);
                    respond = await client.GetAsync(targetAddress); 
                }
            }
            catch (Exception err)
            {

                LogApplication.Agent.LogError(err);
            }



            return respond;


        }



        public static async Task<HttpResponseMessage> PostToDevNote(CmdParam cmdParam, string controller = "devnote")
        {
            //int basePort = DefaultApiPort.ChromePort;
            int targetPort = DefaultApiPort.DesignerPort;

            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            string targetAddress = string.Format("http://localhost:{0}/bot/{1}", targetPort, controller);

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(targetAddress);

                try
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(cmdParam), Encoding.UTF8, "application/json");

                    //step# 13.2 

                    var response = await client.PostAsync("", stringContent);
                    response.EnsureSuccessStatusCode();

                    //var responseContent = await response.Content.ReadAsStringAsync();

                    //dynamic json = JsonConvert.DeserializeObject(responseContent);
                    //dynamic r = json.result;

                    // result = r.someproperty.ToString().Equals("resultdata");
                    //return r;
                    return response;
                }
                catch (Exception err)
                {

                    LogApplication.Agent.LogError(err);
                }

                return null;


            }


        }

        
        public static async Task<HttpResponseMessage> PostToAzure(DevNoteIntegrationEvent devEvent, string controller = "Event")
        {
            //STEP_.EVENT PostToAzure
            //int basePort = DefaultApiPort.ChromePort;
            int targetPort = DefaultApiPort.AzureSenderPort;

            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            //    [Route("api/[controller]")]
            //http://localhost:5500/api/event
            string targetAddress = string.Format("http://localhost:{0}/api/{1}", targetPort, controller);

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(targetAddress);

                try
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(devEvent), Encoding.UTF8, "application/json");

                    //step# 13.2 

                    var response = await client.PostAsync("", stringContent);
                    response.EnsureSuccessStatusCode();

                    //var responseContent = await response.Content.ReadAsStringAsync();

                    //dynamic json = JsonConvert.DeserializeObject(responseContent);
                    //dynamic r = json.result;

                    // result = r.someproperty.ToString().Equals("resultdata");
                    //return r;
                    return response;
                }
                catch (Exception err)
                {

                    LogApplication.Agent.LogError(err);
                }

                return null;


            }


        }



        public static  IRestResponse DevNoteGetParameters(string eventTag)
        {
          
            int targetPort = DefaultApiPort.DesignerPort;
            string baseAddress = string.Format("http://localhost:{0}/", targetPort);
            string targetAddress = string.Format("http://localhost:{0}/bot/devnote/GetParameters?eventTag={1}", targetPort,eventTag );

            var client = new RestClient(baseAddress);//("http://localhost:9000/bot/devnote/GetParameters?eventTag=test");
                                                     //var request = new RestRequest(Method.GET);
                                                     //request.AddHeader("cache-control", "no-cache");
                                                     //request.AddHeader("Connection", "keep-alive");
                                                     //request.AddHeader("Accept-Encoding", "gzip, deflate");
                                                     //request.AddHeader("Host", "localhost:9000");
                                                     //request.AddHeader("Postman-Token", "581d5b0f-5a07-4bdd-95a2-068c7fedcfa0,edc7ba81-b888-470f-b01a-fe8ff3974a2a");
                                                     //request.AddHeader("Cache-Control", "no-cache");
                                                     //request.AddHeader("Accept", "*/*");
                                                     //request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");

            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            //IRestResponse response = client.Execute(request);

            //var client = new RestClient("https://api.twitter.com/1.1");
            // client.Authenticator = new HttpBasicAuthenticator("username", "password");

            var request = new RestRequest(targetAddress, DataFormat.Json);
            return client.Get(request);



        }


        #endregion

        public static async Task<bool> Log(string msg, bool isErrorMessage = false)
        {
            //int basePort = DefaultApiPort.ChromePort;
            int targetPort = DefaultApiPort.MainPort;

            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            string targetAddress = string.Format("http://localhost:{0}/arm/{1}", targetPort, "armvalues");

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(targetAddress);

                try
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
                    _ = await client.PostAsync("", stringContent);

                    if (isErrorMessage == false)
                        LogApplication.Agent.LogWarn(msg);
                    else
                        LogApplication.Agent.LogError(msg);

                    return true;
                    //Task.Run(async () =>
                    //{
                    //    var response = await client.PostAsync("", stringContent);
                    //}).GetAwaiter().GetResult();

                }
                catch (Exception err)
                {

                    LogApplication.Agent.LogError(err);
                    return false;
                }



            }


        }





        public static async  void UpdateMainUI(string msg,StateAlias state= StateAlias.NormalState, int fontSize =10, bool isCentered=false,int duration = 4 )
        {
            var view1 = new UpdateMainViewCmdParam();
            view1.FontSize = fontSize;
            view1.Duration = duration;
            view1.IsCentered = isCentered;
            view1.Message = msg;
            view1.StateAKA = state.ToString();

            //bool cntDown = false;
            await GlobalMain.UpdateView(view1);

        }



        public static void WriteChromeResponse(string message)
        {
            Console.WriteLine(string.Format("{0}:[{1}] {2}", "Chrome", DateTime.Now.ToShortTimeString(), message));
        }
        public static void WriteDevBotResponse(string message)
        {
            Log(string.Format("{0}:[{1}] {2}", "Designer", DateTime.Now.ToShortTimeString(), message));
        }
        public static void WriteRightArmResponse(string message)
        {
            Log(string.Format("{0}:[{1}] {2}", "RightArm", DateTime.Now.ToShortTimeString(), message));
        }
        public static void WriteLeftArmResponse(string message)
        {
            Log(string.Format("{0}:[{1}] {2}", "LeftArm", DateTime.Now.ToShortTimeString(), message));
        }


        #endregion-----------------------------------------------------


        #region-------------------------------- CODECEPT AVATAR------------------------------------------
        [Obsolete]
        public static async Task<bool> IsCodeCeptArmReady()
        {
            bool result = false;

            var response = await BotHttpClient.TaskHttpGetToArmAPI("IsCodeCeptReady");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                
                var hello = response.Content.ReadAsStringAsync().Result;               
                BotHttpClient.WriteDevBotResponse("IsCodeCeptArmReady: \n" +  hello);
                result = Convert.ToBoolean(hello);
            }
            else
                result = false;

            return result;
        }

        #endregion

    }

   
}
