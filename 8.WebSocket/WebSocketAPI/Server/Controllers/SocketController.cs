using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
	[ApiController]
	public class SocketController : ControllerBase
	{
		private readonly ILogger<SocketController> _logger;
		public SocketController(ILogger<SocketController> logger)
		{
			_logger = logger;
		}

		[HttpGet("socket/ws")]
		public async Task Get()
		{
			if (HttpContext.WebSockets.IsWebSocketRequest)
			{
				using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
				_logger.Log(LogLevel.Information, "WebSocket connection established");
				await Echo(webSocket);
			}
			else
			{
				HttpContext.Response.StatusCode = 400;
			}
		}

		private async Task Echo(WebSocket webSocket)
		{
			var buffer = new byte[1024 * 4];
			var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			_logger.Log(LogLevel.Information, "Message received from Client");

			while (!result.CloseStatus.HasValue)
			{
				var serverMsg = Encoding.UTF8.GetBytes($"Server: Hello. You said: {Encoding.UTF8.GetString(buffer)}");
				await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
				_logger.Log(LogLevel.Information, "Message sent to Client");

				result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
				_logger.Log(LogLevel.Information, "Message received from Client");

			}
			await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
			_logger.Log(LogLevel.Information, "WebSocket connection closed");
		}
	}
}