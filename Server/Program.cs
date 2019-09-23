using Server.modules;
using System;
using System.Collections.Generic;
using static Server.modules.Classes;

namespace Server
{
    class Program
    {
        static void Main()
        {
            ClientHandler ch = new ClientHandler();


            Tracker.Track();


        }
    }
}
