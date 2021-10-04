using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	class Program
	{
		static TcpListener server;
		static TcpClient worker;
		static NetworkStream stream;
		static StreamReader reader;
		static StreamWriter writer;

		static void InitServer()
		{
			server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1308);
			server.Start(10);
			Console.WriteLine($"<< Server started at {server.LocalEndpoint} >>");
		}

		static async Task InitStream()
		{
			worker = await server.AcceptTcpClientAsync();
			stream = worker.GetStream();
			reader = new StreamReader(stream);
			writer = new StreamWriter(stream) { AutoFlush = true };
		}

		static async Task Handle()
		{
			while (true)
			{
				await InitStream();

				string request = reader.ReadLine();
				Console.WriteLine($">> Request from {worker.Client.RemoteEndPoint} : {request}");

				worker.Close();
			}
		}

		static async Task Main(string[] args)
		{
			Console.Title = "Server";
			Console.OutputEncoding = Encoding.UTF8;
			InitServer();
			await Handle();
			Console.ReadKey();
		}
	}
}