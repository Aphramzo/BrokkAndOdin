using BrokkAndOdin.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokkAndOdin.ViewModels;

namespace BrokkAndOdin.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPictureRepo pictureRepo;

		public HomeController(IPictureRepo _pictureRepo)
		{
			pictureRepo = _pictureRepo;
		}

		[HttpGet]
		public ActionResult Index()
		{
			var viewModel = new HomeViewModel
			{
				Photos = pictureRepo.GetLatestPhotos(),
				StartDate = AppConfig.Birthdate,
				EndDate = DateTime.Now.AddDays(1)
			};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Index(HomeViewModel viewModel)
		{
			viewModel.Photos = pictureRepo.SearchPhotos(viewModel.SearchString, viewModel.StartDate, viewModel.EndDate);
			return View(viewModel);
		}

	}
}