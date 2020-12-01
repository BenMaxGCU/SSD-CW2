using cw2_ssd.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cw2_ssd.Startup))]
namespace cw2_ssd
{
    public partial class Startup
    {
        private TicketDbContext db = new TicketDbContext();
        
        public void Configuration(IAppBuilder app)
        {
            db.Database.Initialize(true);
            ConfigureAuth(app);
        }
    }
}
