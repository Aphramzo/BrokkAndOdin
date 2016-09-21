using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BrokkAndOdin.Repos;
using BrokkAndOdin.Repos.interfaces;

namespace BrokkAndOdin
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
			//TODO: config base this shit - no good if its in code like this
			container.RegisterType<IPictureRepo, FlickrPictureRepo>();
			container.RegisterType<IUpdateRepo, TwitterUpdateRepo>();
            container.RegisterType<ICacheRepo, RuntimeCacheRepo>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}