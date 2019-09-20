using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KataWeb.Startup))]
namespace KataWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
