﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EntityFramework.BulkInsert.Extensions;
using System.Transactions;
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

        public string addTrip(string userID, string tripDate, string startTime,string data)
        {
            string score = "-1";
            dbManager db = new dbManager();
            trip t = new trip();
            
            t.userID = Convert.ToInt32(userID);
            t.tripDate = tripDate;
            t.startTime = startTime;
            //get an id to link the data to the trip
            int tripID = db.addUserTrip(t);  //saves to database
            //process data string
            StringReader reader = new StringReader(data);
            string line;
            var context = new drivestatsEntities();
            using (var transactionScope = new TransactionScope())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;
                int count = 0;
                List<tripData> li = new List<tripData>();
                while (reader.Peek() > -1)
                {
                    count++;
                    tripData d = new tripData();
                    d.tripID = tripID;
                    line = reader.ReadLine();
                    string lat = line.Substring(1, line.IndexOf(','));
                    line = line.Remove(0, lat.Length + 1);
                    string lon = line.Substring(0, line.IndexOf(']'));
                    line = line.Remove(0, lon.Length + 2);
                    string speed = line.Substring(0, line.IndexOf(';'));
                    line = line.Remove(0, speed.Length + 1);
                    string x = line.Substring(0, line.IndexOf(';'));
                    line = line.Remove(0, x.Length + 1);
                    string y = line.Substring(0, line.IndexOf(';'));
                    line = line.Remove(0, y.Length + 1);
                    string z = line.Substring(0, line.IndexOf(';'));
                    d.latitude = lat;
                    d.longitude = lon;
                    d.speed = Convert.ToDouble(speed.Replace('.', ','));
                    d.maxXAcelerometer = Convert.ToDouble(x.Replace('.', ','));
                    d.maxYAcelerometer = Convert.ToDouble(y.Replace('.', ','));
                    d.maxZAcelerometer = Convert.ToDouble(z.Replace('.', ','));
                    li.Add(d);

                }
                context.BulkInsert(li);
                context.SaveChanges();
                transactionScope.Complete();
            }
            score = "5.5";
            return score; 
        }

        public string UploadFile(string fileName, Stream fileStream)
        {
            string filePath = fileName;
            using (var output = File.Open(filePath, FileMode.Create))
            {
                //fileStream.CopyTo(output); testing
            }
            return "successfully added "+fileName;
        }
        public string test(string thing)
        {
            return "you called: " + thing;
        }

    }
}
