using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCAppRepository.Startup))]
namespace MVCAppRepository
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
