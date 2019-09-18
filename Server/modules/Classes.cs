using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public class config
        {
            public string Server { get; set; }
            public string Port { get; set; }
            public string Database { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public bool AllowZeroDatetime { get; set; }
            public bool ConvertZeroDatetime { get; set; }



            public string Read(string filepath)
            {
                string text = File.ReadAllText(filepath);
                config c = new config();
                JsonConvert.PopulateObject(text, c);
                return "Server=" + c.Server + "; port=" + c.Port + "; database=" + c.Database + "; user=" + c.User + "; password=" + c.Password + "; Allow Zero Datetime=" + c.AllowZeroDatetime + "; Convert Zero Datetime=" + c.ConvertZeroDatetime + ";";
            }
        }
    }
}
