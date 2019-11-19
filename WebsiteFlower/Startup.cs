using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteFlower.Startup))]
namespace WebsiteFlower
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
