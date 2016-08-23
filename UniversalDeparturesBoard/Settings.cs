using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinFeed
{
    public static class Settings
    {
        public const uint WarningTime = 15;
        public const uint LateTime = 10;
        public const uint ImpossibleTime = 7;
        public const int UpdateFequency = 15000;
        public const string Cancelled = @"Cancelled";
        public const string OnTime = @"On time";
        public const string StationCode = @"UNI";
        public const int Offset = 0;
        public const int Window = 120;
    }
}
