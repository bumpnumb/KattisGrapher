using System;
using System.Collections.Generic;
using System.Text;

namespace Server.modules
{
    class Helper
    {

        public static DateTime RoundedHour(DateTime dt)
        {
            return DateTime.Parse(
              String.Format("{0:yyyy-MM-dd HH:00:00}",
                (dt.Minute > 30 ? dt.AddHours(1) : dt)
            ));
        }

        public static string ConvertToMySQLFormat(DateTime d)
        {
            return d.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
