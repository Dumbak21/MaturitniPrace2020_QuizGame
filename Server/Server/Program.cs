using Server.ApiClient;
using Server.Network;
using Server.ServerLogic;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Server
{
    class Program
    {
        public static Processor apiProcessor;

        static void Main(string[] args)
        {
            apiProcessor = new Processor();

            Console.Title = "Server";
            NetworkDataHandler.InitPackets();
            ServerTCP.PORT = 5555;
            ServerTCP.Setup();


            Api.Init();
            bool state = false;
            while (true)
            {
                state = apiProcessor.LoginToAPI().Result;
                if (state)
                {
                    Console.WriteLine("Connected to API");
                    break;
                }
                Console.WriteLine("Trying to connect to API");
                Thread.Sleep(5000);
            }

            ServerManager.CreateNewRoom(1234);




            //var qa = apiProcessor.GetRandomQuestion().Result;
            ////Console.WriteLine(qa.ToString());
            //while (true)
            //{
            //    if (Console.ReadKey().Key == ConsoleKey.A)
            //    {
            //        _ = apiProcessor.GetRandomQuestion().Result;
            //    }
            //}
            
            Console.ReadKey();
        }
    }
}
