using System;
using System.IO;
using System.Threading.Tasks;

namespace GET_STREAM_AND_BYTES
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MainClient httpclient = new MainClient();

            // Tải dữ liệu - trả về mảng byte[]
            string url1 = "https://raw.githubusercontent.com/xuanthulabnet/jekyll-example/master/images/jekyll-01.png";
            byte[] bytes_img1 = await httpclient.DownloadDataBytes(url1);

            //Tải dữ liệu - trả về stream
            string url2 = "https://raw.githubusercontent.com/xuanthulabnet/linux-centos/master/docs/samba1.png";
            Stream stream_img2 = await httpclient.DownloadDataStream(url2);

            // Lưu mảng ra file anh1.png
            string filepath = "anh1.png";
            using (FileStream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                stream.Write(bytes_img1, 0, bytes_img1.Length);
                Console.WriteLine("Saved " + filepath);
            }

            int SIZEBUFFER = 500;
            // Đọc dữ liệu từ stream trả về, lưu ra file anh2.pnng
            string filepath2 = "anh2.png";
            using (FileStream streamwrite = File.OpenWrite(filepath2))
            {
                using Stream streamread = stream_img2;
                byte[] buffer = new byte[SIZEBUFFER]; // tạo bộ nhớ đệm lưu dữ liệu khi đọc stream
                bool endread = false;
                do
                {
                    // Đọc stream nhiều lần tránh sót dữ liệu, đang chỉ đọc mỗi lần được SIZEBUFFER byte
                    int numberRead = streamread.Read(buffer, 0, SIZEBUFFER);
                    if (numberRead == 0) endread = true;
                    else
                    {
                        streamwrite.Write(buffer, 0, numberRead);
                    }
                } while (!endread);
            }            
            Console.WriteLine("Saved " + filepath2);
        }
    }
}
