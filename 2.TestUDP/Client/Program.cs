using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static System.Console;

namespace Client
{
    class Program
    {
        static IPEndPoint serverEndpoint;
        static Socket socket;
        static void InitClient()
        {
            // Address of Server in Network
            serverEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2009);
            socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        }
        static void SendMessage(string message)
        {
            byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
            socket.SendTo(sendBuffer, serverEndpoint);
        }
        static string ReceiveMessage()
        {
            // Endpoint to receive data
            EndPoint dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);
            // Data buffer
            int size = 1024;
            byte[] receiveBuffer = new byte[size];
            int length = socket.ReceiveFrom(receiveBuffer, ref dummyEndpoint);
            return Encoding.ASCII.GetString(receiveBuffer, 0, length);
        }
        static void Main(string[] args)
        {
            Title = "UDP Client";

            while (true)
            {
                ForegroundColor = ConsoleColor.Green;
                Write(">> Your Text : ");
                ResetColor();
                string text = ReadLine();

                InitClient(); // Cần dùng thì tạo, xong thì giải phóng socket đi =>port client mỗi lần gửi là khác nhau
                SendMessage(text);

                if (text.Trim().ToUpper().Equals("QUIT")) break;

                //string reply = ReceiveMessage();
                //WriteLine(">> Reply : " + reply);

                socket.Close();
            }
            ReadKey();
        }
    }
}
