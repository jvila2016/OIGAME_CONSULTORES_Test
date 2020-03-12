using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OIGAME_CONSULTORES_Test.Startup))]
namespace OIGAME_CONSULTORES_Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
