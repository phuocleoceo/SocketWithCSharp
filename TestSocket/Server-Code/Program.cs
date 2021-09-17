using System.Text;
using System.Net;
using System.Net.Sockets;
using static System.Console;

namespace Server_Code
{
    class Program
    {
        static Socket server;
        static Socket client;
        static void InitSocket()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2008);
            // Tạo socket và liên kết với IPEndPoint cục bộ
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iep);
            WriteLine("IPEndPoint cục bộ : " + server.LocalEndPoint.ToString());
            // Lắng nghe với Backlog là số lượng tối đa các kết nối đang chờ : 10
            server.Listen(10);
            WriteLine("Chờ kết nối từ Client !!!!");
        }

        static void AcceptClient()
        {
            client = server.Accept();
            WriteLine("Chấp nhận kết nối từ : " + client.RemoteEndPoint.ToString());
        }

        static void SendToClient(string message)
        {
            // Chuyển thông điệp thành mảng byte
            byte[] data = Encoding.ASCII.GetBytes(message);
            // Gửi nhận dữ liệu theo giao thức đã thiết kế 
            client.Send(data, data.Length, SocketFlags.None);
        }

        static string MessageFromClient()
        {
            byte[] data = new byte[1024];
            int recv = client.Receive(data); // Số dữ liệu mà client nhận
            if (recv == 0) return null;
            return Encoding.ASCII.GetString(data, 0, recv);
        }

        static void Disconnect()
        {
            // Đóng 2 chiều nhận gửi của Client rồi Close server và client
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            server.Close();
        }

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InitSocket();
            AcceptClient();

            // LỜI CHÀO :
            SendToClient("Chao mung den voi Server " + server.LocalEndPoint.ToString());

            while (true)
            {
                string messageFromClient = MessageFromClient();
                if (messageFromClient == null) break; // Không nhận được gì thì nghỉ
                WriteLine("Client gửi lên : " + messageFromClient);

                // Nếu chuỗi nhận được là QUIT thì thoát
                if (messageFromClient.ToUpper().Equals("QUIT")) break;

                // Gửi trả lại cho Client chuỗi s in hoa
                SendToClient(messageFromClient.ToUpper());
            }

            Disconnect();
            ReadKey();
        }
    }
}
