using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DarwinFeed
{
    public class DarwinDepartureBoard
    {
        public DarwinDepartureBoard()
        {
            nrccMessages = new List<string>();
            railServices = new Dictionary<string, DarwinService>();
        }

        public DarwinDepartureBoard(IEnumerable<string> msgs, bool platforms, bool services)
            : this()
        {
            nrccMessages.AddRange(msgs);
            platformAvailable = platforms;
            areServicesAvailable = services;
        }
        private readonly Dictionary<string, DarwinService> railServices;

        /// <summary>
        /// A Dictionary of Services available from this station, Keyed on ServiceID
        /// </summary>
        public Dictionary<string, DarwinService> RailServices
        {
            get
            {
                return railServices;
            }
        }


        public List<DestinationRowViewModel> GetRailServicesVM()
        {
            List<DestinationRowViewModel> result = new List<DestinationRowViewModel>();
            foreach (DarwinService service in railServices.Values)
            {
                result.Add(new DestinationRowViewModel(service));
            }
            return result;
        }
        
        private List<string> nrccMessages;
        /// <summary>
        ///  An optional list of textual messages that should be displayed with the station board. The message may include embedded and xml encoded HTML-like hyperlinks and paragraphs. The messages are typically used to display important disruption information that applies to the location that the station board was for. Any embedded <p> tags are used to force a new-line in the output. Embedded <a> tags allow links to external web pages that may provide more information. Output channels that do not support HTML should strip out the <a> tags and just leave the enclosed text.
        /// </summary>
        public List<string> NrccMessages
        {
            get { return nrccMessages; }
            set { nrccMessages = value; }
        }
        private bool platformAvailable;
        /// <summary>
        /// 	An optional value that indicates if platform information is available. If this value is present with the value "true" then platform information will be returned in the service lists. If this value is not present, or has the value "false", then the platform "heading" should be suppressed in the user interface for this station board.
        /// </summary>
        public bool PlatformAvailable
        {
            get { return platformAvailable; }
            set { platformAvailable = value; }
        }

        private bool areServicesAvailable;
        /// <summary>
        /// An optional value that indicates if services are currently available for this station board. If this value is present with the value "false" then no services will be returned in the service lists. This value may be set, for example, if access to a station has been closed to the public at short notice, even though the scheduled services are still running. It would be usual in such cases for one of the nrccMessages to describe why the list of services has been suppressed. 
        /// </summary>
        public bool AreServicesAvailable
        {
            get { return areServicesAvailable; }
            set { areServicesAvailable = value; }
        }

    }
}
