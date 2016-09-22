using BrokkAndOdin.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrokkAndOdin.Repos.interfaces;
using BrokkAndOdin.ViewModels;
using StackExchange.Profiling;

namespace BrokkAndOdin.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPictureRepo pictureRepo;
		private readonly IUpdateRepo updateRepo;
	    private readonly ICacheRepo cacheRepo;

		public HomeController(IPictureRepo _pictureRepo, IUpdateRepo _updateRepo, ICacheRepo _cacheRepo)
		{
			pictureRepo = _pictureRepo;
			updateRepo = _updateRepo;
		    cacheRepo = _cacheRepo;
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

	    [HttpGet]
	    [Route("RememberWhen")]
	    public ActionResult RememberWhen()
	    {
	        var viewModel = GetMemoriesViewModel();

	        return View(viewModel);
	    }

	    private RememberWhenViewModel GetMemoriesViewModel()
	    {
	        var cacheName = string.Format("rememberWhen{0}", DateTime.Now.AddHours(3).Date);
	        var cachedModel = cacheRepo.Get<RememberWhenViewModel>(cacheName);
            //if (cachedModel != null)
            //{
            //    return cachedModel;
            //}

            var weekAgo = DateTime.Now.AddHours(3).Date.AddDays(-7);
            var monthAgo = DateTime.Now.AddHours(3).Date.AddMonths(-1);
            var sixMonthsAgo = DateTime.Now.AddHours(3).Date.AddMonths(-6);
            var yearAgo = DateTime.Now.AddHours(3).Date.AddYears(-1);

	        var viewModel = new RememberWhenViewModel
	        {
	            WeekAgo = pictureRepo.SearchPhotos(null, weekAgo, weekAgo.AddDays(1)),
	            MonthAgo = pictureRepo.SearchPhotos(null, monthAgo, monthAgo.AddDays(1)),
	            SixMonthsAgo = pictureRepo.SearchPhotos(null, sixMonthsAgo, sixMonthsAgo.AddDays(1)),
	            YearAgo = pictureRepo.SearchPhotos(null, yearAgo, yearAgo.AddDays(1))
	        };

            //might as well cache it for a full day since it only changes every 24 hours by definition
            cacheRepo.Add(viewModel, cacheName, 1000*60*60*24);

	        return viewModel;
	    }

	    [HttpPost]
	    [Route("AnyMemories")]
	    public JsonResult AnyMemories()
	    {
	        var model = GetMemoriesViewModel();
	        if (model.YearAgo.Any() || model.WeekAgo.Any() || model.SixMonthsAgo.Any() || model.MonthAgo.Any())
	        {
	            return Json(true);
	        }

            return Json(null);
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