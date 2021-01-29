using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    class ServerTCP
    {
        private static Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static int MAX_PLAYERS = 50;

        public static Client[] _clients = new Client[MAX_PLAYERS];

        private static byte[] _buffer = new byte[1024];
        public static int PORT { get; set; } = 5555;

        public static void Setup()
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                _clients[i] = new Client();
            }

            _socket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            _socket.Listen(10);
            _socket.BeginAccept(new AsyncCallback(AcceptCB), null);
            Console.WriteLine("Listening");
        }


        private static void AcceptCB(IAsyncResult res)
        {
            Socket socket = _socket.EndAccept(res);
            _socket.BeginAccept(new AsyncCallback(AcceptCB), null);

            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if(_clients[i].socket == null)
                {
                    _clients[i].socket = socket;
                    _clients[i].index = i;
                    _clients[i].ip = socket.RemoteEndPoint.ToString();
                    _clients[i].StartClient();
                    Console.WriteLine("Connected from {0}", _clients[i].ip);
                    SendResult(i);
                    return;
                }
            }
        }


        public static void SendData(int index, byte[] data)
        {
            byte[] size = new byte[4];
            size[0] = (byte)data.Length;
            size[1] = (byte)(data.Length >> 8);
            size[2] = (byte)(data.Length >> 16);
            size[3] = (byte)(data.Length >> 24);

            _clients[index].socket.Send(size);
            _clients[index].socket.Send(data);
        }

        public static void SendResult(int index)
        {
            DataBuffer buffer = new DataBuffer();
            buffer.WriteInt((int)Packets.S_ConnectionOK);
            buffer.WriteString("OKKKK");
            SendData(index, buffer.ToArray());
            buffer.Dispose();
        }
    }

    class Client
    {
        public int index;
        public string ip;
        public Socket socket;
        public bool closing = false;
        private byte[] _buffer = new byte[1024];

        public void StartClient()
        {
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCB), socket);
            closing = false;
        }

        private void RecieveCB(IAsyncResult res)
        {
            Socket socket = (Socket)res.AsyncState;

            try
            {
                int recieved = socket.EndReceive(res);
                if(recieved <= 0)
                {
                    CloseClient(index);
                }
                else
                {
                    byte[] buffer = new byte[recieved];
                    Array.Copy(_buffer, buffer, recieved);

                    NetworkDataHandler.HandleData(index, buffer);

                    socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecieveCB), socket);
                }
            }
            catch
            {
                CloseClient(index);
            }

        }

        private void CloseClient(int index)
        {
            closing = true;
            Console.WriteLine("Connection {0} has been terminated", ip);
            socket.Close();
        }

    }
}
