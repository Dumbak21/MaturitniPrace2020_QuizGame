using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";

            NetworkDataHandler.InitPackets();

            ServerTCP.PORT = 5555;

            ServerTCP.Setup();

            Console.ReadKey();
        }
    }
}
