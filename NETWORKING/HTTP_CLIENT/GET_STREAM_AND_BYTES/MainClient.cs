using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GET_STREAM_AND_BYTES
{
    public class MainClient : IDisposable
    {
        public HttpClient httpClient;
        public MainClient()
        {
            httpClient = new HttpClient();
            // Thiết lập httpClient nếu muốn ở đây
        }

        // Giải phóng tài nguyên
        public void Dispose()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }
        }

        // Tải từ url trả về mảng byte dữ liệu
        public async Task<byte[]> DownloadDataBytes(string url)
        {
            Console.WriteLine($"Starting connect {url} ...");
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                byte[] data = await response.Content.ReadAsByteArrayAsync();
                Console.WriteLine("Received data success !");
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        // Tải từ url, trả về stream để đọc dữ liệu (xem bài về stream)
        public async Task<Stream> DownloadDataStream(string url)
        {
            Console.WriteLine($"Starting connect {url} ...");
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Stream stream = await response.Content.ReadAsStreamAsync();
                Console.WriteLine("Stream for read data OK !");
                return stream;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}
