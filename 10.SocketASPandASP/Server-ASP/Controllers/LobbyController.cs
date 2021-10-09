using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Server_ASP.Models;

namespace Server_ASP.Controllers
{
	public class LobbyController : Controller
	{
		private readonly IMemoryCache _cache;

		public LobbyController(IMemoryCache cache)
		{
			_cache = cache;
		}

		public IActionResult Index()
		{
			if (_cache.TryGetValue<string>("RequestMsg", out string msg))
			{
				ViewBag.msg = msg;
			}
			return View();
		}
	}
}
