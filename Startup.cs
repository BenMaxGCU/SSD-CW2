using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cw2_ssd.Startup))]
namespace cw2_ssd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
