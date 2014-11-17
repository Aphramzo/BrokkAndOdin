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
		public ActionResult Index()
		{
			var picRepo = new FlickrPictureRepo();
			var viewModel = new HomeViewModel
			{
				Photos = picRepo.GetLatestPhotos()
			};
			return View(viewModel);
		}


	}
}