using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System;
using Common;

namespace Client
{
    class Program
    {
        static Socket client;
        static NetworkStream stream;
        static StreamReader reader;
        static StreamWriter writer;

        static void InitStream()
        {
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1308));
            stream = new NetworkStream(client);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
        }

        static void Handle()
        {
            while (true)
            {
                InitStream();

                Console.Write(">>Nhập Id : ");
                int Id = Convert.ToInt32(Console.ReadLine());
                Console.Write(">>Nhập Tên : ");
                string Name = Console.ReadLine();
                Console.Write(">>Nhập Giá : ");
                double Price = Convert.ToDouble(Console.ReadLine());
                DateTime ExpiryDay = new DateTime(2001,08,10);

                Drink d = new Drink
                {
                    Id = Id,
                    Name = Name,
                    Price = Price,
                    ExpiryDay = ExpiryDay
                };

                writer.WriteLine(d.Serialization_Text());

                string response = reader.ReadLine();
                Console.WriteLine(response);
                client.Close();
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Client";
            Console.OutputEncoding = Encoding.UTF8;
            Handle();
            Console.ReadKey();
        }
    }
}
