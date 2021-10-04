using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace PING
{
    class Program
    {
        static void Main(string[] args)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send("facebook.com");

            Console.WriteLine(pingReply.Status);

            if (pingReply.Status == IPStatus.Success)
            {
                Console.WriteLine(pingReply.RoundtripTime);
                Console.WriteLine(pingReply.Address);
            }
            Console.ReadKey();
        }
    }
}
