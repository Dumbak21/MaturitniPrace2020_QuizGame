using Server.ServerLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Network
{
    class NetworkDataHandler
    {
        private delegate void _Packet(int index, byte[] data);
        private static Dictionary<int, _Packet> _Packets;

        public static void InitPackets()
        {
            Console.WriteLine("Init net");
            _Packets = new Dictionary<int, _Packet>
            {
                { (int)Packets.C_ConnectionOK, ConnectionOK },
                { (int)Packets.C_GetRandomQuestion, GetRandomQuestion },
                { (int)Packets.C_JoinRoom, JoinRoom },
                { (int)Packets.C_AddPlayer, AddPlayer },
                { (int)Packets.C_CreateRoom, CreateRoom },
                { (int)Packets.C_RemovePlayer, RemovePlayer },


            };
        }


        public static void HandleData(int index, byte[] data)
        {
            int num;
            DataBuffer buffer = new DataBuffer();
            buffer.WriteBytes(data);
            num = buffer.ReadInt();
            buffer.Dispose();
            if (_Packets.TryGetValue(num, out _Packet packet))
            {
                packet.Invoke(index, data);
            }
        }

        private static void ConnectionOK(int index, byte[] data)
        {
            DataBuffer buffer = new DataBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInt();
            string msg = buffer.ReadString();
            buffer.Dispose();

            Console.WriteLine(msg);
        }

        private static void GetRandomQuestion(int index, byte[] data)
        {
            var question = Program.apiProcessor.GetRandomQuestion().Result;

            DataBuffer buffer = new DataBuffer();
            buffer.WriteInt((int)Packets.S_RandomQuestion);
            buffer.WriteString(question);
            //buffer.ReadString();
            ServerTCP.SendData(index, buffer.ToArray());
            buffer.Dispose();
        }


        private static void JoinRoom(int index, byte[] data)
        {
            DataBuffer buffer = new DataBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInt(); 
            var code = buffer.ReadInt();
            var nick= buffer.ReadString();
            var result = ServerManager.AddToRoom(nick, code);
            Console.WriteLine("Result " + result);
            buffer.Clear();

            //send result of finding/adding to room
            buffer.WriteInt((int)Packets.S_RoomJoinResponse);
            buffer.WriteInt((int)result);

            ServerTCP.SendData(index, buffer.ToArray());
            buffer.Dispose();
        }

        private static void AddPlayer(int index, byte[] data)
        {
            DataBuffer buffer = new DataBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInt();
            var nick = buffer.ReadString();

            var result = ServerManager.AddPlayer(nick, index);

            Console.WriteLine("Result " + result);
            buffer.Clear();
            buffer.WriteInt((int)Packets.S_PlayerAddResponse);
            buffer.WriteInt((int)result);

            ServerTCP.SendData(index, buffer.ToArray());
            buffer.Dispose();
        }

        private static void RemovePlayer(int index, byte[] data)
        {
            DataBuffer buffer = new DataBuffer();
            buffer.WriteBytes(data);
            buffer.ReadInt();
            var nick = buffer.ReadString();

            ServerManager.RemovePlayerByNick(nick);
        }

        private static void CreateRoom(int index, byte[] data)
        {
            var room = ServerManager.CreateNewRoom();
            var response = ServerManager.AddToRoomById(index, room.Code);

            DataBuffer buffer = new DataBuffer();
            buffer.WriteInt((int)Packets.S_RoomCreateResponse);
            buffer.WriteInt((int)response);
            buffer.WriteInt(room.Code);
            ServerTCP.SendData(index, buffer.ToArray());
            buffer.Dispose();
        }
    }
}
