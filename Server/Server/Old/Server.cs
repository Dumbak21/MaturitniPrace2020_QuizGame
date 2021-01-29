using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;

//namespace Server
//{
//    class Server
//    {
//        private static TcpListener tcpListener;

//        private static int MAX_PLAYERS { get; set; }
//        private static int PORT { get; set; }


//        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

//        public static void Init(int maxPlayers, int port)
//        {
//            Console.WriteLine("Server initializing...");
//            MAX_PLAYERS = maxPlayers;
//            PORT = port;

//            ServerData();

//            tcpListener = new TcpListener(IPAddress.Any, PORT);
//            tcpListener.Start();
//            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCP_Callback), null);

//            Console.WriteLine($"Server started at {PORT}");


//        }


//        private static void TCP_Callback(IAsyncResult result)
//        {
//            TcpClient tcpClient = tcpListener.EndAcceptTcpClient(result);

//            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCP_Callback), null);

//            Console.WriteLine($"{tcpClient.Client.RemoteEndPoint}");


//            for (int i = 1; i <= MAX_PLAYERS; i++)
//            {
//                if (clients[i].tcp.socket == null)
//                {
//                    clients[i].tcp.Connect(tcpClient);
//                    return;
//                }
//            }

//            Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} failed to connect - server full");


//        }



//        private static void ServerData()
//        {
//            for (int i = 1; i <= MAX_PLAYERS; i++)
//            {
//                clients.Add(i, new Client(i));
//            }
//        }
//    }
//}
