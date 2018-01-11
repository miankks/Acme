namespace Acme.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Acme.Web.Controllers.Pages;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "RobotsTxtRoute",
                "robots.txt",
                new
                {
                    controller = "RobotsTxt",
                    action = "Index"
                });

            routes.MapRoute(
                "404",
                "notfound/{action}/{id}",
                new
                {
                    controller = "NotFoundPage",
                    action = nameof(NotFoundPageController.GetModelFromSettings),
                    id = string.Empty
                });

            routes.MapRoute(
                "403",
                "noaccess/{action}/{id}",
                new
                {
                    controller = "NoAccessPage",
                    action = nameof(NoAccessPageController.GetModelFromSettings),
                    id = string.Empty
                });
        }
    }
}