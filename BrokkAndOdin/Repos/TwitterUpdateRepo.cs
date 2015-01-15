using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetSharp;
using AutoMapper;

namespace BrokkAndOdin.Repos
{
	public class TwitterUpdateRepo : IUpdateRepo
	{
		private TwitterService _twitter {get;set;}
		private TwitterService _Twitter
		{
			get
			{
				if (_twitter == null)
				{
					var twitter = new TwitterService(AppConfig.TwitterKey, AppConfig.TwitterSecret);
					twitter.AuthenticateWith(AppConfig.TwitterToken, AppConfig.TwitterTokenSecret);
					_twitter = twitter;
				}
				return _twitter;
			}
		}

		public IList<Models.Update> GetLatestUpdates()
		{
			var tweets = _Twitter.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions
			{
				ScreenName = AppConfig.TwitterScreenName
			});
			
			//lazy way to adjust timezone
			foreach (var tweet in tweets)
			{
				tweet.CreatedDate = tweet.CreatedDate.AddHours(-7);
			}

			return Mapper.Map<IList<Models.Update>>(tweets);
		}
	}
}