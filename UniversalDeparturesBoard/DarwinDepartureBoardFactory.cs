using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversalDeparturesBoard.DawinWebService;

namespace DarwinFeed
{
    //As and when the webservice data contract alters, this is the class to edit. Not the DarwinXXX business objects, unless you need to wish a field.
    public class DarwinDepartureBoardFactory
    {
        public static DarwinDepartureBoard CreateDepartureBoard(StationBoardWithDetails webserviceStationBoard)
        { 
            List<string> nrccMsgs = createMessageList(webserviceStationBoard.nrccMessages);
            DarwinDepartureBoard departureBoard = new DarwinDepartureBoard(nrccMsgs, webserviceStationBoard.platformAvailable, webserviceStationBoard.areServicesAvailable);
            List<DarwinService> railServices = createRailServices(webserviceStationBoard.trainServices);
            foreach (DarwinService service in railServices)            
                departureBoard.RailServices.Add(service.ServiceID, service);
            
            return departureBoard;
        }

        private static List<DarwinService> createRailServices(ServiceItemWithCallingPoints[] serviceItemWithCallingPoints)
        {
            List<DarwinService> services = new List<DarwinService>();
            foreach (ServiceItemWithCallingPoints serviceFromWeb in serviceItemWithCallingPoints)
            {
                DarwinService toAdd = createSingleRailService(serviceFromWeb);
                services.Add(toAdd);
            }
            return services;
        }

        private static DarwinService createSingleRailService(ServiceItemWithCallingPoints serviceFromWeb)
        {

            DateTime? sta = null;
            if(!string.IsNullOrEmpty(serviceFromWeb.sta))
                sta = DateTime.Parse(serviceFromWeb.sta);
            DateTime? eta = ParseEstimatedDateTime(serviceFromWeb.eta, sta);
            DateTime? std = null;
            if (!string.IsNullOrEmpty(serviceFromWeb.std))
                std = DateTime.Parse(serviceFromWeb.std);
            DateTime? etd = ParseEstimatedDateTime(serviceFromWeb.etd, std);   
            
            DarwinService result = new DarwinService(
                /*serviceFromWeb.rsid -- this seems to have disappeared from the webservice?*/ string.Empty , sta, eta, std, etd, serviceFromWeb.platform,
                serviceFromWeb.@operator, serviceFromWeb.operatorCode, serviceFromWeb.isCircularRoute, serviceFromWeb.isCancelled,
                serviceFromWeb.filterLocationCancelled, (DarwinServiceType)(int)serviceFromWeb.serviceType, serviceFromWeb.length,
                serviceFromWeb.detachFront, serviceFromWeb.isReverseFormation
                , serviceFromWeb.delayReason, serviceFromWeb.cancelReason, serviceFromWeb.serviceID, serviceFromWeb.adhocAlerts
                );

            result.CurrentDestinations = createDarwinServiceLocationArray(serviceFromWeb.currentDestinations).ToArray();
            result.CurrentOrigins = createDarwinServiceLocationArray(serviceFromWeb.currentOrigins).ToArray();
            result.Destination = createDarwinServiceLocationArray(serviceFromWeb.destination).ToArray();
            result.Origin = createDarwinServiceLocationArray(serviceFromWeb.origin).ToArray();
            result.PreviousCallingPoints = createCallingPointList(serviceFromWeb.previousCallingPoints);
            result.SubsequentCallingPoints = createCallingPointList(serviceFromWeb.subsequentCallingPoints);
            return result;
        }

        private static List<DarwinCallingPoint> createCallingPointList(ArrayOfCallingPoints[] arrayOfCallingPoints)
        {
            
            List<DarwinCallingPoint> result = new List<DarwinCallingPoint>();
            if(arrayOfCallingPoints==null)
                return result;
            foreach (ArrayOfCallingPoints arrayOfPoints in arrayOfCallingPoints)//This does throw away some information that I'm not modelling
                foreach (CallingPoint cp in arrayOfPoints.callingPoint)
                {
                    DateTime? scheduled = parseScheduled(cp.st);
                    DateTime? actual = ParseEstimatedDateTime(cp.at, scheduled);//this also passes "On time" if it matches the schedule
                    DateTime? estimate = ParseEstimatedDateTime(cp.et, scheduled);
                    DarwinCallingPoint toAdd = new DarwinCallingPoint(
                        cp.locationName, cp.crs, scheduled, estimate, actual, cp.isCancelled, cp.length, cp.detachFront, cp.adhocAlerts);
                    result.Add(toAdd);

                }
            return result;
        }

        private static DateTime? parseScheduled(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            if (str.Equals(Settings.Cancelled))
                return null;
            return DateTime.Parse(str);
        }

        private static DateTime? ParseEstimatedDateTime(string str,DateTime? scheduled)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            if (str.Equals(Settings.Cancelled))
                return null;
            if (string.Equals(str, Settings.OnTime, StringComparison.OrdinalIgnoreCase))
                return scheduled;
            DateTime result;
            if (DateTime.TryParse(str, out result))
                return (DateTime?)result;
            return null;
        }

        private static List<DarwinServiceLocation> createDarwinServiceLocationArray(ServiceLocation[] serviceLocation)
        {
            List<DarwinServiceLocation> result = new List<DarwinServiceLocation>();
            if (serviceLocation == null)
                return result;
            foreach (ServiceLocation location in serviceLocation)
            {
                DarwinServiceLocation toAdd = new DarwinServiceLocation(
                    location.locationName, location.crs, location.via, location.futureChangeTo, location.assocIsCancelled);
                result.Add(toAdd);
            }
            return result;
        }

        private static List<string> createMessageList(NRCCMessage[] nRCCMessages)
        {
            List<string> msgs = new List<string>();
            if (nRCCMessages == null) return msgs;//it's often null
            foreach (NRCCMessage msg in nRCCMessages)            
                msgs.Add(msg.Value);
            return msgs;
        }
    }
}
