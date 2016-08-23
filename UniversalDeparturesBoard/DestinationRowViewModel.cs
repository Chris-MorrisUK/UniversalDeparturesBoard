using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace DarwinFeed
{
    public class DestinationRowViewModel : CallingPointViewModel , INotifyPropertyChanged
    {
        public DestinationRowViewModel():base()
        {
            CallingPoints = new ObservableCollection<CallingPointViewModel>();
            Headcode = "UKNOW";
        }

        public DestinationRowViewModel(DarwinService service):this()
        {
            this.Headcode = service.Rsid;
            this.Platform = service.Platform;
            this.ScheduledDepartureTime = service.ScheduledDepartureTime;
            this.DepartureTime = service.EstimeatedDepartureTime;
            this.PointName = buildDestination(service.CurrentDestinations,service.Destination);
            this.ServiceMessage = Util.StringArrayToLines(service.AdhocAlerts);
            this.minutesLate = Util.CalculateDelayMinutes(service.ScheduledDepartureTime, service.EstimeatedDepartureTime);
            this.Cancelled = service.IsCancelled;
            if (Cancelled)
            {
                if (!string.IsNullOrEmpty(ServiceMessage))
                    ServiceMessage += Environment.NewLine;
                this.ServiceMessage += "Cancelled: " + service.CancelReason;
            }
            else if(!string.IsNullOrEmpty(service.DelayReason))
            {
                if (!string.IsNullOrEmpty(ServiceMessage))
                    ServiceMessage += Environment.NewLine;
                this.ServiceMessage += "Delayed: " + service.DelayReason;
            }
            foreach (DarwinCallingPoint cp in service.SubsequentCallingPoints)
            {
                this.CallingPoints.Add(new CallingPointViewModel(cp.LocationName, cp.EstimatedTime, cp.ScheduledTime));
            }
            
            
        }


        public ObservableCollection<CallingPointViewModel> CallingPoints { get; private set; }
        public string Headcode { get; private set; }//This doesn't change once created - no need for property changed
        public string Platform { get; private set; }//This doesn't change once created - no need for property changed
        public string ServiceMessage { get; private set; }
        public string Lateness
        {
            get
            {
               
                if (minutesLate == 0)
                    return string.Empty;
                if (minutesLate > 0)
                    return string.Format("{0} Minutes Late", minutesLate);
                if (minutesLate < 0)
                    return string.Format("{0} Minutes Early", minutesLate);
                return string.Empty;
            }
        }

        public Brush TimeColourBrush
        {
            get
            {
                if (Cancelled)
                    return BrushConstants.CancelledRed;
                //WarningTime is time in minutes before departure to start warning
                //LateTime is when a slow walk won't get you there, in minutes
                //Impossible time is when even rushing won't help (thus is the smallest number)
                if (!this.LikelyDeptarureTime.HasValue)
                    return (SolidColorBrush)Application.Current.Resources["brushText"];
                TimeSpan timeTilTrain =   this.LikelyDeptarureTime.Value - DateTime.Now;
                if (timeTilTrain.Minutes > Settings.WarningTime)
                    return (SolidColorBrush)Application.Current.Resources["brushText"];
                if (timeTilTrain.Minutes > Settings.LateTime)
                    return BrushConstants.WarningGold;
                if (timeTilTrain.Minutes > Settings.ImpossibleTime)
                {
                    
                    GradientStopCollection stops = new GradientStopCollection();

                    GradientStop start = new GradientStop();
                    start.Color = Colors.Red;                    
                    stops.Add(start);

                    double maybeZone = Settings.LateTime * 60 - Settings.ImpossibleTime * 60;//work in seconds
                    double chance = (maybeZone - timeTilTrain.Seconds) / maybeZone;
                    
                    GradientStop transition = new GradientStop();
                    transition.Color = Colors.Gold;
                    transition.Offset = chance;
                    stops.Add(transition);

                    return new LinearGradientBrush(stops,0);
                }
                //At this point you're going to miss it, even if you leave now
                return BrushConstants.DarkRed;

            }
        }
        public bool Cancelled { get; private set; }
        public bool ShowItems 
        {
            get
            {
                return !Cancelled;
            }
        }

        private int minutesLate;
    }
}
