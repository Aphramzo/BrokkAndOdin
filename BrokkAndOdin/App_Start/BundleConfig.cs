using System.Web;
using System.Web.Optimization;

namespace BrokkAndOdin
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/gallery").Include(
					  "~/Scripts/gallery.js",
					  "~/Scripts/jquery.easing.1.3.js",
					  "~/Scripts/jquery.elastislide.js",
					  "~/Scripts/jquery.tmpl.min.js",
					  "~/Scripts/moment.js",
					  "~/Scripts/dateRangePicker.js",
					  "~/Scripts/home.js",
					  "~/Scripts/imageOptions.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/Stylesheets/bootstrap.css",
					  "~/Content/Stylesheets/site.css"));

			bundles.Add(new StyleBundle("~/Content/gallery").Include(
					  "~/Content/Stylesheets/gallery.css",
					  "~/Content/dateRangePicker.css"));
		}
	}
}
