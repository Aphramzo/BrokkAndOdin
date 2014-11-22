using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrokkAndOdin.Models;

namespace BrokkAndOdin.Repos
{
	public interface IPictureRepo
	{
		IList<Photo> GetLatestPhotos();
		IList<Photo> GetLatestPhotos(int pageNumber);
		IList<Photo> GetPhotoById(string photo);
		IList<Photo> SearchPhotos(string searchString, DateTime? startDate, DateTime? endDate);
	}
}
