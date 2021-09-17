using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using Common;

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

                Drink d = new Drink();
                d.Deserialization_Text(request);
                Console.WriteLine(d);

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
