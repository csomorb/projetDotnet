using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestProjet.Startup))]
namespace TestProjet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
