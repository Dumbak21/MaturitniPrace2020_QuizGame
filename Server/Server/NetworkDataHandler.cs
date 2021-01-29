using System;
using System.Collections.Generic;
using System.Text;

namespace Server
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
            { (int)Packets.C_ConnectionOK, ConnectionOK }
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
    }
}
