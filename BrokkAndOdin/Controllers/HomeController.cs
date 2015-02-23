using BrokkAndOdin.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokkAndOdin.ViewModels;
using StackExchange.Profiling;

namespace BrokkAndOdin.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPictureRepo pictureRepo;
		private readonly IUpdateRepo updateRepo;

		public HomeController(IPictureRepo _pictureRepo, IUpdateRepo _updateRepo)
		{
			pictureRepo = _pictureRepo;
			updateRepo = _updateRepo;
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
				viewModel.HideThumbs = true;
			}
			else{
				using (MiniProfiler.Current.Step("Getting Photos From Repo"))
				{
					viewModel.Photos = pictureRepo.GetPhotoById(photo);
					viewModel.HideThumbs = false;
				}
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

		[HttpGet]
		[Route("News")]
		public ActionResult News()
		{
			var updates = updateRepo.GetLatestUpdates();
			return View(new NewsViewModel()
			{
				Updates = updates
			});
		}
	}
}