using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InterestRateCalculation.Startup))]
namespace InterestRateCalculation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
