using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDataHandler 
{
    private delegate void _Packet(byte[] data);
    private static Dictionary<int, _Packet> _Packets;

    public static void InitPackets()
    {
        //Console.WriteLine("Init net");
        Debug.Log("Init net");

        _Packets = new Dictionary<int, _Packet>
        {
            { (int)Packets.S_ConnectionOK, ConnectionOK }
        };
    }

    public static void HandleData(byte[] data)
    {
        int num;
        DataBuffer buffer = new DataBuffer();
        buffer.WriteBytes(data);
        num = buffer.ReadInt();
        buffer.Dispose();
        if(_Packets.TryGetValue(num, out _Packet packet))
        {
            packet.Invoke(data);
        }
    }

    private static void ConnectionOK(byte[] data)
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInt();
        string msg = buffer.ReadString();
        buffer.Dispose();

        //Console.WriteLine(msg);
        Debug.Log(msg);

        Client.SendResult();
    }
}
