using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WebApp
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            // @see https://docs.microsoft.com/en-us/previous-versions/cc668201(v=vs.140)
            routes.MapPageRoute(
                "",
                "userCrud/",//{action}/{categoryName}
                "~/App_Start/App_Pages/crudWebForm.aspx"
            );
        }
    }
}
