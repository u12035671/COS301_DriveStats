using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace driveStatsRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestService.svc or RestService.svc.cs at the Solution Explorer and start debugging.
    public class RestService : IRestService
    {
        public void DoWork()
        {
        }

        public string login(string email)
        {
            try
            {
                dbManager db = new dbManager();
                return db.getUserID(email);
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public int addTrip(int userID, string tripDate, string startLat, string startLong)
        {
            return 5;
        }

        public string UploadFile(string fileName, Stream fileStream)
        {
            string filePath = fileName;
            using (var output = File.Open(filePath, FileMode.Create))
            {
                fileStream.CopyTo(output);
            }
            return "successfully added "+fileName;
        }

    }
}
