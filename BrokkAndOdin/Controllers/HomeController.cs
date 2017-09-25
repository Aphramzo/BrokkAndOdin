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
	        var cacheName = string.Format("rememberWhen{0}", DateTime.Now.Date);
	        var cachedModel = cacheRepo.Get<RememberWhenViewModel>(cacheName);
            if (cachedModel != null)
            {
                return cachedModel;
            }

	        var date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Mountain Standard Time");

            var monthAgo = date.Date.AddMonths(-1);
	        var viewModel = new RememberWhenViewModel();

	        viewModel.TimePeriods.Add(new RememberWhenTimePeriodViewModel
	        {
	            Photos = pictureRepo.SearchPhotos(null, monthAgo, monthAgo.AddDays(1)),
	            Description = "That a month ago this happened"
	        });
        

            var ageInMonths = (DateTime.Now.Month - AppConfig.Birthdate.Month) + 12 * (DateTime.Now.Year - AppConfig.Birthdate.Year);
	        var countdownAge = ageInMonths;
            while (countdownAge > 0)
	        {
                countdownAge = countdownAge - 6;
	            if (countdownAge < 0)
	                continue;
	            var searchDate = date.Date.AddMonths(countdownAge - ageInMonths);
	            viewModel.TimePeriods.Add(new RememberWhenTimePeriodViewModel
	            {
	                Photos = pictureRepo.SearchPhotos(null, searchDate, searchDate.AddDays(1)),
	                Description = string.Format("That {0} {1} ago this happened", GetYearsAgoString(searchDate), GetMonthsAgoString(searchDate))
	            });
          
	        }
          
            //might as well cache it for a full day since it only changes every 24 hours by definition
            cacheRepo.Add(viewModel, cacheName, 1000*60*60*24);

	        return viewModel;
	    }

	    private string GetMonthsAgoString(DateTime searchDate)
	    {
            var months = DateTime.Now.Month - searchDate.Month;
	        if (months == 0)
	            return string.Empty;

	        return string.Format("{0} months", months);
	    }

	    private string GetYearsAgoString(DateTime searchDate)
	    {
            var years = DateTime.Now.Year - searchDate.Year;
	        if (years == 0)
	            return string.Empty;
	        if (years == 1)
	            return "1 year";

	        return string.Format("{0} years", years);
	    }

	    [HttpPost]
	    [Route("AnyMemories")]
	    public JsonResult AnyMemories()
	    {
	        var model = GetMemoriesViewModel();
	        foreach (var period in model.TimePeriods)
	        {
	            if (period.Photos.Any())
	            {
	                return Json(true);
	            }
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
                        //I'm not sure where it's coming from, but it appears someone has a bad querystring for filtering the pictures, 
                        //and it contains a bad date. Lets just ignore it if that is the case
                        try { viewModel.EndDate = Convert.ToDateTime(paramValue[1]); }
                        catch (Exception e) { //swallow
                        }
                        
                        break;
                }
            }
        }
	}
}