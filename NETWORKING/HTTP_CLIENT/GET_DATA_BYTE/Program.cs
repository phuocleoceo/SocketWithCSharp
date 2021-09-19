using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GET_DATA_BYTE
{
    class Program
    {
        static async Task<byte[]> DownloadDataBytes(string url)
        {
            // Khởi tạo http client
            using HttpClient httpClient = new HttpClient();
            // Thiết lập các Header nếu cần
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");

            try
            {
                // Thực hiện truy vấn GET
                HttpResponseMessage response = await httpClient.GetAsync(url);
                // Phát sinh Exception nếu mã trạng thái trả về là lỗi
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Success - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");

                // Đọc nội dung content trả về - ĐỌC CHUỖI NỘI DUNG
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                Console.WriteLine($"Got {bytes.Length} byte");
                Console.WriteLine("-----------------------------------------------------------------------------");
                return bytes;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        static async Task Main(string[] args)
        {
            var url = "https://raw.githubusercontent.com/xuanthulabnet/jekyll-example/master/images/jekyll-01.png";
            byte[] bytes = await DownloadDataBytes(url);

            string filepath = "anh1.png";
            using var stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None);
            stream.Write(bytes, 0, bytes.Length);
            Console.WriteLine("Saved " + filepath);  // bin -> debug
        }
    }
}
