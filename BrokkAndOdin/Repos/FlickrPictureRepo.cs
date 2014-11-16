using FlickrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BrokkAndOdin.Repos
{
	public class FlickrPictureRepo
	{
		public string GetPhotoUrl()
		{
			var flickr = new Flickr(ConfigurationManager.AppSettings["FlickrKey"], ConfigurationManager.AppSettings["FlickrSecert"]);
			var set = flickr.PhotosetsGetList("129426516@N03");
			var photos = flickr.PhotosetsGetPhotos(set.First().PhotosetId);
			return photos.First().LargeUrl;
		}
	}
}