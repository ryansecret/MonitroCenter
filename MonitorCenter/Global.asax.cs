using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using Monitor.Model;
using Monitor.Service;
using Monitor.Service.ServiceModel;
using Monitor.Service.Utility;
 
using Newtonsoft.Json;
using TimeStep = Monitor.Service.ServiceModel.TimeStep;

namespace MonitorCenter
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Task.Factory.StartNew(() =>
            //{
            //    //清楚失效数据，比如只保留1天数据
            //    var service = ServiceLocator.Current.GetInstance<RateService>();
            //     service.DropData();
            //    //设置timer定时更新
                
            //    var serviceConfig = MockHelper.GetServiceHealths();
            //    foreach (var cf in serviceConfig)
            //    {
                 
            //        var timer = new Timer(1000*(int)cf.RateTimeStep);
            //        timer.Elapsed += (dd, e) =>
            //        {
            //            var rate = new WebClient().DownloadData(cf.RateUrl);
            //            dynamic s = new { ServiceName = "sdf", RateTimeStep = TimeStep.Hour, TimeStamp = DateTime.Now, Rate = 0 }; 
            //            var d = JsonConvert.DeserializeAnonymousType(Encoding.UTF8.GetString(rate),s);
                        
            //            if (d != null)
            //            {
            //                service.Add(new ServiceRateModel(d.ServiceName,d.RateTimeStep, d.TimeStamp, d.Rate));
            //            }
            //        };
            //        timer.Start();
            //    }
            //});
        }
    }
}