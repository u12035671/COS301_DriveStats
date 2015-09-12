using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using RestSharp;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:59997/RestService.svc/");
            var req = new RestRequest("addtrip/", Method.POST);
            req.RequestFormat = DataFormat.Json;
            

            StreamReader streamReader = new StreamReader("dataSample3.dat");
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            req.AddJsonBody(new {userID="1", tripDate = "12/9/15", startTime = "11:57", data=text});
            Console.WriteLine("contacting server");
            IRestResponse res = client.Execute(req);
            Console.WriteLine(res.Content);
            Console.ReadLine();
        }
    }
}
