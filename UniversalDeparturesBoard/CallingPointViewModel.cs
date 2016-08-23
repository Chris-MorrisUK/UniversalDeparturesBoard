using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DarwinFeed
{
    public class CallingPointViewModel : INotifyPropertyChanged 
    {
        public CallingPointViewModel()
            : this("Test Calling Point", DateTime.Now,DateTime.Now)
        { 

        }
        public CallingPointViewModel(string pointName, DateTime? etd,DateTime? std)
        {
            this.pointName = pointName;
            this.departureTime = etd;
            this.scheduledDepartureTime = std;
        }

        private string pointName;
        public string PointName
        {
            get { return pointName; }
            protected set
            {
                if (pointName != value)
                {
                    pointName = value;
                    OnPropertyChanged("PointName");
                }
            }
        }

        private DateTime? departureTime;
        public DateTime? DepartureTime
        { 
            get { return departureTime; }
            protected set
            {
                if (departureTime != value)
                {
                    departureTime = value;
                    OnPropertyChanged("DepartureTimeString");
                }
            }
        }

        /// <summary>
        /// The best estimate of the departure time, not the scheduled time
        /// </summary>
        public string DepartureTimeString
        {
            get
            {
                if (departureTime.HasValue)
                    return departureTime.Value.ToString("t");
                else
                {
                    if (scheduledDepartureTime.HasValue)
                        return scheduledDepartureTime.Value.ToString("t");
                }
                return string.Empty;
            }
        }


        private DateTime? scheduledDepartureTime;
        public DateTime? ScheduledDepartureTime
        {
            get { return scheduledDepartureTime; }
            protected set
            {
                if (scheduledDepartureTime != value)
                {
                    scheduledDepartureTime = value;
                    OnPropertyChanged("DepartureTimeString");
                }
            }
        }

        public DateTime? LikelyDeptarureTime
        {
            get
            {
                return departureTime ?? scheduledDepartureTime;
            }
        }

        protected static string buildDestination(DarwinServiceLocation[] darwinServiceLocations, DarwinServiceLocation[] singularLocation)
        {            
            StringBuilder result = new StringBuilder();
            if ((singularLocation == null) && (darwinServiceLocations == null)) return string.Empty;
            foreach (DarwinServiceLocation location in singularLocation)
            {
                result.Append(location.LocationName + " ");
                if (!string.IsNullOrEmpty(location.Via))
                {
                    result.Append(location.Via.Trim() + " ");
                }
            }
            if (darwinServiceLocations.GetLength(0) > 0)
            {
                foreach (DarwinServiceLocation location in darwinServiceLocations)
                {
                    result.Append(location.LocationName + " ");
                    if (!string.IsNullOrEmpty(location.Via))
                    {
                        result.Append(location.Via + " ");
                    }
                }
            }
            if(result.Length>0)
                result.Length--;//remove the last space;
            return result.ToString();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
