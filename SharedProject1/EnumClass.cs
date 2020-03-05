using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{
    public enum EnumCmd
    {
        //actions
        GoTo,
        TypeKey,
        Click,
        Scroll,
        Submit,
        SaveToFile,
        Email,
        Puppet,
        Debug,
        Find,
        ConsoleTest,
        Codecept,
        Initialize,
        Screenshot,

        //Main UI
        UpdateMainView,

        //status
       ChromeStarted,
       ChromeBusy,
       ChromeClosing ,
       
       //project
       StartBotProject,
       BotProjectCompleted,
       BotProjectRunning,
       BotStep,

       //WF
       RunWF,
       EndWFResult,

      //Database
      DBGetEvents,
      DBGetParameters


    }

    public enum EnumMessageTo
    {
        ArmApi,
        CodeCeptArm,
        Chrome,
        Designer

    }
    public enum EnumMessageFrom
    {
        ArmApi,
        CodeCeptArm,
        Chrome,
        Designer
        

    }

    public enum EnumScroll
    {
        Up,
        Down,
        Left,
        Rigth
    }

    public enum EnumRobotParts
    {
        Head,   //DevBot
        Project,   //Body
        ChromeArm,
        CodeCeptIArm,
        CodeCeptStepArm,
        RightArm,
        Main,
        Player,
        Designer  //base or feet

    }

    public static class EnumFiles
    {
        public static string Receiver = "RunAzureReceiver.bat";
        public static string Sender = "RunAzureSender.bat";
        public static string Designer = "RunDesigner.bat";
        public static string Player = "RunPlayer.bat";
        public static string RunDevNoteMain = "RunDevNoteMain.txt";

        public static string WFOutput = "WFOutput.json";

        public static string WFInput = "WFInput.json";

        public static string EventResult = "EventResult.json";


        public static string MyGrabValue = "MyGrabValue.txt";
        public static string MyResult = "result.txt";

        public static string MinimizeAll = "MinimizeAll.bat";

        public static string UI_Prefix = "UI_";


    }

    public enum EnumProcess
    {
        NonGlobal,
        Designer,
        Player,
        Receiver,
        Sender
    }

    public enum TODO
    {
        Main=900,
        Designer=700,
        Player = 800,        
        Receiver=500,
        Sender=600
    }

    public enum STEP_
    {
        INIT = 0,
        MAIN = 900,
        Designer = 700,
        PLAYER = 800,
        RECIEVER = 500,
        SENDER = 600,
        CodeCept = 80,
        EVENT=300,
        RESULT

    }

    public enum StateAlias
    {
        InitializingState = 0,
        InitializingAPI,

        Starting_Sender,
        Starting_Receiver,
        Starting_Designer,

        ReadyForAuto,

        Running_Auto,

        TalkNotConnectedState,
        QueryReminderState,
        QuerySettingsState,
        QuerySkypeState,
        LogInToSettingsState,
        ChangeLogInState,
        JustLogInState,
        FailedLoginState,
        NormalState
    }

    public enum MyConfig
    {
        MyMainFolder,
        AzureServiceBusReceiver,
        AzureServiceBusSender,
        DevNoteDesingnerExe,
        DevNotePlayerExe,
        ChromeExe,
        ScreenshotOnFail,
        Project2EndPointFolder


    }

    #region ERROR CODES
    public enum ErrorCodes
    {
        TimedOut=600,
        MaxTryLimit ,
        Unhandled=1000
    }

    #endregion

    public enum EnumDEBUG_MODE
    {
        DEBUG,
        OFFLINE,        
        OVERRIDE,
        TEST
          
    }

}
