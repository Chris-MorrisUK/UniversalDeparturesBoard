using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace DarwinFeed
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Departures = new ObservableCollection<DestinationRowViewModel>();
            message = "Not Yet Updated";
#if DEBUG
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;
#endif
            guiThreadDispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            updateTimer = new System.Threading.Timer(UpdateDisplay, this, 1000, Settings.UpdateFequency);
            tmrCurrentTimeDisplay = new DispatcherTimer();
            tmrCurrentTimeDisplay.Interval = TimeSpan.FromSeconds(1);
            tmrCurrentTimeDisplay.Tick += TmrCurrentTimeDisplay_Tick;

            tmrCurrentTimeDisplay.Start();
        }

        private void TmrCurrentTimeDisplay_Tick(object sender, object e)
        {
            OnPropertyChanged("CurrentTime");
            OnPropertyChanged("TimeSinceUpdate");
        }



        public void UpdateDisplay(object state)
        {
#if DEBUG
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;
#endif
            if (DarwinDataStore.TheDataStore.UpdateDepartureBoard())
            {
                List<DestinationRowViewModel> railservices = DarwinDataStore.TheDataStore.TheDepartureBoard.GetRailServicesVM();
#pragma warning disable 4014 //I don't want to wait for the result - there isn't one
                guiThreadDispatcher.RunAsync(CoreDispatcherPriority.Low, new DispatchedHandler(() => guiThreadUpdate(railservices)));
            }
            else
                guiThreadDispatcher.RunAsync(CoreDispatcherPriority.Low, new DispatchedHandler(() => { Message = @"Error updating board - check internet connection"; }));
#pragma warning restore 4014
        }

        private void guiThreadUpdate(List<DestinationRowViewModel> toAdd)
        {            
            buildMessages(DarwinDataStore.TheDataStore.TheDepartureBoard.NrccMessages);
            AreServicesAvailable = DarwinDataStore.TheDataStore.TheDepartureBoard.AreServicesAvailable;
            Departures.Clear();
            IEnumerable<DestinationRowViewModel> departuresInTheFure = toAdd.Where(x => ((x.LikelyDeptarureTime.HasValue) && (x.LikelyDeptarureTime.Value > DateTime.Now)));
            foreach (DestinationRowViewModel vm in departuresInTheFure)            
                Departures.Add(vm);
            timeOfLastUpdate = DateTime.Now;            
        }
        private void buildMessages(List<string> messages)
        {
            StringBuilder sbMessages = new StringBuilder();
            foreach (string line in messages)            
                sbMessages.AppendLine(line);            
            Message = sbMessages.ToString();
        }

        public ObservableCollection<DestinationRowViewModel> Departures { get; private set; }

        private string message;
        public string Message
        {
            get 
            {
                return message;
            }
            set 
            {
                if (value != message)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }
        private bool areServicesAvailable;
        public bool AreServicesAvailable
        {
            get
            {
                return areServicesAvailable;
            }
            set
            {
                if (value != areServicesAvailable)
                {
                    areServicesAvailable = value;
                    OnPropertyChanged("AreServicesAvailable");
                }
            }
        }

        private DateTime? timeOfLastUpdate;
        public string TimeSinceUpdate
        {
            get
            {
                if (timeOfLastUpdate == null)
                    return "Never Updated";
                return string.Format("Updated {0} seconds ago", (DateTime.Now - timeOfLastUpdate).Value.Seconds);
            }
        }

        public string CurrentTime
        {
            get
            {
                return DateTime.Now.ToString("T");//T = Long Time pattern, current culture
            }
        }


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly System.Threading.Timer updateTimer;
        private readonly CoreDispatcher guiThreadDispatcher;
        private readonly DispatcherTimer tmrCurrentTimeDisplay;



    }
}
