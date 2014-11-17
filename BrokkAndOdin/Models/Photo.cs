﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokkAndOdin.Models
{
	public class Photo
	{
		public DateTime? DateTaken { get; set; }
		public string ThumbnailUrl { get; set; }
		public string FullUrl { get; set; }
		public string Description { get; set;}
		public IList<String> Tags { get; set; }

		public string Title
		{
			get
			{
				if (!String.IsNullOrEmpty(Description))
					return String.Format("{0} ({2} days old) - {1}", DateTaken.Value.ToShortDateString(), Description, Age);
				else
					return DateTaken.Value.ToShortDateString();
			}
		}

		public int Age
		{
			get
			{
				return (DateTaken - AppConfig.Birthdate).Value.Days;
			}
		}
	}
}