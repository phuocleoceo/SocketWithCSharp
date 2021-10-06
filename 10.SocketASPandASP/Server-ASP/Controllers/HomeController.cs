using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Server_ASP.Models;

namespace Server_ASP.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IMemoryCache _cache;

		public HomeController(ILogger<HomeController> logger, IMemoryCache cache)
		{
			_logger = logger;
			_cache = cache;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			if (_cache.TryGetValue<string>("RequestMsg", out string msg))
			{
				ViewBag.msg = msg;
			}
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
