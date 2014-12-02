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
		public DateTime? DateTaken { get; set; }
		public string ThumbnailUrl { get; set; }
		public string FullUrl { get; set; }
		public string Description { get; set;}
		public IList<String> Tags { get; set; }
		public GeoCoordinate Coords { get; set; }

		public string Title
		{
			get
			{
				if (!String.IsNullOrEmpty(Description))
					return String.Format("{0} ({2}) - {1}", DateTaken.Value.ToShortDateString(), Description, AgeString);
				else
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

		public string AgeString
		{
			get
			{
				if (AgeInDays < 21)
					return String.Format("{0} days old", AgeInDays);
				else if (AgeInWeeks < 10)
					return String.Format("{0} weeks old", AgeInWeeks);
				else
					return String.Format("{0} months old", AgeInMonths);
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