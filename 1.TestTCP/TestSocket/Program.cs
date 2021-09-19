using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using static System.Console;

namespace TestSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ia = IPAddress.Parse("127.0.0.1");  //parse cái ip string
            IPEndPoint ie = new IPEndPoint(ia, 8000);  // tạo endpoint từ cái ip trên
            WriteLine(ie.ToString());

            Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            WriteLine("AddressFamily: {0}", test.AddressFamily);
            WriteLine("SocketType: {0}", test.SocketType);
            WriteLine("ProtocolType: {0}", test.ProtocolType);
            WriteLine("Blocking: {0}", test.Blocking);
            test.Blocking = false;
            WriteLine("new Blocking: {0}", test.Blocking);
            WriteLine("Connected: {0}", test.Connected);
            
            test.Bind(ie);  // bind (liên kết) cái test này với cái endpoint trên kia
            WriteLine(test.LocalEndPoint.ToString());
            IPEndPoint iep = (IPEndPoint)test.LocalEndPoint;
            WriteLine("Local EndPoint: {0}", iep.ToString());
            test.Close();
            ReadKey();
        }
    }
}
