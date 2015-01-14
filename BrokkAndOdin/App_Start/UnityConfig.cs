using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BrokkAndOdin.Repos;

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
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}