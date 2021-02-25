using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Network
{
    public enum Packets
    {
        //Pozdrav
        S_ConnectionOK = 1,
        C_ConnectionOK = 2,


        //Questions
        C_GetRandomQuestion = 202,
        S_RandomQuestion = 201,
        C_GotRandomQuestion = 204,

        //Room
        C_JoinRoom = 302,
        C_CreateRoom = 304,


        S_RoomJoinResponse = 401,
        S_RoomCreateResponse = 403,

        S_AddedToRoom = 301,
        S_CreatedRoom = 303,
        S_RoomFull = 403,
        S_PlayerNotFound = 405,
        S_RoomNotFound = 407,


        //Player
        C_AddPlayer = 352,
        C_RemovePlayer = 354,

        S_PlayerAddResponse = 451,
        S_AddedPlayer = 351,
        S_PlayerExists = 453
    }
}
