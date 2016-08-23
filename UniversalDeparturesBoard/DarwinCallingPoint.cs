using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DarwinFeed
{
    public class DarwinCallingPoint
    {
        public DarwinCallingPoint()
        {

        }

        public DarwinCallingPoint(
            string locationName,
            string crs,
            DateTime? scheduledTime,
            DateTime? estimatedTime,
            DateTime? actualTime,
            bool isCancelled,
            int length,
            bool detachFront,
            string[] adhocAlerts
            )
        {
            this.locationName = locationName;
            this.crs=crs;
            this.scheduledTime =scheduledTime;
            this.estimatedTime = estimatedTime;
            this.actualTime = actualTime;
            this.isCancelled = isCancelled;
            this.length = length;
            this.detachFront = detachFront;
            this.adhocAlerts= adhocAlerts;
        }

        string locationName;
        /// <summary>
        /// The display name of this location.
        /// </summary>
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }

        string crs;
        /// <summary>
        /// The CRS code of this location. A CRS code of ??? indicates an error situation where no crs code is known for this location.
        /// </summary>
        public string CRS
        {
            get { return crs; }
            set { crs = value; }
        }

        DateTime? scheduledTime;
        /// <summary>
        /// The scheduled time of the service at this location. The time will be either an arrival or departure time, depending on whether it is in the subsequent or previous calling point list.
        /// </summary>
        public DateTime? ScheduledTime
        {
            get { return scheduledTime; }
            set { scheduledTime = value; }
        }

        DateTime? estimatedTime;
        /// <summary>
        /// The estimated time of the service at this location. The time will be either an arrival or departure time, depending on whether it is in the subsequent or previous calling point list. Will only be present if an actual time (at) is not present.
        /// </summary>
        public DateTime? EstimatedTime
        {
            get { return estimatedTime; }
            set { estimatedTime = value; }
        }

        DateTime? actualTime;
        /// <summary>
        /// The actual time of the service at this location. The time will be either an arrival or departure time, depending on whether it is in the subsequent or previous calling point list. Will only be present if an estimated time (et) is not present.
        /// </summary>
        public DateTime? ActualTime
        {
            get { return actualTime; }
            set { actualTime = value; }
        }

        bool isCancelled;
        /// <summary>
        /// A flag to indicate that this service is cancelled at this location.
        /// </summary>
        public bool IsCancelled
        {
            get { return isCancelled; }
            set { isCancelled = value; }
        }

        int length;
        /// <summary>
        /// The train length (number of units) at this location. If not supplied, or zero, the length is unknown.
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        bool detachFront;
        /// <summary>
        /// True if the service detaches units from the front at this location.
        /// </summary>
        public bool DetachFront
        {
            get { return detachFront; }
            set { detachFront = value; }
        }

        string[] adhocAlerts;
        /// <summary>
        /// A list of active Adhoc Alert texts for to this location. This list contains an object called AdhocAlertTextType which contains a string to show the Adhoc Alert Text for the locaiton. 
        /// </summary>
        public string[] AdhocAlerts
        {
            get { return adhocAlerts; }
            set { adhocAlerts = value; }
        }
    }
}
