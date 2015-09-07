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
            var client = new RestClient("http://drivestatsrest.cloudapp.net/RestService.svc/");
            var req = new RestRequest("addtrip/", Method.POST);
            req.RequestFormat = DataFormat.Json;
            

            StreamReader streamReader = new StreamReader("dataSample.dat");
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            req.AddJsonBody(new {userID="2", tripDate = "7/9/15", startTime = "22:29", data=text});
            Console.WriteLine("contacting server");
            IRestResponse res = client.Execute(req);
            Console.WriteLine(res.Content);
            Console.ReadLine();
        }
    }
}
