using System.Text;
using System.Net;
using System.Net.Sockets;
using static System.Console;

namespace Client_Code
{
    class Program
    {
        static Socket client;
        static void InitClient()
        {
            // Xác định địa chỉ Server, tạo Socket rồi kết nối đến IPEndPoint server
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2008);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(iep);
        }

        static void SendToServer(string message)
        {
            // Chuyển thông điệp thành mảng byte
            byte[] data = Encoding.ASCII.GetBytes(message);
            // Gửi nhận dữ liệu theo giao thức đã thiết kế 
            client.Send(data, data.Length, SocketFlags.None);
        }

        static string MessageFromServer()
        {
            byte[] data = new byte[1024];
            int recv = client.Receive(data); // Số dữ liệu mà client nhận
            if (recv == 0) return null;
            return Encoding.ASCII.GetString(data, 0, recv);
        }

        static void Disconnect()
        {
            client.Disconnect(true);  // true : reuseSocket
            client.Close();
        }

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InitClient();

            // LỜI CHÀO TỪ SERVER
            string hello = MessageFromServer();
            WriteLine("Server gửi : " + hello);

            while (true)
            {
                Write("Nhập thông điệp muốn gửi : ");
                string input = ReadLine();
                // Gửi lên server
                SendToServer(input);

                // Nếu gõ QUIT thì dừng Client
                if (input.ToUpper().Equals("QUIT")) break;

                // In thông điệp mà Server gửi ra màn hình
                string messageFromServer = MessageFromServer();
                if (messageFromServer == null) break;
                WriteLine("Server gửi : " + messageFromServer);
            }

            Disconnect();
            ReadKey();
        }
    }
}
