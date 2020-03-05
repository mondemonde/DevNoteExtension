
using Common.COMMANDS;
using DevNote.Interface;
using EFCoreTransactionsReceiver.IntegrationEvents;
using IntegrationEvents.Events.DevNote;
using Newtonsoft.Json;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.API.EventHandler
{
    public class DevNoteIntegrationEventHandler : IIntegrationEventHandler<DevNoteIntegrationEvent>
    {

       // public  IBotHost azuBOT { get; set; }

        //public DevNoteIntegrationEventHandler(IBotHost bot)
        //{
        //    azuBOT = bot;
        //}

        public DevNoteIntegrationEventHandler()
        {
          
        }


        
        //STEP.Azure #41 Handle(DevNoteIntegrationEvent @event)
        public async Task Handle(DevNoteIntegrationEvent @event)
        {

            //convert to command param
            RunWFCmdParam cmd = new RunWFCmdParam
            {
                // XamlFullPath="QRLogin",
                //username = @event.//"demouser@blastasia.com", //default
                //password =// "Pass@word1",
                EventName = @event.EventName,
                EventParameters = @event.EventParameters,
                GuidId = @event.GuidId
                

            };       


            //_HACK safe to delete 
            #region---TEST ONLY: Compiler will  automatically erase this in RELEASE mode and it will not run if Global.GlobalTestMode is not set to TestMode.Simulation
#if DEBUG 
            System.Diagnostics.Debug.WriteLine("HACK-TEST -DevNoteIntegrationEvent");
            if(GlobalDef.DEBUG_MODE == EnumDEBUG_MODE.TEST)
            {
                var stringContent = JsonConvert.SerializeObject(@event);
                Console.WriteLine("Azure recievedt: " + stringContent);

                return;
            }

            
#endif
            #endregion //////////////END TEST





            if (!string.IsNullOrEmpty(@event.OuputResponse))
            {
                //do not trigger wf
               await  BotHttpClient.Log("Confirmed to AZURE: " + @event.OuputResponse);
            }
            else
            {
                //STEP_.EVENT CreateEventInput FileEnpoint here           
               await FileEndPointManager.CreateEventInput(cmd);

            }

            return;

            //xTODO call this in UIMain by filewatcher   
            #region
            //package to cmd
            RunWFCmdParam cmdCarrier = new RunWFCmdParam();

            if (cmd.EventParameters == null)
                cmd.EventParameters = new Dictionary<string, string>();

            //TIP:  _ = await BotHttpClient.PostToDevNote(cmdCarrier);
            cmdCarrier.Payload = cmd;
            _ = await BotHttpClient.PostToDevNote(cmdCarrier);
            #endregion



        }

    }
}
