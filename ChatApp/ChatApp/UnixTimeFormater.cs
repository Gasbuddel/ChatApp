using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    //Klasse zum Formatieren von Unix zu DateTime Format
    public static class UnixTimeFormater
    {
        //Aus einem Unix Format (Sek seit 1970) ein Datetime Objekt erstellen
        public static DateTime UnixToDateTime(double unixTime)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            timeStamp = timeStamp.AddSeconds(unixTime).ToLocalTime();
            return timeStamp;
        }

        //Aus einem DateTime Objekt ein Unix-Objekt erstellen
        public static double DateTimeToUnix(DateTime dateTime)
        {
            double result;
            DateTime timeStamp = new DateTime(1970, 1, 1).ToLocalTime();
            result = (dateTime - timeStamp).TotalSeconds;
            return result;
        }
    }
}
