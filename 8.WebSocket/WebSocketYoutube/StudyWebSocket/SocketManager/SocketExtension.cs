using System.Reflection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace StudyWebSocket.SocketManager
{
	public static class SocketExtension
	{
		public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
		{
			services.AddTransient<ConnectionManager>();
			foreach (Type type in Assembly.GetEntryAssembly().ExportedTypes)
			{
				if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
				{
					services.AddSingleton(type);
				}
			}
			return services;
		}

		public static IApplicationBuilder MapSockets(this IApplicationBuilder app,
											PathString path, SocketHandler socket)
		{
			return app.Map(path, c => c.UseMiddleware<SocketMiddleware>(socket));
		}
	}
}