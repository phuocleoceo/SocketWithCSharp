using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Text;
using System.Collections.Generic;

namespace StudyWebSocket.SocketManager
{
	public abstract class SocketHandler
	{
		public ConnectionManager Connections { get; set; }

		public SocketHandler(ConnectionManager connections)
		{
			Connections = connections;
		}

		public virtual async Task OnConnected(WebSocket socket)
		{
			await Task.Run(() => Connections.AddSocket(socket));
		}

		public virtual async Task OnDisconnected(WebSocket socket)
		{
			await Connections.RemoveSocketAsync(Connections.GetIdOfSocket(socket));
		}

		public async Task SendMessage(WebSocket socket, string message)
		{
			if (socket.State != WebSocketState.Open)
				return;
			await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length),
													WebSocketMessageType.Text, true, CancellationToken.None);
		}

		public async Task SendMessage(string id, string message)
		{
			await SendMessage(Connections.GetSocketById(id), message);
		}

		public async Task SendMessageToAll(string message)
		{
			foreach (KeyValuePair<string, WebSocket> con in Connections.GetAllConnections())
			{
				await SendMessage(con.Value, message);
			}
		}

		public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
	}
}