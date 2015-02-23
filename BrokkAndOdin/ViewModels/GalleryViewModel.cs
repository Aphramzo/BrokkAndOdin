using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokkAndOdin.ViewModels
{
	public class GalleryViewModel
	{
		public IList<Models.Photo> Photos { get; set; }
		public string SearchString { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string picture { get; set; }
		public bool HideThumbs { get; set; }
	}
}