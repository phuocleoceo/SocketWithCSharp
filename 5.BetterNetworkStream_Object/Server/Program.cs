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
        static TcpListener server;
        static TcpClient worker;
        static NetworkStream stream;
        static StreamReader reader;
        static StreamWriter writer;

        static void InitServer()
        {
            server = new TcpListener(IPAddress.Any, 1308);
            server.Start(10);
            Console.WriteLine($"<< Server started at {server.LocalEndpoint} >>");
        }

        static void InitStream()
        {
            worker = server.AcceptTcpClient();
            stream = worker.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
        }

        static void Handle()
        {
            while (true)
            {
                InitStream();

                string request = reader.ReadLine();
                Console.WriteLine($">> Request from {worker.Client.RemoteEndPoint} : {request}");

                Drink d = request.Drink_Deserialization();
                Console.WriteLine(d);
                writer.WriteLine("OK");
                //writer.WriteLine(request.ToUpper());

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
