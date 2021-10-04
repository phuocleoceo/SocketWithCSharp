using System;
using System.Threading.Tasks;

namespace HTTP_LISTENER_ADVANCED
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MyHttpServer server = new MyHttpServer(new string[] { "http://*:8080/" });
            await server.StartAsync();

            // Run CMD as Administrator
            // netsh http add urlacl url=http://*:8080/ user=Admin
        }
    }
}
