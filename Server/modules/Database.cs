using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Server.modules.Classes;

namespace Server.modules
{
    class Database
    {

        //public void StartConnection() //example connection
        //{
        //    using (var context = new UserContext())
        //    {
        //        context.Database.EnsureCreated();
        //        Console.WriteLine("Sucessfull database connection...");

        //        context.SaveChanges();
        //    }
        //}

        public List<User> GetUsersByName (List<string> names)
        {
            List<User> users = new List<User>();
            using (var context = new UserContext())
            {
                foreach (string name in names)
                {
                    users.Add(context.Users.Where(x => x.Name == name).FirstOrDefault());                    
                }
                return users;
            }
        }

    }
}
