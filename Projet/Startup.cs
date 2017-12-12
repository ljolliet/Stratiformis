using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projet.Startup))]
namespace Projet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
