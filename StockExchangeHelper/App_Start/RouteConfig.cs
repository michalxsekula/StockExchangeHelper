using System.Web.Mvc;
using System.Web.Routing;

namespace StockExchangeHelper
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "ExchangeRate", action = "GetRate", id = UrlParameter.Optional}
            );
        }
    }
}