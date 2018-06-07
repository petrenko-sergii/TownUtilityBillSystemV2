using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TownUtilityBillSystemV2.Startup))]
namespace TownUtilityBillSystemV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
