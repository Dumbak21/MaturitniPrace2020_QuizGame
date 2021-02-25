using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    void Start()
    {
        NetworkDataHandler.InitPackets();
        Client.Connect();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void GetRandomQuestion()
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteInt((int)Packets.C_GetRandomQuestion);
        Client.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void JoinToRoom(int code)
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteInt((int)Packets.C_JoinRoom);
        buffer.WriteInt(code);
        buffer.WriteString(DataManager.NickName);
        Client.SendData(buffer.ToArray());
        buffer.Dispose();
    }


    public static void AddPlayer(string nick)
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteInt((int)Packets.C_AddPlayer);
        buffer.WriteString(nick);
        Client.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void RemovePlayer()
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteInt((int)Packets.C_RemovePlayer);
        buffer.WriteString(DataManager.NickName);
        Client.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void CreateRoom()
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteInt((int)Packets.C_CreateRoom);
        Client.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    void OnDestroy()
    {
        Client.AppStop();
    }
}
