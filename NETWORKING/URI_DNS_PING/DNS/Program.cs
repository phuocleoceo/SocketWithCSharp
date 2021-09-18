using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DNS
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.google.com/";
            Uri uri = new Uri(url);

            IPHostEntry hostEntry = Dns.GetHostEntry(uri.Host);
            Console.WriteLine($"Host {hostEntry.HostName} có các IP");
            hostEntry.AddressList.ToList().ForEach(ip => Console.WriteLine(ip));
            Console.ReadKey();
        }
    }
}
