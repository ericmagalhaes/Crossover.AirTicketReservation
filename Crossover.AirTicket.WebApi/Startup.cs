using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Crossover.AirTicket.WebApi
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = new Container();
            // Configure Web API for self-host. 
            var config = new HttpConfiguration()
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    }
}
