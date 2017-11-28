using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MenuApp.Startup))]
namespace MenuApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
