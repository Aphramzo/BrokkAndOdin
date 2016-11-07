using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;
using Itenso.TimePeriod;

namespace BrokkAndOdin.Models
{
	public class Photo : ITimeLineItem, IMapableItem
	{
		public string Id { get; set; }
		public string PhotoSecret { get; set; }
		public DateTime? DateTaken { get; set; }
		public string ThumbnailUrl { get; set; }
		public string FullUrl { get; set; }
        public string SmallUrl { get; set; }
		public string Description { get; set;}
		public IList<String> Tags { get; set; }
		public GeoCoordinate Coords { get; set; }

		//Question time - is a video really just a kind of picture? 
		//Basically a thumbnail that has a related video?
		public string VideoUrl { get; set; }
		public bool HasVideo
		{
			get
			{
				return !String.IsNullOrEmpty(VideoUrl);
			}
		}

		public string Title
		{
			get
			{
                if (!String.IsNullOrEmpty(Description) && AppConfig.ShowAge)
                {
                    return String.Format("{0} ({2}) - {1}", DateTaken.Value.ToShortDateString(), Description, AgeString);
                }
                else if(!String.IsNullOrEmpty(Description))
                {
                    return String.Format("{0} - {1}", DateTaken.Value.ToShortDateString(), Description, AgeString);
                }
                
                return DateTaken.Value.ToShortDateString();
                	
			}
		}

		public DateDiff Age
		{
			get
			{
				return new DateDiff(AppConfig.Birthdate, DateTaken.Value);
			}
		}

		public int AgeInDays
		{
			get
			{
				return Age.Days;
			}
		}

		public int AgeInWeeks
		{
			get
			{
				//it would seem that weeks goes off start of week
				//So even though the boys DOB was Friday, it is calculating off of that Monday.
				//TODO: Make config based or better way than hardcoding the adddays junk
				return new DateDiff(AppConfig.Birthdate, DateTaken.Value.AddDays(-5)).Weeks;
			}
		}

		public int AgeInMonths
		{
			get
			{
				return Age.Months;
			}
		}

	    public int AgeInYears
	    {
	        get { return Age.Years; }
	    }

		public string AgeString
		{
			get
			{
				if (AgeInDays < 21)
					return String.Format("{0} days old", AgeInDays);
				else if (AgeInWeeks < 10)
					return String.Format("{0} weeks old", AgeInWeeks);
				else if (AgeInMonths < 24)
				    return String.Format("{0} months old", AgeInMonths);
				else
				    return String.Format("{0} years old", AgeInYears);
			}
		}

		public DateTime ItemDateTime
		{
			get
			{
				return DateTaken.Value;
			}
		}
	}
}