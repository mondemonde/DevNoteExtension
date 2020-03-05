using Common;
using LogApplication.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskWaiter
{
    public enum EnumTaskStatus
    {
        Started,
        DoneCodeCept, 
        DoneBGWF,
        Finished
    }
    public class Conditions
    {

       public long Id { get; set; }

       public int ArmPort { get; set; }
       public int ChromePort { get; set; }

        public string Name { get; set; }

        public  Conditions()
        {
            ArmPort = Common.DefaultApiPort.MainPort;
            ChromePort = Common.DefaultApiPort.ChromePort;
            Name = "TaskWaiter";
        }

        public Conditions(string name)
        {
            Name = name;
            ArmPort = Common.DefaultApiPort.MainPort;
            ChromePort = Common.DefaultApiPort.ChromePort;
        }

        public Conditions(int aPort, int cPort)
        {
            ArmPort = aPort;
            ChromePort = cPort;
        }

        //todo use this queue
        public  Queue<int> Todos { get; set; }

        public bool isChromeTaskDone { get; set; }      

        public  bool IsDevBotRunning { get; set; }
        public  bool IsCodeCeptRunning { get; set; }
        public  bool IsFrontWfRunning { get; set; }



        public async Task<string> WaitUntilChromeIsRunnung(int frequency = 100, int timeout = 45000)
        {
            string result = string.Empty;
            isChromeTaskDone = false;

            var waitTask = Task.Run(async () =>
            {
                while (!isChromeTaskDone)
                {
                    var response =  await BotHttpClient.TaskHttpGetToChrome("hi");
                   
                    if (response.StatusCode== System.Net.HttpStatusCode.OK)
                    {
                        isChromeTaskDone = true;
                        var hello = response.Content.ReadAsStringAsync().Result;
                        //Console.WriteLine(hello);
                        BotHttpClient.WriteChromeResponse(hello);
                        result = hello ;
                    }                  

                    if(GlobalDef.CurrentDesigner !=null && GlobalDef.CurrentDesigner.IsRunningWF==false)
                    {
                       var respond = "Cancelled Run WF.";
                        BotHttpClient.WriteChromeResponse(respond);
                        result = respond;
                        isChromeTaskDone = true;
                    }

                    await Task.Delay(frequency);
                }
            });

            if (waitTask != await Task.WhenAny(waitTask,
                    Task.Delay(timeout)))
            {
                // throw new TimeoutException();
                result = "WaitUntilChromeIsRunnung TimeoutException";
                LogApplication.Agent.LogError(result);
            }

            return result;
        }

        public async Task<string> WaitUntilCodeceptIsRunning(int frequency = 100, int timeout = 45000)
        {
            string result = string.Empty;
            isChromeTaskDone = false;

            var waitTask = Task.Run(async () =>
            {
                while (!isChromeTaskDone)
                {
                    var response = await BotHttpClient.TaskHttpGetToArmAPI("hi");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        isChromeTaskDone = true;
                        var hello = response.Content.ReadAsStringAsync().Result;
                        //Console.WriteLine(hello);
                        BotHttpClient.WriteChromeResponse(hello);
                        result = hello;
                    }

                    await Task.Delay(frequency);
                }
            });

            if (waitTask != await Task.WhenAny(waitTask,
                    Task.Delay(timeout)))
            {
                // throw new TimeoutException();
                result = "WaitUntilChromeIsRunnung TimeoutException";
                LogApplication.Agent.LogError(result);
            }

            return result;
        }


       
        public async Task<string> WaitUntilDesignerIsRunning(int frequency = 1000, int timeout = 60000)
        {
            string result = string.Empty;
           bool isRunning = false;

            var waitTask = Task.Run(async () =>
            {
                while (!isRunning)
                {
                    var response = await BotHttpClient.TaskHttpGetToDesigner("hi");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        isRunning = true;
                        var hello = response.Content.ReadAsStringAsync().Result;
                        //Console.WriteLine(hello);
                        BotHttpClient.WriteDevBotResponse(hello);
                        result = hello;
                    }

                    await Task.Delay(frequency);
                }
            });

            if (waitTask != await Task.WhenAny(waitTask,
                    Task.Delay(timeout)))
            {
                // throw new TimeoutException();
                result = "WaitUntilDesignerIsRunnung TimeoutException";
                LogApplication.Agent.LogError(result);
            }

            return result;
        }


        string CheckChromeStarted()
        {
            string result = string.Empty;
            var response =  BotHttpClient.TaskHttpGetToChrome("hi").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                isChromeTaskDone = true;
                var hello = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine(hello);
                BotHttpClient.WriteChromeResponse(hello);
                result = hello;
            }
            return result;

        }
        //public async Task WaitUntilChromeStarted(int id)
        //{
        //    // example usage (inline lambda)
        //    await WaitUntil(() =>
        //     Todos.Dequeue() == id
        //    );
        //    Console.WriteLine($"Dequeue': {id}");
        //}




     

        /// <summary>
        /// Blocks while condition is true or timeout occurs.
        /// </summary>
        /// <param name="condition">The condition that will perpetuate the block.</param>
        /// <param name="frequency">The frequency at which the condition will be check, in milliseconds.</param>
        /// <param name="timeout">Timeout in milliseconds.</param>
        /// <exception cref="TimeoutException"></exception>
        /// <returns></returns>
        public  async Task WaitWhile(Func<bool> condition, int frequency = 100
            , int timeout = -1)
        {
            var waitTask = Task.Run(async () =>
            {
                while (condition()) await Task.Delay(frequency);
            });

            if (waitTask != await Task.WhenAny(waitTask, Task.Delay(timeout)))
                throw new TimeoutException();
        }

        /// <summary>
        /// Blocks until condition is true or timeout occurs.
        /// </summary>
        /// <param name="condition">The break condition.</param>
        /// <param name="frequency">The frequency at which the condition will be checked.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public  async Task WaitUntil(Func<bool> condition, int frequency = 100, int timeout = -1)
        {
            var waitTask = Task.Run(async () =>
            {
                bool isconsole = true;
                DateTime dateStatus = DateTime.Now.AddSeconds(5);
                while (!condition())
                {
                    
                    await Task.Delay(frequency);
                 
                    if(string.IsNullOrEmpty(this.Name))
                    {
                        this.Name = "WaitUntil";
                    }
                    if (dateStatus < DateTime.Now)
                    {
                        dateStatus = DateTime.Now.AddSeconds(10);
                        Console.WriteLine("Busy... " + Name);
                    }
                    try
                    {
                        if(isconsole)
                            ConsoleSpinner.Instance.Update();

                    }
                    catch (Exception)
                    {
                        //throw;
                        isconsole = false;
                    }
                }
            });

            if (waitTask != await Task.WhenAny(waitTask,
                    Task.Delay(timeout)))
                throw new TimeoutException();
        }
    }
}
