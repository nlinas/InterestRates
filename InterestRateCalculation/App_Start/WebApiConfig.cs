using System.Web.Http;

class WebApiConfig
{
    public static void Register(HttpConfiguration configuration)
    {
        configuration.Routes.MapHttpRoute(
            name: "API Default",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { controller = "InterestRate", action = "Get", id = RouteParameter.Optional }
            );
    }
}