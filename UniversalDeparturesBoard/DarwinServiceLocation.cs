using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DarwinFeed
{
    public class DarwinServiceLocation
    {
        public DarwinServiceLocation(
        /// <summary>
        /// The name of the location.
        /// </summary>
            string locationName,
        /// <summary>
        /// The CRS code of this location. A CRS code of ??? indicates an error situation where no crs code is known for this location.
        /// </summary>
        string crs,
            /// <summary>
            /// An optional via text that should be displayed after the location, to indicate further information about an ambiguous route. Note that vias are only present for ServiceLocation objects that appear in destination lists.
            /// </summary>
        string via,
            /// <summary>
            /// A text string contianing service type (Bus/Ferry/Train) to which will be changed in the future.
            /// </summary>
        string futureChangeTo,
            /// <summary>
            /// This origin or destination can no longer be reached because the association has been cancelled. 
            /// </summary>
        bool assocIsCancelled
            )
        {
            this.locationName = locationName;
            this.crs = crs;
            this.via = via;
            this.futureChangeTo = futureChangeTo;
            this.assocIsCancelled = assocIsCancelled;
        }

        string locationName;
        /// <summary>
        /// The name of the location.
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

        string via;
        /// <summary>
        /// An optional via text that should be displayed after the location, to indicate further information about an ambiguous route. Note that vias are only present for ServiceLocation objects that appear in destination lists.
        /// </summary>
        public string Via
        {
            get { return via; }
            set { via = value; }
        }
        string futureChangeTo;
        /// <summary>
        /// A text string contianing service type (Bus/Ferry/Train) to which will be changed in the future.
        /// </summary>
        public string FutureChangeTo
        {
            get { return futureChangeTo; }
            set { futureChangeTo = value; }
        }

        bool assocIsCancelled;
        /// <summary>
        /// This origin or destination can no longer be reached because the association has been cancelled. 
        /// </summary>
        public bool AssocIsCancelled
        {
            get { return assocIsCancelled; }
            set { assocIsCancelled = value; }
        }
    }
}
