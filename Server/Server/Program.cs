using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Console.Title = "Server";

            Server.Init(50, 26950);
            Console.ReadKey();
        }
    }
}
