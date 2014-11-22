using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;

namespace BrokkAndOdin.Models
{
	public class Update : ITimeLineItem, IMapableItem
	{
		public DateTime UpdateDateTime { get; set; }
		public string Description { get; set; }

		public DateTime ItemDateTime
		{
			get { return UpdateDateTime; }
		}

		//TODO: map this
		public GeoCoordinate Coords
		{
			get { return new GeoCoordinate(); }
		}
	}
}