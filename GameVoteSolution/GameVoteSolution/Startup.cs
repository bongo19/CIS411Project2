using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameVoteSolution.Startup))]
namespace GameVoteSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
