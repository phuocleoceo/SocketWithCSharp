using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using StudyWebSocket.SocketManager;

namespace StudyWebSocket.Handlers
{
	public class WebSocketMessageHandler : SocketHandler
	{
		public WebSocketMessageHandler(ConnectionManager connections) : base(connections) { }

		public override async Task OnConnected(WebSocket socket)
		{
			await base.OnConnected(socket);
			string socketId = Connections.GetIdOfSocket(socket);
			await SendMessageToAll($"{socketId} just joined the party ****");
		}

		public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
		{
			string socketId = Connections.GetIdOfSocket(socket);
			string message = $"{socketId} said : {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
			await SendMessageToAll(message);
		}
	}
}