using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static System.Console;

namespace Server
{
    class Program
    {
        static IPEndPoint localEndpoint;
        static EndPoint remoteEndpoint;
        static Socket socket;
        static void InitServer()
        {
            // Address of Server in Network
            localEndpoint = new IPEndPoint(IPAddress.Any, 2009);
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEndpoint);
            WriteLine($"Local socket bind to {localEndpoint}. Waiting for request !");
            remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
        }
        static void SendMessage(string message)
        {
            byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
            socket.SendTo(sendBuffer, remoteEndpoint);
        }
        static string ReceiveMessage()
        {
            // Data buffer
            int size = 1024;
            byte[] receiveBuffer = new byte[size];
            int length = socket.ReceiveFrom(receiveBuffer, ref remoteEndpoint);
            return Encoding.ASCII.GetString(receiveBuffer, 0, length);
        }
        static void Main(string[] args)
        {
            Title = "UDP Server";
            InitServer();

            while (true)
            {
                string text = ReceiveMessage();
                WriteLine($">> Receive from {remoteEndpoint} : {text}"); // default ToString() in template string

                if (text.Trim().ToUpper().Equals("QUIT")) break;

                //SendMessage(text.ToUpper());
            }
            socket.Close();
            ReadKey();
        }
    }
}
