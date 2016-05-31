using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyShuttle.Web.Models;
using MyShuttle.Model;
using MyShuttle.Data;

namespace MyShuttle.Web.Controllers
{
	public class HomeController : Controller
	{
		ICarrierRepository _carrierRepository;

		[ViewDataDictionary]
		public ViewDataDictionary tempViewData { get; set; }

		public HomeController(ICarrierRepository carrierRepository)
		{
			_carrierRepository = carrierRepository;
		}

		// GET: /<controller>/
		public IActionResult Index()
		{
			tempViewData.Model = new MyShuttleViewModel()
			{
				MainMessage = "The Ultimate B2B Shuttle Service Solution"
			};
			return new ViewResult() { ViewData = tempViewData };
		}

		[HttpPost]
		public async Task<int> Post([FromBody]Carrier carrier)
		{
			return await _carrierRepository.AddAsync(carrier);
		}

	}
}
