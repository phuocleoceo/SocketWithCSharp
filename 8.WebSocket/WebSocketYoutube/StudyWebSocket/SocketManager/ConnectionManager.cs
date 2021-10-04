using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace StudyWebSocket.SocketManager
{
	public class ConnectionManager
	{
		// Phiên bản tối ưu đa luồng của Dictionary, cho phép nhiều luồng đọc
		private ConcurrentDictionary<string, WebSocket> _connections;

		public ConnectionManager()
		{
			_connections = new ConcurrentDictionary<string, WebSocket>();
		}

		#region BaoDong
		public ConcurrentDictionary<string, WebSocket> GetAllConnections()
		{
			return _connections;
		}

		public WebSocket GetSocketById(string id)
		{
			return _connections.FirstOrDefault(c => c.Key == id).Value;
		}

		public string GetIdOfSocket(WebSocket socket)
		{
			return _connections.FirstOrDefault(c => c.Value == socket).Key;
		}

		public async Task RemoveSocketAsync(string id)
		{
			_connections.TryRemove(id, out var socket);
			// Lấy ra thằng socket đã xoá để đóng nó đi
			await socket.CloseAsync(WebSocketCloseStatus.NormalClosure,
									"Socket Connection Closed !", CancellationToken.None);
		}
		#endregion

		#region CRUD
		private string GetConnectionId()
		{
			return Guid.NewGuid().ToString("N");
		}

		public void AddSocket(WebSocket socket)
		{
			_connections.TryAdd(GetConnectionId(), socket);
		}

		#endregion
	}
}