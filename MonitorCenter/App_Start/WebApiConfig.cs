using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using Monitor.Model;
using Monitor.Service;
using Monitor.Service.ServiceModel;
using Monitor.Service.Utility;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;
using TimeStep = Monitor.Service.ServiceModel.TimeStep;

namespace MonitorCenter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            ServiceLocator.SetLocatorProvider(()=>new MonitorServiceLoacator());
            
            
        }
    }
}
