using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Model
{
    public class Room
    {
        public int Code { get; set; }


        //pole 4 hráčů
        public Player[] players { get; set; }

        //json
        public string actualQuestion { get; set; }

        public Player actualPlayer { get; set; }


    }
}
