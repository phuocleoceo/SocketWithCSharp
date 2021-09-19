using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GET_STRING
{
    class Program
    {
        // In ra thông tin các Header của HTTP Response
        public static void ShowHeaders(HttpHeaders headers)
        {
            Console.WriteLine("CÁC HEADER:");
            foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
            {
                foreach (string value in header.Value)
                {
                    Console.WriteLine($"{header.Key,25} : {value}");

                }
            }
        }

        // Tải về trang web và trả về chuỗi nội dung
        public static async Task<string> GetWebContent(string url)
        {
            // Khởi tạo http client
            using HttpClient httpClient = new HttpClient();

            // Thiết lập các Header nếu cần
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
            try
            {
                // Thực hiện truy vấn GET
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Hiện thị thông tin header trả về
                ShowHeaders(response.Headers);
                Console.WriteLine("-----------------------------------------------------------------------------");

                // Phát sinh Exception nếu mã trạng thái trả về là lỗi
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Tải thành công - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");

                // Đọc nội dung content trả về - ĐỌC CHUỖI NỘI DUNG
                string htmltext = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Nhận được {htmltext.Length} ký tự");
                Console.WriteLine("-----------------------------------------------------------------------------");
                return htmltext;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string content = await GetWebContent("https://www.google.com/search?q=phuocleoceo");
            Console.WriteLine(content.Substring(0, 200) + "...");
        }

    }
}
