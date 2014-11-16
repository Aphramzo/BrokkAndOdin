using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrokkAndOdin.Startup))]
namespace BrokkAndOdin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
