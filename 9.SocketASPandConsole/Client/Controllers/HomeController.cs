using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Client.Models;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Text;

namespace Client.Controllers
{
	public class HomeController : Controller
	{
		// private TcpClient client;
		// private NetworkStream stream;
		// private StreamReader reader;
		// private StreamWriter writer;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> SendMessage(string message)
		{
			try
			{
				// Open stream and client.
				TcpClient client = new TcpClient();
				client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1308));
				NetworkStream stream = client.GetStream();

				// Write to stream by NetworkStream
				// byte[] data = Encoding.ASCII.GetBytes(message);
				// await stream.WriteAsync(data, 0, data.Length);

				// Write by StreamWriter
				StreamWriter sw = new StreamWriter(stream) { AutoFlush = true };
				await sw.WriteAsync(message);

				// data = new byte[1024];
				// string responseData = "";
				// int bytesRead;
				// while ((bytesRead = stream.Read(data, 0, 256)) > 0)
				// {
				// 	responseData += Encoding.ASCII.GetString(data, 0, bytesRead);
				// }
				stream.Close();
				client.Close();
				return NoContent();
				//return responseData;
			}
			catch (SocketException e)
			{
				Console.WriteLine(e.Message);
				return NoContent();
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
