using System.Web.Http;

namespace Wsa.Gaas.GobbletGobblers.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ////Clear current formatters
            //config.Formatters.Clear();

            ////Add only a json formatter
            //config.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
}
