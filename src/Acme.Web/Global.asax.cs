namespace Acme.Web
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Controllers.Pages;
    using EPiServer.ServiceLocation;

    public class Global : EPiServer.Global
    {
        /// <summary>
        ///     Get key to vary cached items by. Overrides EPi's way. Needs values in all settings elements, for example:
        ///     httpCacheability="Public"
        ///     httpCacheExpiration="0:10:0"
        ///     httpCacheVaryByCustom="custom"
        ///     httpCacheVaryByParams="needs-a-value-but-not-used"
        /// </summary>
        /// <param name="context">The current request's context.</param>
        /// <param name="custom">The string from configuration setting httpCacheVaryByCustom.</param>
        /// <returns>The raw URL of the page requested.</returns>
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom.Equals("custom", StringComparison.OrdinalIgnoreCase))
            {
                return context.Request.RawUrl;
            }

            return base.GetVaryByCustomString(context, custom);
        }

        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Add(new ViewEngine());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((Global)sender).Context;

            if (httpContext.IsCustomErrorEnabled == false)
            {
                return;
            }

            IController controller = null;

            var ex = this.Server.GetLastError();
            var httpException = ex as HttpException;
            var statusCode = 0;
            var controllerName = string.Empty;
            var actionName = string.Empty;

            if (httpException != null)
            {
                statusCode = httpException.GetHttpCode();

                switch (statusCode)
                {
                    case 403:
                        controller = ServiceLocator.Current.GetInstance<NoAccessPageController>();
                        controllerName = "NoAccessPage";
                        actionName = nameof(NoAccessPageController.GetModelFromSettings);
                        break;
                    case 404:
                        controller = ServiceLocator.Current.GetInstance<NotFoundPageController>();
                        controllerName = "NotFoundPage";
                        actionName = nameof(NotFoundPageController.GetModelFromSettings);
                        break;
                }
            }

            // Let customErrors in web.config handle these errors
            if (statusCode == 0 || controller == null || string.IsNullOrEmpty(controllerName))
            {
                return;
            }

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.TrySkipIisCustomErrors = true;

            var routeData = new RouteData();
            routeData.Values["controller"] = controllerName;
            routeData.Values["action"] = actionName;

            ((ControllerBase)controller).ViewData.Model = GetErrorInfo(httpContext, ex);

            controller.Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            RouteConfig.RegisterRoutes(routes);
        }

        private static HandleErrorInfo GetErrorInfo(HttpContext context, Exception ex)
        {
            var currentController = string.Empty;
            var currentAction = string.Empty;
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(context));

            if (currentRouteData != null)
            {
                if (string.IsNullOrEmpty(currentRouteData.Values["controller"]?.ToString()) == false)
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (string.IsNullOrEmpty(currentRouteData.Values["action"]?.ToString()) == false)
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            if (string.IsNullOrEmpty(currentController) || string.IsNullOrEmpty(currentAction))
            {
                return null;
            }

            return new HandleErrorInfo(ex, currentController, currentAction);
        }
    }
}
