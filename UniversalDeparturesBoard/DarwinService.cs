using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DarwinFeed
{
    public class DarwinService
    {
        public DarwinService(string _rsid, 
            DateTime? _scheduledArrivalTime, 
            DateTime? _estimatedArrivalTime,
            DateTime? _scheduledDepartureTime,
            DateTime? _estimatedDepartureTime,
            string _platform,
            string _toc,
            string _operatorCode,
            bool? _isCircularRoute,
            bool _isCancelled,
            bool _filterLocationCancelled,
            DarwinServiceType _serviceType,
            int _length,
            bool _detachFront,
            bool _isReverseFormation,
            string _delayReason,
            string _cancelReason,
            string _serviceID,
            string[] _adhocAlerts
            )
        { 
            rsid = _rsid;
            scheduledArrivalTime = _scheduledArrivalTime;
            estimatedArrivalTime= _estimatedArrivalTime;
            scheduledDepartureTime= _scheduledDepartureTime;
            estimatedArrivalTime =_estimatedDepartureTime;
            platform =_platform;
            toc= _toc;
            operatorCode = _operatorCode;
            isCircularRoute = _isCircularRoute;
            isCancelled = _isCancelled;
            filterLocationCancelled = _filterLocationCancelled;
            serviceType = _serviceType;
            length = _length;
            detachFront = _detachFront;
            isReverseFormation = _isReverseFormation;
            delayReason = _delayReason;
            cancelReason = _cancelReason;
            serviceID = _serviceID;
            adhocAlerts = _adhocAlerts;
        }
        string rsid;
        /// <summary>
        /// The Retail Service ID of the service, if known.
        /// </summary>
        public string Rsid
        {
            get { return rsid; }
            set { rsid = value; }
        }
        DarwinServiceLocation[] origin;
        /// <summary>
        /// A list of ServiceLocation objects giving original origins of this service. Note that a service may have more than one original origin, if the service comprises of multiple trains that join at a previous location in the schedule. Original Origins will only be available for Arrival and Arrival & Departure station boards.
        /// </summary>
        public DarwinServiceLocation[] Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        DarwinServiceLocation[] destination;
        /// <summary>
        /// A list of ServiceLocation objects giving original destinations of this service. 
        /// Note that a service may have more than one original destination, if the service comprises of multiple trains that divide at a subsequent location in the schedule.
        /// Original Destinations will only be available for Departure and Arrival & Departure station boards.
        /// </summary>
        public DarwinServiceLocation[] Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        DarwinServiceLocation[] currentOrigins;
        /// <summary>
        /// An optional list of ServiceLocation objects giving live/current origins of this service which is not starting at original cancelled origins. Note that a service may have more than one live origin. if the service comprises of multiple trains that join at a previous location in the schedule. Live Origins will only be available for Arrival and Arrival & Departure station boards.
        /// </summary>
        public DarwinServiceLocation[] CurrentOrigins
        {
            get { return currentOrigins; }
            set { currentOrigins = value; }
        }
        
        DarwinServiceLocation[] currentDestinations;
        /// <summary>
        /// An optional list of ServiceLocation objects giving live/current destinations of this service which is not ending at original cancelled destinations. 
        /// Note that a service may have more than one live destination, if the service comprises of multiple trains that divide at a subsequent location in the schedule.
        /// Live Destinations will only be available for Departure and Arrival & Departure station boards.
        /// </summary>
        public DarwinServiceLocation[] CurrentDestinations
        {
            get { return currentDestinations; }
            set { currentDestinations = value; }
        }

        DateTime? scheduledArrivalTime;
        /// <summary>
        /// An optional Scheduled Time of Arrival of the service at the station board location. Arrival times will only be available for Arrival and Arrival & Departure station boards but may also not be present at locations that are not scheduled to arrive at the location (e.g. the origin).
        /// </summary>
        public DateTime? ScheduledArrivalTime
        {
            get { return scheduledArrivalTime; }
            set { scheduledArrivalTime = value; }
        }
        
        
        DateTime? estimatedArrivalTime;
        /// <summary>
        /// An optional Estimated Time of Arrival of the service at the station board location. Arrival times will only be available for Arrival and Arrival & Departure station boards and only where an sta time is present.
        /// </summary>
        public DateTime? EstimatedArrivalTime
        {
            get { return estimatedArrivalTime; }
            set { estimatedArrivalTime = value; }
        }

        DateTime? scheduledDepartureTime;
        /// <summary>
        /// An optional Scheduled Time of Departure of the service at the station board location. Departure times will only be available for Departure and Arrival & Departure station boards but may also not be present at locations that are not scheduled to depart at the location (e.g. the destination).
        /// </summary>
        public DateTime? ScheduledDepartureTime
        {
            get { return scheduledDepartureTime; }
            set { scheduledDepartureTime = value; }
        }

        DateTime? estimeatedDepartureTime;
        /// <summary>
        /// An optional Estimated Time of Departure of the service at the station board location. Departure times will only be available for Departure and Arrival & Departure station boards and only where an std time is present.
        /// </summary>
        public DateTime? EstimeatedDepartureTime
        {
            get { return estimeatedDepartureTime; }
            set { estimeatedDepartureTime = value; }
        }
        string platform;
        /// <summary>
        /// An optional platform number for the service at this location. This will only be present where available and where the station board platformAvailable value is "true".
        /// </summary>
        public string Platform
        {
            get { return platform; }
            set { platform = value; }
        }
        string toc;

        /// <summary>
        /// The name of the Train Operating Company that operates the service.
        /// </summary>
        public string Toc
        {
            get { return toc; }
            set { toc = value; }
        }
        string operatorCode;
        /// <summary>
        /// The code of the Train Operating Company that operates the service.
        /// </summary>
        public string OperatorCode
        {
            get { return operatorCode; }
            set { operatorCode = value; }
        }

        bool? isCircularRoute;
        /// <summary>
        /// If this value is present and has the value "true" then the service is operating on a circular route through the network and will call again at this location later on its journey. The user interface should indicate this fact to the user, to help them choose the correct service from a set of similar alternatives.
        /// </summary>
        public bool? IsCircularRoute
        {
            get { return isCircularRoute; }
            set { isCircularRoute = value; }
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
        bool filterLocationCancelled;
        /// <summary>
        /// A flag to indicate that this service is no longer stopping at the requested from/to filter location.
        /// </summary>
        public bool FilterLocationCancelled
        {
            get { return filterLocationCancelled; }
            set { filterLocationCancelled = value; }
        }

        DarwinServiceType serviceType;
        /// <summary>
        /// The type of service (train, bus, ferry) that this item represents. Note that real-time information (e.g. eta, etd, ata, atd, etc.) is only available and present for train services.
        /// </summary>
        public DarwinServiceType ServiceType
        {
            get { return serviceType; }
            set { serviceType = value; }
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
        bool isReverseFormation;
        /// <summary>
        /// True if the service is operating in the reverse of its normal formation.
        /// </summary>
        public bool IsReverseFormation
        {
            get { return isReverseFormation; }
            set { isReverseFormation = value; }
        }

        string cancelReason;
        /// <summary>
        /// A cancellation reason for this service.
        /// </summary>
        public string CancelReason
        {
            get { return cancelReason; }
            set { cancelReason = value; }
        }
        string delayReason;
        /// <summary>
        /// A delay reason for this service.
        /// </summary>
        public string DelayReason
        {
            get { return delayReason; }
            set { delayReason = value; }
        }
        string serviceID; 
        /// <summary>
        /// 	The unique service identifier of this service relative to the station board on which it is displayed. This value can be passed to GetServiceDetails to obtain the full details of the individual service.
        /// </summary>
        public string ServiceID
        {
            get { return serviceID; }
            set { serviceID = value; }
        }
        
        string[] adhocAlerts;
        /// <summary>
        /// A list of Adhoc Alerts related to this location for this service. This list contains an object called AdhocAlertTextType which contains a string to show the Adhoc Alert Text for the location. 
        /// </summary>
        public string[] AdhocAlerts
        {
            get { return adhocAlerts; }
            set { adhocAlerts = value; }
        }
        List<DarwinCallingPoint> subsequentCallingPoints;
        /// <summary>
        /// A list of CallingPoints relative to this location for this service
        /// </summary>
        public List<DarwinCallingPoint> SubsequentCallingPoints
        {
            get { return subsequentCallingPoints; }
            set { subsequentCallingPoints = value; }
        }

        List<DarwinCallingPoint> previousCallingPoints;
        /// <summary>
        /// A list of CallingPoints relative to this location for this service
        /// </summary>
        public List<DarwinCallingPoint> PreviousCallingPoints
        {
            get { return previousCallingPoints; }
            set { previousCallingPoints = value; }
        }
    }
}
