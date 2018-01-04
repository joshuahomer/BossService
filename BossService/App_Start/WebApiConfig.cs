using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using BossService.Controllers;

namespace BossService
{
    public static class WebApiConfig
    {
        static TwitterController twitter;
        //https://msdn.microsoft.com/en-us/library/system.threading.timer(v=vs.110).aspx
        //https://social.msdn.microsoft.com/Forums/en-US/48f77cfb-b8f7-46a5-9de9-6f681b3a6373/calling-method-every-hour?forum=csharpgeneral
        private static void PrintMessage(Object state)
        {
            twitter.Get(245687543);
        }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            twitter = new TwitterController();
            //automatically tweet 3 times a day - 28800000
            Timer timer = new Timer(PrintMessage, null, 0, 3600000);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
