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
            { (int)Packets.S_ConnectionOK, ConnectionOK },
            { (int)Packets.S_RandomQuestion, GetQuestion },
            { (int)Packets.S_RoomJoinResponse, AddedToRoom },
            { (int)Packets.S_PlayerAddResponse, PlayerAdded },
            { (int)Packets.S_RoomCreateResponse, RoomCreated },

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

    private static void GetQuestion(byte[] data)
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInt();
        string msg = buffer.ReadString();
        QaA.Question = msg;
        buffer.Dispose();
    }
    
    private static void AddedToRoom(byte[] data)
    {

        DataBuffer buffer = new DataBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInt();
        var error = buffer.ReadInt();
        buffer.Dispose();

        DataManager.RoomResponse((Packets)error);

        //JoinGame.SetError((Packets)error);
    }

    private static void RoomCreated(byte[] data)
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInt();
        var error = buffer.ReadInt();
        var code = buffer.ReadInt();
        buffer.Dispose();

        DataManager.RoomCode = code.ToString();
        DataManager.RoomResponse((Packets)error);
    }

    private static void PlayerAdded(byte[] data)
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteBytes(data);
        buffer.ReadInt();
        var error = buffer.ReadInt();
        buffer.Dispose();

        DataManager.NickResponse((Packets)error);
    }
}
