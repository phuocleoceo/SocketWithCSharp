using static System.Console;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client_Code
{
    class Program
    {
        static Socket client;
        static NetworkStream ns;
        static void InitClient()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2009);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(iep);
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
            InitClient();
            while (true)    
            {
                string input = ReadLine();
                WriteData(input);
                
                if (input.ToUpper().Equals("QUIT")) break;

                string s = ReadData();
                WriteLine(s);
            }
            client.Close();
        }
    }
}
