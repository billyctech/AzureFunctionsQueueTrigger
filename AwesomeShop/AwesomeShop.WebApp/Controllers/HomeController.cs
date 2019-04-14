using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AwesomeShop.WebApp.Models;
using AwesomeShop.AzureQueueLibrary.QueueConnection;
using AwesomeShop.AzureQueueLibrary.Messages;

namespace AwesomeShop.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IQueueCommunicator _queueCommunicator;

		public HomeController(IQueueCommunicator queueCommunicator)
		{
			_queueCommunicator = queueCommunicator;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult ContactUs()
		{
			ViewBag.Message = "";
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ContactUs(string contactName, string emailAddress)
		{
			var thankYouEmail = new SendEmailCommand()
			{
				To = emailAddress,
				Subject = "Thank you for reaching out",
				Body = "We will contact you shortly"
			};
			await _queueCommunicator.SendAsync(thankYouEmail);

			var adminEmail = new SendEmailCommand()
			{
				To = "admin@test.com",
				Subject = "New Contact",
				Body = $"{contactName} has reached out via contact form. Please respond back at {emailAddress}"
			};
			await _queueCommunicator.SendAsync(adminEmail);
			ViewBag.Message = "Thank you we've received your message =)";
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
