using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Server_ASP
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;

		public Worker(ILogger<Worker> logger)
		{
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1308);
			server.Start(10);
			_logger.LogInformation($"<< Server started at {server.LocalEndpoint} >>");

			while (true)
			{
				TcpClient worker = await server.AcceptTcpClientAsync();
				NetworkStream stream = worker.GetStream();
				StreamReader reader = new StreamReader(stream);
				StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

				string request = reader.ReadLine();
				_logger.LogWarning($">> Request from {worker.Client.RemoteEndPoint} : {request}");

				worker.Close();
			}
		}
	}
}
