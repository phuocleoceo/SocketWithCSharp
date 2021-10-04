using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HANDLE_CLASS
{
    class Program
    {
        static async Task HttpClientHandler_Handle(string url)
        {
            // Tạo handler
            using HttpClientHandler handler = new HttpClientHandler();

            // Tạo bộ chứa cookie và sử dụng bởi handler
            CookieContainer cookies = new CookieContainer();
            // Thêm các cookie nêu muốn
            // cookies.Add(new Uri(url), new Cookie("name", "value"));

            handler.CookieContainer = cookies;

            // Tạo HttpClient - thiết lập handler cho nó
            using HttpClient httpClient = new HttpClient(handler);

            // Tạo HttpRequestMessage
            using HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0");
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("key1", "value1"),
                new KeyValuePair<string, string>("key2", "value2")
            };
            httpRequestMessage.Content = new FormUrlEncodedContent(parameters);

            // Thực hiện truy vấn
            HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);

            // Hiện thị các cookie (các cookie trả về có thể sử dụng cho truy vấn tiếp theo)
            cookies.GetCookies(new Uri(url)).ToList().ForEach(cookie =>
            {
                Console.WriteLine($"{cookie.Name} = {cookie.Value}\n");
            });

            // Đọc chuỗi nội dung trả về (HTML)
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        static async Task SocketsHttpHandler_Handle(string url)
        {
            // Tạo bộ chứa cookie và sử dụng bởi handler
            CookieContainer cookies = new CookieContainer();
            // Thêm các cookie nêu muốn
            // cookies.Add(new Uri(url), new Cookie("name", "value"));

            // Tạo handler
            using SocketsHttpHandler handler = new SocketsHttpHandler();
            handler.CookieContainer = cookies;     // Thay thế CookieContainer mặc định
            handler.AllowAutoRedirect = false;                // không cho tự động Redirect
            handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            handler.UseCookies = true;

            // Tạo HttpClient - thiết lập handler cho nó
            using HttpClient httpClient = new HttpClient(handler);

            // Tạo HttpRequestMessage
            using HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0");
            httpRequestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml+json");

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("key1", "value1"),
                new KeyValuePair<string, string>("key2", "value2")
            };
            httpRequestMessage.Content = new FormUrlEncodedContent(parameters);

            // Thực hiện truy vấn
            HttpResponseMessage response = await httpClient.SendAsync(httpRequestMessage);

            // Hiện thị các cookie (các cookie trả về có thể sử dụng cho truy vấn tiếp theo)
            cookies.GetCookies(new Uri(url)).ToList().ForEach(cookie =>
            {
                Console.WriteLine($"{cookie.Name} = {cookie.Value}\n");
            });

            // Đọc chuỗi nội dung trả về (HTML)
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        static async Task Main(string[] args)
        {
            await HttpClientHandler_Handle("https://postman-echo.com/post");
            Console.WriteLine("\n--------------------------------------------------------------------------------");
            await SocketsHttpHandler_Handle("https://postman-echo.com/post");
        }
    }
}
