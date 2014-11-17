using FlickrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using BrokkAndOdin.Models;
using AutoMapper;

namespace BrokkAndOdin.Repos
{
	public class FlickrPictureRepo : IPictureRepo
	{
		public IList<Models.Photo> GetLatestPhotos()
		{
			var flickr = new Flickr(ConfigurationManager.AppSettings["FlickrKey"], ConfigurationManager.AppSettings["FlickrSecert"]);
			var set = flickr.PhotosetsGetList("129426516@N03");

			var flickrPhotos = flickr.PhotosetsGetPhotos(set.First().PhotosetId, PhotoSearchExtras.DateTaken | PhotoSearchExtras.Description | PhotoSearchExtras.Tags);
			var photos = Mapper.Map<IList<Models.Photo>>(flickrPhotos);
			return photos.OrderByDescending(x => x.DateTaken).ToList();
		}
	}
}