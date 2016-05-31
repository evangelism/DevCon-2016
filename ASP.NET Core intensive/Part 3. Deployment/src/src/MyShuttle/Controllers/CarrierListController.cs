using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShuttle.Data;
using MyShuttle.Web.Models;

namespace MyShuttle.Web.Controllers
{
    public class CarrierListController : Controller
    {
		private ICarrierRepository _carrierRepository;

		public CarrierListController(ICarrierRepository carrierRepository)
		{
			_carrierRepository = carrierRepository;
		}

		public async Task<IActionResult> Index(SearchViewModel searchVM)
		{
			string searchString = searchVM == null ? null : searchVM.SearchString;
			var carriers = await _carrierRepository.GetCarriersAsync(searchString);
			var model = new CarrierListViewModel(carriers);
			return View("Index", model);
		}
		
    }
}
