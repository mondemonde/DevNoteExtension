using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
   public static class DefaultApiPort
    {
        //main
        public static int MainPort = 7100;

        public static  int ChromePort = 7200;
        public static int ChromeDebuggerPort = 8088;

        //designer
        public static int DesignerPort = 9000;

        public static int AzureSenderPort = 5500;
        public static int AzureReceiverPort = 5000;

        public static string GetLocalBaseAddress(int port)
        {
            string baseAddress = string.Format("http://localhost:{0}/", port);
            return baseAddress;

        }
    }


}
