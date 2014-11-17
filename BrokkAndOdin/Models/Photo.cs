using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokkAndOdin.Models
{
	public class Photo
	{
		public DateTime? DateTaken { get; set; }
		public string ThumbnailUrl { get; set; }
		public string LargeUrl { get; set; }
		public string Description { get; set;}
		public IList<String> Tags { get; set; }

		public string Title
		{
			get
			{
				if (!String.IsNullOrEmpty(Description))
					return String.Format("{0} - {1}", DateTaken.Value.ToShortDateString(), Description);
				else
					return DateTaken.Value.ToShortDateString();
			}
		}
	}
}