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

        public List<User> GetUsersByName(List<string> names)
        {
            using (var context = new UserContext())
            {
                return new List<User>(context.Users.Where(user => names.Contains(user.Name))
                    .Select(user => new User
                    {
                        ID = user.ID,
                        Name = user.Name,
                        DataPoints = context.DataPoints.Select(dp => new DataPoint
                        {
                            ID = dp.ID,
                            Value = dp.Value,
                            Time = dp.Time
                        }).Where(dp => dp.ID == user.ID).ToList()
                    }));
            }
        }

    }
}
