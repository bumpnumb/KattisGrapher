using Server.modules;
using System;
using System.Collections.Generic;
using static Server.modules.Classes;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientHandler ch = new ClientHandler();

            Database db = new Database();
            Console.WriteLine("db launched?");
            List<string> names = new List<string>();
            names.Add("Stig");

            List<User> guys = db.GetUsersByName(names);
            Console.WriteLine(guys[0].Name);
        }
    }
}
