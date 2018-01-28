using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockExchangeHelper.Startup))]
namespace StockExchangeHelper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
