using static System.Console;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server_Code
{
    class Program
    {
        static Socket server;
        static Socket client;
        static NetworkStream ns;
        static void InitServer()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2009);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iep);
            server.Listen(10);
        }

        static void AcceptClient()
        {
            client = server.Accept();
            WriteLine("Chap nhan ket noi tu : " + client.RemoteEndPoint.ToString());
            ns = new NetworkStream(client);
        }

        static void WriteData(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            ns.Write(data, 0, data.Length);
        }

        static string ReadData()
        {
            byte[] data = new byte[1024];
            int rec = ns.Read(data, 0, data.Length);
            return Encoding.ASCII.GetString(data, 0, rec);
        }

        static void Main(string[] args)
        {
            InitServer();
            AcceptClient();
            while (true)
            {
                string rd = ReadData();
                WriteLine(rd);

                if (rd.Equals("QUIT")) break;

                WriteData(rd.ToUpper());
            }
            client.Close();
            server.Close();
        }
    }
}
