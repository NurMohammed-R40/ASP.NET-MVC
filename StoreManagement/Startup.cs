using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreManagement.Startup))]
namespace StoreManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
