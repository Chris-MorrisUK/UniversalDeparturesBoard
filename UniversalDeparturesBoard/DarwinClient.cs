using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using UniversalDeparturesBoard.DawinWebService;

namespace DarwinFeed
{
    public class DarwinClient
    {


        public static  DarwinDepartureBoard GetGetEntireBoard(string crs, int offset, int window)
        {
            try
            {
                LDBServiceSoapClient client = new LDBServiceSoapClient();
                StationBoardWithDetails board = null;
                using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                {
                    MessageHeader authHeader = MessageHeader.CreateHeader("AccessToken", @"http://thalesgroup.com/RTTI/2013-11-28/Token/types", DarwinToken.TheAccessToken);
                    OperationContext.Current.OutgoingMessageHeaders.Add(authHeader);

                    //Note that this is called on a background thread - I don't need to make it async. Doing so causes a range of exciting race hazards further on
                    GetDepBoardWithDetailsResponse response = client.GetDepBoardWithDetailsAsync(0, crs, string.Empty, FilterType.from, offset, window).Result;
                    board = (StationBoardWithDetails)response.GetStationBoardResult;
                }
                return DarwinDepartureBoardFactory.CreateDepartureBoard(board);
            }
            catch (AggregateException ae)
            {
                crashDump(ae);
                return null;
            }
            catch (System.Runtime.InteropServices.SEHException seh)
            {
                crashDump(seh);
                return null;
            }           
            
        }

        private static async void crashDump(Exception ex)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
             Windows.Storage.StorageFile crashDump = await storageFolder.CreateFileAsync("crash.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);
            await Windows.Storage.FileIO.WriteTextAsync(crashDump, DateTime.Now.ToString());
            await Windows.Storage.FileIO.WriteTextAsync(crashDump, ex.ToString());
            await Windows.Storage.FileIO.WriteTextAsync(crashDump, ex.StackTrace);
            if (ex.InnerException != null)
                await Windows.Storage.FileIO.WriteTextAsync(crashDump, ex.InnerException.ToString());
        }
    }
}
