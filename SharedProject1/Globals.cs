using Common.COMMANDS;
using DevNote.Interface;
using LogApplication.Common.Commands;
using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    
    public static class GlobalDef
    {

        public static EnumDEBUG_MODE DEBUG_MODE { get; set; }

        #region MAIN window
        public static bool IsMainShuttingdown { get; set; }

        public static Process ProcessSender { get; set; }
        public static Process ProcessReceiver { get; set; }
        public static Process ProcessPlayer { get; set; }


        #endregion

        public static RunWFCmdParam CurrentCmd {
            get { return FileEndPointManager.ReadInputWFCmdJsonFile(); }
           }

        

        public static IBotHost CurrentBotHost { get; set; }

        public static IBot CurrentDesigner { get; set; }

        public static List<IProjIdentity> Projects { get; set; }

        public static IProjIdentity CurrentProject { get; set; }

        [Obsolete]
        public static IArmPlayer CurrentIPlayer { get; set; }

        [Obsolete]
        public static IArmPlayer CurrentPlayer
        {
            get
            {
                if (ListOfCurrentPlayers.Count > 0)
                    return ListOfCurrentPlayers[ListOfCurrentPlayers.Count - 1];
                else
                    return null;
            }
        }

        [Obsolete]
        public static List<IArmPlayer> ListOfCurrentPlayers { get; set; }


        [Obsolete]
        public static bool IsCodeCeptArmReady()
        {
            //TIP: Sync with asynch
            Task<bool> task = Task.Run<bool>(async () => await BotHttpClient.IsCodeCeptArmReady());
            return task.Result;

        }


        //[Obsolete]
        public static IDevNoteBrowser CurrentBrowser
        {
            get;set;
        }

        public static bool IsHeadless
        {
            get
            {
                ConfigManager config = new ConfigManager();
                var isHeadless = config.GetValue("IsHeadless");
                return Convert.ToBoolean(isHeadless);
            }
        }


        public static IFrontWF FrontWF
        {

            get
            {
                if (CurrentDesigner != null && CurrentDesigner.FrontWF != null)
                    return CurrentDesigner.FrontWF;
                else
                    return null;
            }


        }

      
  }

    public class GlobalEvents
    {

        public event EventHandler PostBackRecieved;

        protected virtual void OnPostBackRecieved(EventArgs e)
        {
            EventHandler handler = PostBackRecieved;
            handler?.Invoke(this, e);
        }


    }



}
