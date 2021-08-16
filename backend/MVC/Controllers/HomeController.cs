using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public IActionResult Privacy()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> UserInfo()
		{
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var client = new HttpClient();

			var response = await client.GetUserInfoAsync(new UserInfoRequest
			{
				Address = "http://localhost:5000/connect/userinfo",
				Token = accessToken
			});

			return View(response);
		}

		[Authorize]
		public async Task<IActionResult> CallApi()
		{
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var client = new HttpClient();

			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
			var content = await client.GetStringAsync("http://localhost:5001/api/identity");

			ViewBag.Json = JArray.Parse(content).ToString();
			
			return View();
		}

		[Authorize]
		public IActionResult Logout()
		{
			return SignOut("Cookies", "oidc");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
