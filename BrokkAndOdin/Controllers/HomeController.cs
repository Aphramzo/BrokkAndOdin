using BrokkAndOdin.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrokkAndOdin.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var picRepo = new FlickrPictureRepo();
			var picUrl = picRepo.GetPhotoUrl();
			ViewBag.FirstPicUrl = picUrl;
			return View();
		}


	}
}