using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Server.modules.Classes;

namespace Server.modules
{
    class Tracker
    {
        public static List<User> TrackerHandler(string msg)
        {
            Database db = new Database();
            List<string> wanted = new List<string>(msg.Split(',').ToList());
            List<User> trackedUsers = db.GetUsersByName(wanted);


            for (int i = wanted.Count - 1; i >= 0; i--)
            {
                foreach (User u in trackedUsers)
                {
                    if (u.Name == wanted[i])
                    {
                        wanted.Remove(wanted[i]);
                        break;
                    }
                }
            }

            if (wanted.Count != 0)
            {
                //StartTracking(wanted);

                //add wanted to users in db

            }


            return trackedUsers;
        }



    }
}
