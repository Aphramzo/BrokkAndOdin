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
		private Flickr _flickr { get; set; }
		private Flickr _Flickr
		{
			get
			{
				if (_flickr == null)
				{
					var flickr = new Flickr(AppConfig.FlickrKey, AppConfig.FlickrSecert);
					_flickr = flickr;
				}
				return _flickr;
			}
		}

		public IList<Models.Photo> GetLatestPhotos()
		{
			return GetLatestPhotos(1);
		}

		public IList<Models.Photo> GetLatestPhotos(int pageNumber)
		{
			var flickrPhotos = _Flickr.PeopleGetPublicPhotos(
				AppConfig.FlickrUser, 
				pageNumber,
				PicturesPerPage,
				SafetyLevel.Safe,
				PhotoSearchExtras.DateTaken | PhotoSearchExtras.Description | PhotoSearchExtras.Tags);
			var photos = Mapper.Map<IList<Models.Photo>>(flickrPhotos);
			return photos.OrderByDescending(x => x.DateTaken).ToList();
		}

		public IList<Models.Photo> SearchPhotos(string searchString, DateTime? startDate, DateTime? endDate)
		{
			var flickrPhotos = _Flickr.PhotosSearch(new PhotoSearchOptions
			{
				UserId = AppConfig.FlickrUser,
				Text = searchString,
				Extras = PhotoSearchExtras.DateTaken | PhotoSearchExtras.Description | PhotoSearchExtras.Tags,
				MaxTakenDate = endDate.HasValue ? endDate.Value : DateTime.Now,
				MinTakenDate = startDate.HasValue ? startDate.Value : AppConfig.Birthdate
			});
			return Mapper.Map<IList<Models.Photo>>(flickrPhotos).OrderByDescending(x => x.DateTaken).ToList();
		}


		public IList<Models.Photo> GetPhotoById(string photo)
		{
			var flickrPhoto = _Flickr.PhotosGetInfo(photo);
			var photos = new List<Models.Photo>();
			photos.Add(Mapper.Map<Models.Photo>(flickrPhoto));
			return photos;
		}
	}
}