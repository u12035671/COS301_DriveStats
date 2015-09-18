using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.Collections;
using System.Net;


namespace ConsoleApplication1
{
    class smtp
    {
        public smtp()
        {
            TcpClient client = new TcpClient("localhost", 25);
            Stream s = client.GetStream();
            StreamReader sr = new StreamReader(s);
            StreamWriter sw = new StreamWriter(s);
            sw.AutoFlush = true;
            Console.WriteLine(sr.ReadLine());
            sw.WriteLine("helo localhost");
            Console.WriteLine(sr.ReadLine());
            sw.WriteLine("MAIL FROM: <u12035671@tuks.co.za>");
            Console.WriteLine(sr.ReadLine());
            sw.WriteLine("RCPT TO: <zander.boshoff@gmail.com>");
            Console.WriteLine(sr.ReadLine());
            sw.WriteLine("DATA");
            Console.WriteLine(sr.ReadLine());
            sw.WriteLine("From: u12035671@tuks.co.za.com");
            sw.WriteLine("To: zander.boshoff@gmail.com");
            sw.WriteLine("SUBJECT: Alarm has been triggered");
            sw.WriteLine("dear sir alarm (enter alarm number) has been triggered");
            sw.WriteLine(".");
            Console.WriteLine(sr.ReadLine());
            Console.WriteLine(sr.ReadLine());
            sw.WriteLine("SEND");
            Console.ReadLine();
        }
    }
}
