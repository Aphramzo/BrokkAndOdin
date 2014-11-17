using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokkAndOdin.ViewModels
{
	public class HomeViewModel
	{
		public IList<Models.Photo> Photos { get; set; }
		public string SearchString { get; set; }
	}
}