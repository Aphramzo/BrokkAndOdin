using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrokkAndOdin.Models;

namespace BrokkAndOdin.Repos
{
	public interface IUpdateRepo
	{
		IList<Update> GetLatestUpdates();
	}
}