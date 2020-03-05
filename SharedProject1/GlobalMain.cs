using Common.COMMANDS;
using DevNote.Interface;
using LogApplication.Common.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   public class GlobalMain
    {

        public static string Message { get; set; }

        public static StateAlias StateAKA { get; set; }

        public async static Task<bool> UpdateView(UpdateMainViewCmdParam cmd)
        {
            var container = new UpdateMainViewCmdParam();
            container.Payload = cmd;

           await BotHttpClient.PostToArmAPI(container);

            return true;

        }

        public async static Task<bool> UpdateMe()
        {

            var response = await BotHttpClient.TaskHttpGetToArmAPI("UpdateMe");

            var responseContent = await response.Content.ReadAsStringAsync();
            var main = JsonConvert.DeserializeObject<GlobalMain>(responseContent);


            GlobalMain.StateAKA = main.StataAKA_DTO;
            GlobalMain.Message = main.Message_DTO;


            return true;

        }

        public async static Task<StateAlias> GetStateAKA()
        {

            await UpdateMe();            

            return GlobalMain.StateAKA;

        }



        public StateAlias StataAKA_DTO { get; set; }
        public  string Message_DTO { get; set; }


        //public async static Task<bool>DisplayCustom(string message, string status, int fontsize = 25, int duration = 5, bool isCentered = false)

        //{
        //    var container = new UpdateMainViewCmdParam();
        //    container.Payload = cmd;

        //    await BotHttpClient.PostToArmAPI(container);

        //    return true;

        //}




    }


}
