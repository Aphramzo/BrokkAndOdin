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
		[Route("")]
		public ActionResult Gallery(string photo)
		{
			var viewModel = new GalleryViewModel
			{
				StartDate = AppConfig.Birthdate,
				EndDate = DateTime.Now.AddDays(1)
			};

			if (string.IsNullOrEmpty(photo))
			{
				viewModel.Photos = pictureRepo.GetLatestPhotos();
			}
			else{
				viewModel.Photos = pictureRepo.GetPhotoById(photo);
			}
			return View(viewModel);
		}

		[HttpPost]
		[Route("")]
		public ActionResult Gallery(GalleryViewModel viewModel)
		{
			viewModel.Photos = pictureRepo.SearchPhotos(viewModel.SearchString, viewModel.StartDate, viewModel.EndDate);
			return View(viewModel);
		}

	}
}