using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversalDeparturesBoard.DawinWebService;

namespace DarwinFeed
{
    public class DarwinToken
    {
        public DarwinToken()
        {
            theToken = new AccessToken();
            theToken.TokenValue = token;
        }
        private const string token = @"Your token here";
        private AccessToken theToken;        
        public AccessToken Token
        {
            get
            {
                return theToken;
            }
        }

        private static DarwinToken theDarwinToken;
        private static Object tokenLock = new object();
        public static DarwinToken GetTheDarwinToken()
        {
            if (theDarwinToken == null)
            {
                lock (tokenLock)
                {
                    if (theDarwinToken == null)            
                        theDarwinToken = new DarwinToken();
                }
            }
            return theDarwinToken;
        
        }

        public static AccessToken TheAccessToken
        {
            get {
                return GetTheDarwinToken().Token;
            }
        }

    }
}
