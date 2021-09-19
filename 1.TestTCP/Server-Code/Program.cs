using System.Text;
using System.Net;
using System;
using System.Net.Sockets;
using static System.Console;

namespace Server_Code
{
    class Program
    {
        static Socket listener;
        static Socket worker;
        static void InitSocket()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 2008);
            // Tạo socket và liên kết với IPEndPoint cục bộ
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(iep);
            WriteLine("IPEndPoint cục bộ : " + listener.LocalEndPoint.ToString());
            // Lắng nghe với Backlog là số lượng tối đa các kết nối đang chờ : 10
            listener.Listen(10);
            WriteLine("Chờ kết nối từ Client !!!!");
        }

        static void AcceptClient()
        {
            worker = listener.Accept();
            WriteLine("Chấp nhận kết nối từ : " + worker.RemoteEndPoint.ToString());
        }

        static void SendToClient(string message)
        {
            // Chuyển thông điệp thành mảng byte
            byte[] data = Encoding.ASCII.GetBytes(message);
            // Gửi nhận dữ liệu theo giao thức đã thiết kế 
            worker.Send(data, data.Length, SocketFlags.None);
        }

        static string MessageFromClient()
        {
            byte[] data = new byte[1024];
            int recv = worker.Receive(data); // Độ dài mảng byte nhận được
            if (recv == 0) return null;
            return Encoding.ASCII.GetString(data, 0, recv);
        }

        static void Disconnect()
        {
            // Đóng 2 chiều nhận gửi của Client rồi Close server và client
            worker.Shutdown(SocketShutdown.Both);
            worker.Close();
            listener.Close();
        }

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InitSocket();
            AcceptClient();

            // LỜI CHÀO :
            SendToClient("Chao mung den voi Server " + listener.LocalEndPoint.ToString());

            while (true)
            {
                string messageFromClient = MessageFromClient();
                if (messageFromClient == null) break; // Không nhận được gì thì nghỉ
                Console.WriteLine("Client gửi lên : " + messageFromClient);

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
