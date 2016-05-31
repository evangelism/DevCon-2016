using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyShuttle.Web.Models;

namespace MyShuttle.Web.Controllers
{
    public class CarrierController : Controller
    {
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login(string returnUrl = null)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			try
			{
				if (ModelState.IsValid)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					return RedirectToAction("Register", "Carrier");
				}
			}
			catch (System.Exception ex)
			{

			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> LogOff()
		{
			return RedirectToAction("Index", "Home");
		}

		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					return RedirectToAction("Login", "Carrier");
				}
				else
				{
					return RedirectToAction("Register", "Carrier");
				}
			}
			catch (System.Exception ex)
			{

			}
			return RedirectToAction("Register", "Carrier");
		}

	}
}
