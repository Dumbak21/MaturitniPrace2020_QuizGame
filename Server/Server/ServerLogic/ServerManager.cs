using Server.Model;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.ServerLogic
{
    public static class ServerManager
    {
        public static List<Player> PlayerList = new List<Player>();
        public static List<Room> RoomList = new List<Room>();
        static Random random = new Random();

        public static Room CreateNewRoom(int code = 0)
        {
            var room = new Room { players = new Player[4] };
            if (code <= 0 || code > 9999)
            {
                room.Code = random.Next(1, 10000);

            }
            else
            {
                room.Code = code;
            }
            RoomList.Add(room);
            Console.WriteLine("Creating room {0}", room.Code);
            return room;
        }

        public static Player FindPlayerByNick(string nick)
        {
            foreach (var pl in PlayerList)
            {
                if (pl.Nickname == nick)
                {
                    return pl;
                }
            }
            Console.WriteLine("Finding player {0}", nick);
            return null;
        }

        public static Packets AddPlayer(string nick, int index)
        {
            if (FindPlayerByNick(nick) == null)
            {
                var player = new Player { Nickname = nick, Score = 0, Id = index };
                PlayerList.Add(player);
                return Packets.S_AddedPlayer;
            }
            else
            {
                return Packets.S_PlayerExists;
            }
        }

        public static Packets AddToRoomById(int index, int code)
        {
            var response = AddToRoom(FindPlayerById(index).Nickname, code);
            if(response == Packets.S_AddedToRoom)
            {
                return Packets.S_CreatedRoom;
            }
            else
            {
                return response;
            }
        }

        public static Player FindPlayerById(int index)
        {
            return PlayerList.Find(x => x.Id == index);
        }
        public static void RemovePlayerById(int index)
        {
            PlayerList.Remove(PlayerList.Find(x => x.Id == index));
        }
        public static void RemovePlayerByNick(string nick)
        {
            PlayerList.Remove(PlayerList.Find(x => x.Nickname == nick));
        }

        public static Packets AddToRoom(string playerNick, int code)
        {
            Player player = FindPlayerByNick(playerNick);
            if(player == null)
            {
                Console.WriteLine("Player {0} was not found", playerNick);
                //player not found
                return Packets.S_PlayerNotFound;
            }
            else
            {
                foreach (var room in RoomList)
                {
                    //find room by code
                    if (room.Code == code)
                    {

                        for (int i = 0; i < room.players.Length; i++)
                        {
                            if (room.players[i] == null)
                            {
                                room.players[i] = player;
                                break;
                            }
                            if (i == (room.players.Length - 1))
                            {
                                Console.WriteLine("Room {0} is full", code);
                                return Packets.S_RoomFull;
                                //room full
                            }
                        }
                        Console.WriteLine("Player {0} added to room {1}", playerNick, code);
                        //player added
                        return Packets.S_AddedToRoom;
                    }
                }
                Console.WriteLine("Room {0} not found", code);
                return Packets.S_RoomNotFound;
            }

        }
    }
}
