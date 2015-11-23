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
		public ActionResult Gallery(string photo, string q)
		{
			var viewModel = new GalleryViewModel
			{
				StartDate = AppConfig.Birthdate,
				EndDate = DateTime.Now.AddDays(1),
				HideThumbs = false
			};

			if (string.IsNullOrEmpty(photo) && string.IsNullOrEmpty(q))
			{
				viewModel.Photos = pictureRepo.GetLatestPhotos();
			}
			else if(!string.IsNullOrEmpty(photo))
            {
				using (MiniProfiler.Current.Step("Getting Photos From Repo"))
				{
					viewModel.Photos = pictureRepo.GetPhotoById(photo);
					viewModel.HideThumbs = true;
				}
			}
            else if (!string.IsNullOrEmpty(q))
            {
                using (MiniProfiler.Current.Step("Getting Photos by shared query"))
                {
                    ConvertQueryStringParametersToViewModel(q, viewModel);
                    viewModel.Photos = pictureRepo.SearchPhotos(viewModel.SearchString, viewModel.StartDate, viewModel.EndDate);
                }
            }
			
			return View(viewModel);
		}

	    [HttpPost]
		[Route("")]
		public ActionResult Gallery(GalleryViewModel viewModel)
		{
			viewModel.Photos = pictureRepo.SearchPhotos(viewModel.SearchString, viewModel.StartDate, viewModel.EndDate);
			viewModel.HideThumbs = false;
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

	    [HttpGet]
	    [Route("VideoFilms")]
	    public ActionResult Video()
	    {
            return View(new VideoViewModel
            {
                Videos = pictureRepo.SearchPhotos("video", AppConfig.Birthdate, DateTime.Now.AddDays(1))
            });
	    }

        private void ConvertQueryStringParametersToViewModel(string q, GalleryViewModel viewModel)
        {
            var query = HttpUtility.UrlDecode(q);
            var queryArr = query.Split('&');
            foreach (var parameter in queryArr)
            {
                var paramValue = parameter.Split('=');
                switch (paramValue[0])
                {
                    case "w":
                        viewModel.SearchString = paramValue[1];
                        break;
                    case "s":
                        viewModel.StartDate = Convert.ToDateTime(paramValue[1]);
                        break;
                    case "e":
                        viewModel.EndDate = Convert.ToDateTime(paramValue[1]);
                        break;
                }
            }
        }
	}
}