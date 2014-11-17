using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BrokkAndOdin
{
	public static class AppConfig
	{
		public static string FlickrKey { get { return ConfigurationManager.AppSettings["FlickrKey"]; } }
		public static string FlickrSecert { get { return ConfigurationManager.AppSettings["FlickrSecert"]; } }
		public static string FlickrUser { get { return ConfigurationManager.AppSettings["FlickrUser"]; } }

		public static DateTime Birthdate { get {return Convert.ToDateTime(ConfigurationManager.AppSettings["Birthdate"]);}}
	}
}