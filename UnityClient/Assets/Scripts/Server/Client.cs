using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using UnityEngine;

public class Client
{
    private static Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private byte[] _buffer = new byte[1024];

    public static void Connect()
    {
        _socket.BeginConnect("127.0.0.1", 5555, new AsyncCallback(ConnectCB), _socket);
    }

    //
    private static bool loop = true;
    public static void AppStop()
    {
        loop = false;
    }
    //
    private static void ConnectCB(IAsyncResult res)
    {
        _socket.EndConnect(res);
        while (loop)
        {
            OnRecieve();
        }
    }


    //
    public static void ReConnect()
    {
        _socket.BeginConnect("127.0.0.1", 5555, new AsyncCallback(ReConnectCB), _socket);
    }
    private static void ReConnectCB(IAsyncResult res)
    {
        _socket.EndConnect(res);
    }
    //

    private static void OnRecieve()
    {
        byte[] _size = new byte[4];
        byte[] _buffer = new byte[1024];

        int total = 0;
        int current = 0;

        try
        {
            current = total = _socket.Receive(_size);
            if(total <= 0)
            {
                //Console.WriteLine("Disconnected");
                Debug.Log("Disconnected");
            }
            else
            {
                while(total < _size.Length && current > 0)
                {
                    current = _socket.Receive(_size, total, _size.Length - total, SocketFlags.None);
                    total += current;
                }
                int messageSize = 0;
                messageSize |= _size[0];
                messageSize |= (_size[1] << 8);
                messageSize |= (_size[2] << 16);
                messageSize |= (_size[3] << 24);

                byte[] data = new byte[messageSize];

                total = 0;
                current = total = _socket.Receive(data, total, data.Length - total, SocketFlags.None);

                while(total < messageSize && current > 0)
                {
                    current = _socket.Receive(data, total, data.Length - total, SocketFlags.None);
                    total += current;
                }

                NetworkDataHandler.HandleData(data);
            }
        }
        catch
        {
            //Console.WriteLine("No server in touch");
            Debug.Log("No server in touch");

            //ReConnect();
        }
    }

    public static void SendData(byte[] data)
    {
        _socket.Send(data);
    }

    public static void SendResult()
    {
        DataBuffer buffer = new DataBuffer();
        buffer.WriteInt((int)Packets.C_ConnectionOK);
        buffer.WriteString("LOL HI");
        SendData(buffer.ToArray());
        buffer.Dispose();
    }
}

