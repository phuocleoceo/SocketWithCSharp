using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SEND_ASYNC
{
    public class MainClient:IDisposable
    {
        HttpClient _httpClient = null;
        public HttpClient httpClient => _httpClient ?? (new HttpClient());  // vế trái khác null thì ok, bằng null thì lấy vế phải

        // Giải phóng tài nguyên
        public void Dispose()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
            }
        }

        // Post Json Data
        public async Task<string> SendAsyncJson(string url, string json)
        {
            Console.WriteLine($"Starting connect {url}");
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = httpContent;

                HttpResponseMessage response = await httpClient.SendAsync(request);
                string rcontent = await response.Content.ReadAsStringAsync();
                return rcontent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}
