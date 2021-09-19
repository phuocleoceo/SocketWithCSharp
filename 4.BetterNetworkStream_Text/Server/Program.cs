using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace Server
{
    class Program
    {
        static Socket server;
        static Socket worker;
        static NetworkStream stream;
        static StreamReader reader;
        static StreamWriter writer;

        static void InitServer()
        {
            server = new Socket(SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1308));
            server.Listen(10);
            Console.WriteLine($"<< Server started at {server.LocalEndPoint} >>");
        }

        static void InitStream()
        {
            worker = server.Accept();
            stream = new NetworkStream(worker);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
        }

        static void Handle()
        {
            while (true)
            {
                InitStream();

                string request = reader.ReadLine();
                Console.WriteLine($">> Request from {worker.RemoteEndPoint} : {request}");
                string response;

                switch (request.ToLower())
                {
                    case "date": response = DateTime.Now.ToLongDateString(); break;
                    case "time": response = DateTime.Now.ToLongTimeString(); break;
                    case "year": response = DateTime.Now.Year.ToString(); break;
                    case "month": response = DateTime.Now.Month.ToString(); break;
                    case "day": response = DateTime.Now.Day.ToString(); break;
                    case "dow": response = DateTime.Now.DayOfWeek.ToString(); break;
                    case "doy": response = DateTime.Now.DayOfYear.ToString(); break;
                    default: response = "UNKNOW COMMAND"; break;
                }

                writer.WriteLine(response);
                worker.Close();
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Server";
            Console.OutputEncoding = Encoding.UTF8;
            InitServer();
            Handle();
            Console.ReadKey();
        }
    }
}
