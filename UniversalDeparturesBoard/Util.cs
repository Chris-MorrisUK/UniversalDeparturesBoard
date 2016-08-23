using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DarwinFeed
{
    public static class Util
    {
        public static string StringArrayToLines(string[] asArray)
        {
            if (asArray == null)
                return string.Empty;
            StringBuilder result = new StringBuilder();
            foreach (string line in asArray)
            {
                result.AppendLine(line);
            }
            return result.ToString();
        }

        public static int CalculateDelayMinutes(DateTime? Scheduled, DateTime? Estimated)
        {
            if ((!Scheduled.HasValue) && (!Estimated.HasValue)) return 0;
            if ((Scheduled.HasValue) && (!Estimated.HasValue)) return 0;//Estimated is always suppled when running to time
            TimeSpan difference = Estimated.Value - Scheduled.Value;
            return difference.Minutes;
        }
    }
}
