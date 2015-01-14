using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokkAndOdin.ViewModels
{
	public class NewsViewModel
	{
		public IList<Models.Update> Updates { get; set; }
	}
}