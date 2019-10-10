using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using static Server.modules.Classes;

namespace Server.modules
{
    class Database
    {
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
                            UserID = dp.UserID,
                            Value = dp.Value,
                            Time = dp.Time
                        }).Where(dp => dp.UserID == user.ID).ToList()
                    }));
            }
        }
        public void StartTrackingByName(List<string> names)
        {
            using (var context = new UserContext())
            {
                context.Database.EnsureCreated();
                foreach (string name in names)
                {
                    User newuser = new User();
                    newuser.Name = name;
                    context.Users.Add(newuser);
                }
                context.SaveChanges();
            }
        }

        public List<User> GetTrackedUsers()
        {
            using (var context = new UserContext())
            {
                return new List<User>(context.Users.Select(user => new User
                {
                    ID = user.ID,
                    Name = user.Name,
                    DataPoints = context.DataPoints.Select(dp => new DataPoint
                    {
                        ID = dp.ID,
                        UserID = dp.UserID,
                        Value = dp.Value,
                        Time = dp.Time
                    }).Where(dp => dp.UserID == user.ID).OrderByDescending(dp => dp.Time).ToList()
                }));
            }
        }
        public void AddDataPoint(User u, float Score)
        {
            using (var context = new UserContext())
            {
                string commandText = @"INSERT INTO `kattis`.`DataPoints` (`Time`, `Value`, `UserID`) VALUES({0},{1},{2})";
                int n = context.Database.ExecuteSqlCommand(commandText, Helper.ConvertToMySQLFormat(Helper.RoundedHour(DateTime.Now)), Score, u.ID);
            }
        }

    }
}
