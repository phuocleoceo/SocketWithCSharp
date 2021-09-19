using System;
using System.Threading.Tasks;

namespace HTTP_LISTENER_ADVANCED
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new MyHttpServer(new string[] { "http://*:8080/" });
            await server.StartAsync();

            // netsh http add urlacl url=http://*:8080/ user=Admin
        }
    }
}
