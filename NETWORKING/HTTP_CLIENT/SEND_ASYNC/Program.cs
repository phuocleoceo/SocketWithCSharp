using System;
using System.Text;
using System.Threading.Tasks;

namespace SEND_ASYNC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string url = "https://bsite.net/phuocleoceo/api/employee/";
            //string json = @"
            //    {
            //        ""id"":""1"", 
            //        ""method"":""timestampToDate"", 
            //        ""params"": {""routin"":""UnixTime"", ""timestamp"":""1483228800""}
            //    }";
            string json = "";

            MainClient client = new MainClient();
            string result = await client.SendAsyncJson(url,json);
            Console.WriteLine(result);
        }
    }
}
