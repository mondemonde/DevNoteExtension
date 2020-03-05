using LogApplication.Common.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   public static class BotHttpClient
    {

        public static async Task<HttpResponseMessage> TaskHttpGetToChrome(string action, string controller = "chrome" , int? chromePort= 7200)
        {
            int targetPort = chromePort ?? DefaultApiPort.ChromePort;
            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            string targetAddress = string.Format("http://localhost:{0}/browser/{1}/{2}", targetPort, controller,action);

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
                return await client.GetAsync(targetAddress);;               
            


            }


        }


        //public static async Task<dynamic> Post(CmdParam cmdParam,int? basePort ,int? targetPort)
        //{
        //    basePort = basePort ?? DefaultApiPort.ChromePort;
        //    targetPort = targetPort ?? DefaultApiPort.ArmPort;

        //    string baseAddress = string.Format("http://localhost:{0}/", basePort);
        //    string targetAddress = string.Format("http://localhost:{0}/", targetPort);

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseAddress);

        //        //var data = JObject.FromObject(new
        //        //{
        //        //    name = "some value",
        //        //    version = new { type = "some value", ver = 2 }
        //        //});
        //        var data = JObject.FromObject(cmdParam);

        //        var response = client.PostAsync(targetAddress,
        //                                        new StringContent(JsonConvert.SerializeObject(data),
        //                                        Encoding.UTF8, "application/json")).Result;

        //        response.EnsureSuccessStatusCode();

        //        var responseContent = await response.Content.ReadAsStringAsync();

        //        dynamic json = JsonConvert.DeserializeObject(responseContent);
        //        dynamic r = json.result;

        //        // result = r.someproperty.ToString().Equals("resultdata");
        //        return r;

        //    }


        //}
        public static async Task<dynamic> ChromePostToRightArm(CmdParam cmdParam,string controller="right")
        {
            //int basePort = DefaultApiPort.ChromePort;
            int targetPort = DefaultApiPort.ArmPort;

            //string baseAddress = string.Format("http://localhost:{0}/", basePort);
            string targetAddress = string.Format("http://localhost:{0}/arm/{1}", targetPort,controller);

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



        public static void WriteChromeResponse(string message)
        {
            Console.WriteLine(string.Format("{0}:[{1}] {2}", "Chrome", DateTime.Now.ToShortTimeString(), message));
        }
        public static void WriteDevBotResponse(string message)
        {
            Console.WriteLine(string.Format("{0}:[{1}] {2}", "Bot", DateTime.Now.ToShortTimeString(), message));
        }
        public static void WriteRightArmResponse(string message)
        {
            Console.WriteLine(string.Format("{0}:[{1}] {2}", "RightArm", DateTime.Now.ToShortTimeString(), message));
        }
        public static void WriteLeftArmResponse(string message)
        {
            Console.WriteLine(string.Format("{0}:[{1}] {2}", "LeftArm", DateTime.Now.ToShortTimeString(), message));
        }

    }
}
