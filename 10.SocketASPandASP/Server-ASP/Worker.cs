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
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Server_ASP
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly IMemoryCache _memoryCache;
		private TcpListener server;
		private TcpClient worker;
		private NetworkStream stream;
		private StreamReader reader;
		private StreamWriter writer;

		public Worker(ILogger<Worker> logger, IMemoryCache memoryCache)
		{
			_logger = logger;
			_memoryCache = memoryCache;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1308);
			server.Start(10);
			_logger.LogInformation($"<< Server started at {server.LocalEndpoint} >>");

			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					worker = await server.AcceptTcpClientAsync();
					stream = worker.GetStream();
					reader = new StreamReader(stream);
					writer = new StreamWriter(stream) { AutoFlush = true };

					string request = reader.ReadLine();
					_logger.LogWarning($">> Request from {worker.Client.RemoteEndPoint} : {request}");

					_memoryCache.Set("RequestMsg", request);
					worker.Close();
				}
				catch
				{
					await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
				}
			}
		}

		public override void Dispose()
		{
			if (server != null)
			{
				server.Server.Dispose();
			}
			base.Dispose();
		}
	}
}
