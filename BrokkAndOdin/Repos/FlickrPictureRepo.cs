using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrokkAndOdin.Models;
using AutoMapper;

namespace BrokkAndOdin.Repos
{
	public class FlickrPictureRepo : IPictureRepo
	{
		public int PicturesPerPage = 50;

		public IList<Models.Photo> GetLatestPhotos()
		{
			return GetLatestPhotos(1);
		}

		public IList<Models.Photo> GetLatestPhotos(int pageNumber)
		{
			var flickr = new Flickr(AppConfig.FlickrKey, AppConfig.FlickrSecert);
			
			var flickrPhotos = flickr.PeopleGetPublicPhotos(
				AppConfig.FlickrUser, 
				pageNumber,
				PicturesPerPage,
				SafetyLevel.Safe,
				PhotoSearchExtras.DateTaken | PhotoSearchExtras.Description | PhotoSearchExtras.Tags);
			var photos = Mapper.Map<IList<Models.Photo>>(flickrPhotos);
			return photos.OrderByDescending(x => x.DateTaken).ToList();
		}
	}
}