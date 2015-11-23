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

	    public string ShareableLink
	    {
            get { return String.Format("q=w%3d{0}%26s%3D{1}%26e%3D{2}", HttpUtility.UrlEncode(SearchString), HttpUtility.UrlEncode(StartDate.ToString()), HttpUtility.UrlEncode(EndDate.ToString())); }
	    }
	}
}