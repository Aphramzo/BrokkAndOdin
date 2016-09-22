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

		public static string TwitterKey { get { return ConfigurationManager.AppSettings["TwitterKey"]; } }
		public static string TwitterSecret { get { return ConfigurationManager.AppSettings["TwitterSecret"]; } }
		public static string TwitterToken { get { return ConfigurationManager.AppSettings["TwitterToken"]; } }
		public static string TwitterTokenSecret { get { return ConfigurationManager.AppSettings["TwitterTokenSecret"]; } }
		public static string TwitterScreenName { get { return ConfigurationManager.AppSettings["TwitterScreenName"]; } }

		public static DateTime Birthdate { get {return Convert.ToDateTime(ConfigurationManager.AppSettings["Birthdate"]);}}

        public static string SiteName { get { return ConfigurationManager.AppSettings["SiteName"]; } }
        public static string SiteTitle { get { return ConfigurationManager.AppSettings["SiteTitle"]; } }

        public static bool ShowAge { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAge"]); } }
        public static string AppInsightsIKey { get { return ConfigurationManager.AppSettings["iKey"]; } }
        public static string Favicon { get { return ConfigurationManager.AppSettings["Favicon"]; } }
	}
}