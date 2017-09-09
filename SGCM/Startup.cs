using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SGCM.Startup))]
namespace SGCM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}