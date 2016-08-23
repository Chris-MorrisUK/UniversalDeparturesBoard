using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DarwinFeed
{
    //This could be more advanced - could link to racoon or some other data store. For now though I just hold it in memory
    public class DarwinDataStore 
    {
        private DarwinDataStore()
        { 

        }
        
        public DarwinDepartureBoard TheDepartureBoard { get; private set; }

        public  bool UpdateDepartureBoard()
        {
            TheDepartureBoard = DarwinClient.GetGetEntireBoard(Settings.StationCode, Settings.Offset, Settings.Window);
            return TheDepartureBoard != null;
        }

        #region singleton stuff
        private static DarwinDataStore theDataStore;
        private static object theDataStoreLock = new Object();
        public static DarwinDataStore TheDataStore
        {
            get
            {
                if (theDataStore != null)
                    return theDataStore;
                lock (theDataStoreLock)
                {
                    if (theDataStore == null)
                        theDataStore = new DarwinDataStore();
                }
                return theDataStore;
            }
        }
        #endregion


       
    }
}
