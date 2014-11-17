using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrokkAndOdin.Models;

namespace BrokkAndOdin.Repos
{
	interface IPictureRepo
	{
		IList<Photo> GetLatestPhotos();
	}
}
