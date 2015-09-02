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
            var req = new RestRequest("UploadFile/{fileName}", Method.POST);

            req.AddFile("fileName", "Server.jpg");
            IRestResponse res = client.Execute(req);
            Console.WriteLine(res.Content);
            Console.ReadLine();
        }
    }
}
