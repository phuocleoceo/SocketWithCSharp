using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Server_ASP.Extensions
{
	public static class ServiceProviderExtensions
	{
		public static TWorkerType GetHostedService<TWorkerType>(this IServiceProvider serviceProvider)
		{
			return serviceProvider.GetServices<IHostedService>()
								  .OfType<TWorkerType>().FirstOrDefault();
		}
	}
}