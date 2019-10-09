using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using static Server.modules.Classes;

namespace Server.modules
{
    class Tracker
    {
        public static int lastHour = DateTime.Now.Hour;

        static readonly HttpClient client = new HttpClient();


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

        public Tracker()
        {
            Console.WriteLine("Tracker has started");
            Thread timerThread = new Thread(Timer);
            timerThread.Start();
        }

        public void Timer()
        {
            //every 60 seconds, check if a new hour has begun.
            Console.WriteLine("Timer Thread has started");

            while (true)
            {
                if (TrackTimer())
                {
                    Track();
                }
                Thread.Sleep(1000);
            }
        }


        public static bool TrackTimer()
        {
            if (lastHour < DateTime.Now.Hour || (lastHour == 23 && DateTime.Now.Hour == 0))
            {
                lastHour = DateTime.Now.Hour;
                return true;
            }
            return false;
        }

        public static void Track()
        {
            Database db = new Database();
            List<User> users = db.GetTrackedUsers();

            foreach (User user in users)
            {
                Console.WriteLine("Launching Scraper for User " + user.Name);
                //var task = Task.Run(() => Scraper(user));

                Task<string> t = Scraper(user);
                t.Wait();
                string body = t.Result;

                if (body != "error")
                {
                    Parse(user, body);
                }
                else
                {
                    Console.WriteLine("Could not retrieve score for user " + user.Name);
                }
                Thread.Sleep(1000);
            }
        }

        public static void Parse(User user, string text)
        {
            //parsing body to find score

            int last = text.LastIndexOf("<td>");

            string sub = text.Substring(last + 4, 6);

            for (int i = sub.Length - 1; i >= 0; i--)
            {
                if (sub[i] < 46 || sub[i] > 57 || sub[i] == 47)
                    sub = sub.Substring(0, i);
            }

            float score = float.Parse(sub, CultureInfo.InvariantCulture);

            // end parsing

            // first check is user has no datapaints, then check if highest score is not same as fetched (its sorted as highest score = lowest index)
            if (user.DataPoints.Count == 0 || score != user.DataPoints[0].Value) 
            {
                Console.WriteLine("User " + user.Name + " has a new score of " + score);

                Database db = new Database();
                db.AddDataPoint(user, score);
            }
            else
                Console.WriteLine("User " + user.Name + " has a unchanged score of " + score);
        }



        public static async Task<string> Scraper(User user)
        {
            Console.WriteLine("Launched task for user " + user.Name);

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://open.kattis.com/users/" + user.Name);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "error";
            }
        }


    }
}
