﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Server.modules
{
    class Classes
    {
        public class User
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public List<DataPoint> DataPoints { get; set; }
        }
        public class DataPoint
        {
            public int UserID { get; set; }
            [ForeignKey("UserID")]
            public DateTime Time { get; set; }
            public float Value { get; set; }

        }

        public class Config
        {
            public string Server { get; set; }
            public string Port { get; set; }
            public string Database { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public bool AllowZeroDatetime { get; set; }
            public bool ConvertZeroDatetime { get; set; }


            //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=C: \Users\g_jes\Documents\Gilberte Jessie\aspjProject\App_Data\Database.mdf;Integrated Security=True");
            public string Read(string filepath)
            {
                string text = File.ReadAllText(filepath);
                Config c = new Config();
                JsonConvert.PopulateObject(text, c);

                return @"server=" + c.Server + ";user id=" + c.User + ";password=" + c.Password + ";initial catalog=" + c.Database + ";";
                //return "Server=" + c.Server + /*"; port=" + c.Port +*/ "; database=" + c.Database + "; user=" + c.User + "; password=" + c.Password + /*"; Allow Zero Datetime=" + c.AllowZeroDatetime + "; Convert Zero Datetime=" + c.ConvertZeroDatetime + */";";
                //"Server=yourservername;Database=replacethisstring;User ID=youruserid;Password=yourpassword;TrustServerCertificate=True;Trusted_Connection=False;Connection Timeout=30;Integrated Security=False;Persist Security Info=False;Encrypt=True;MultipleActiveResultSets=True;"
                //return "Data Source=" + c.Server +/* ";Port=" + c.Port +*/ ";Initial Catalog=" + c.Database + ";persist security info=True;user id=" + c.User + ";password=" + c.Password + ";";

                //"Data Source=127.0.0.1;Initial Catalog=mydb;persist security info=True;user id=sa;password=*****"

            }
        }
    }
}
