using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;

//namespace Server
//{
//    class Client
//    {

//        public Client(int _id)
//        {
//            Id = _id;
//            tcp = new TCP(Id);
//        }
//        public int Id { get; set; }
//        public TCP tcp { get; set; }

//        public static int bufferSize = 4096;


//        public class TCP
//        {
//            public TCP(int _id)
//            {
//                id = _id;
//            }

//            private NetworkStream networkStream;
//            private byte[] recieveBuffer;

//            public TcpClient socket;
//            private readonly int id;

//            public void Connect(TcpClient _socket)
//            {
//                socket = _socket;
//                socket.ReceiveBufferSize = bufferSize;
//                socket.SendBufferSize = bufferSize;
//                networkStream = socket.GetStream();
//                recieveBuffer = new byte[bufferSize];

//                networkStream.BeginRead(recieveBuffer, 0 , bufferSize, RecieveCallback, null);
//            }

//            private void RecieveCallback(IAsyncResult result)
//            {
//                try
//                {
//                    int lenght = networkStream.EndRead(result);
//                    if(lenght <= 0)
//                    {
//                        return;
//                    }
//                    byte[] data = new byte[lenght];
//                    Array.Copy(recieveBuffer, data, lenght);

//                    networkStream.BeginRead(recieveBuffer, 0, bufferSize, RecieveCallback, null);
//                }
//                catch (Exception x)
//                {
//                    Console.WriteLine($"Error: {x}");
//                }
//            }
//        }
//    }
//}
