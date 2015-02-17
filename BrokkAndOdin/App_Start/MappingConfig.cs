using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace BrokkAndOdin
{
	public static class MappingConfig
	{
		public static void RegisterMappings()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<FlickrNet.Photo, Models.Photo>()
					.ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.SquareThumbnailUrl))
					.ForMember(dest => dest.FullUrl, opt => opt.MapFrom(src => src.LargeUrl))
					.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PhotoId))
					.ForMember(dest => dest.PhotoSecret, opt => opt.MapFrom(src => src.Secret));

				cfg.CreateMap<FlickrNet.PhotoInfo, Models.Photo>()
					.ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.SquareThumbnailUrl))
					.ForMember(dest => dest.FullUrl, opt => opt.MapFrom(src => src.LargeUrl))
					.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PhotoId));

				cfg.CreateMap<TweetSharp.TwitterStatus, Models.Update>()
					.ForMember(dest => dest.UpdateDateTime, opt => opt.MapFrom(src => src.CreatedDate))
					.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TextAsHtml));
			});
		}
	}
}