using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System;
using Common;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    class Program
    {
        static BinaryFormatter formatter = new BinaryFormatter();
        static TcpClient client;
        static NetworkStream stream;
        static StreamReader reader;
        static StreamWriter writer;

        static void InitStream()
        {
            client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1308));
            stream = client.GetStream();
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
                DateTime ExpiryDay = new DateTime(2001, 08, 10);

                Drink d = new Drink
                {
                    Id = Id,
                    Name = Name,
                    Price = Price,
                    ExpiryDay = ExpiryDay
                };

                formatter.Serialize(stream, d);

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
