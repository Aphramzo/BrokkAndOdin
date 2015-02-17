using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrokkAndOdin.Models;
using AutoMapper;
using StackExchange.Profiling;

namespace BrokkAndOdin.Repos
{
	public class FlickrPictureRepo : IPictureRepo
	{
		public int PicturesPerPage = 50;
		private static string FlickrVideoUrl = "https://www.flickr.com/photos/{0}/{1}/play/site/{2}/";
		private Flickr _Flickr
		{
			get
			{
				return new Flickr(AppConfig.FlickrKey, AppConfig.FlickrSecert);
			}
		}

		public IList<Models.Photo> GetLatestPhotos()
		{
			using (MiniProfiler.Current.Step("Getting Latest Flickr Photos"))
			{
				return GetLatestPhotos(1);
			}
		}

		public IList<Models.Photo> GetLatestPhotos(int pageNumber)
		{
			PhotoCollection flickrPhotos;
			IList<Models.Photo> photos;
			using (MiniProfiler.Current.Step("Getting Latest Flickr Photos For Page"))
			{
				flickrPhotos = _Flickr.PeopleGetPublicPhotos(
				AppConfig.FlickrUser, 
				pageNumber,
				PicturesPerPage,
				SafetyLevel.Safe,
				PhotoSearchExtras.DateTaken | PhotoSearchExtras.Description | PhotoSearchExtras.Tags);
			}

			using (MiniProfiler.Current.Step("Mapping Latest Flickr Photos to Photos"))
			{
				photos = Mapper.Map<IList<Models.Photo>>(flickrPhotos);
			}
			
			PopulateVideoUrls(photos);

			return photos.OrderByDescending(x => x.DateTaken).ToList();
		}

		public IList<Models.Photo> SearchPhotos(string searchString, DateTime? startDate, DateTime? endDate)
		{
			PhotoCollection flickrPhotos;
			IList<Models.Photo> photos;
			using (MiniProfiler.Current.Step("Searching Flicker Photos"))
			{
				flickrPhotos = _Flickr.PhotosSearch(new PhotoSearchOptions
				{
					UserId = AppConfig.FlickrUser,
					Text = searchString,
					Extras = PhotoSearchExtras.DateTaken | PhotoSearchExtras.Description | PhotoSearchExtras.Tags,
					MaxTakenDate = endDate.HasValue ? endDate.Value : DateTime.Now,
					MinTakenDate = startDate.HasValue ? startDate.Value : AppConfig.Birthdate
				});
			}
			using (MiniProfiler.Current.Step("Mapping Latest Flickr Photos to Photos"))
			{
				photos = Mapper.Map<IList<Models.Photo>>(flickrPhotos).OrderByDescending(x => x.DateTaken).ToList();
			}

			PopulateVideoUrls(photos);
			return photos;
		}

		public IList<Models.Photo> GetPhotoById(string photo)
		{
			using (MiniProfiler.Current.Step("Getting Photo By Id"))
			{
				var flickrPhoto = _Flickr.PhotosGetInfo(photo);
				var photos = new List<Models.Photo>();
				photos.Add(Mapper.Map<Models.Photo>(flickrPhoto));
				PopulateVideoUrls(photos);
				return photos;
			}
		}

		private void PopulateVideoUrls(IList<Models.Photo> photos)
		{
			using (MiniProfiler.Current.Step("PopulateVideoUrls"))
			{
				photos.Where(c => c.Tags.Contains("video")).ToList().ForEach(x => PopulateVideoUrl(x));
			}
		}

		private void PopulateVideoUrl(Models.Photo photo)
		{
			using (MiniProfiler.Current.Step("Populate Video URL"))
			{
				photo.VideoUrl =  String.Format(FlickrVideoUrl, AppConfig.FlickrUser, photo.Id, photo.PhotoSecret);
			}
		}
	}
}