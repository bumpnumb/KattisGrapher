using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
                db.StartTrackingByName(wanted);
            }

            return trackedUsers;
        }

        public static int lastHour = DateTime.Now.Hour;

        public static void TrackTimer(object source, ElapsedEventArgs e)
        {
            if (lastHour < DateTime.Now.Hour || (lastHour == 23 && DateTime.Now.Hour == 0))
            {
                lastHour = DateTime.Now.Hour;
                Track();
            }
        }

        public static void Track()
        {
            Database db = new Database();
            List<User> users = db.GetTrackedUsers();

            foreach (User user in users)
            {
                var task = Task.Run(() => Scraper(user));
            }
        }

        public static void Parse(User user, string text)
        {
            //Console.WriteLine(text);

            int last = text.LastIndexOf("<td>");

            string sub = text.Substring(last + 4, 6);

            for (int i = sub.Length - 1; i >= 0; i--)
            {
                if (sub[i] < 46 || sub[i] > 57 || sub[i] == 47)
                    sub = sub.Substring(0, i);
            }

            float score = float.Parse(sub, CultureInfo.InvariantCulture);

            if (score != user.DataPoints[0].Value)
            {
                Database db = new Database();
                db.AddDataPoint(user, score);
            }
        }


        static readonly HttpClient client = new HttpClient();

        public static async Task Scraper(User user)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://open.kattis.com/users/" + user.Name);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Parse(user, responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }


    }
}
