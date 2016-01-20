using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using Monitor.Service;
using Monitor.Service.ServiceModel;
using Monitor.Service.Utility;

namespace MonitorCenter.Controllers
{
    public class MonitorController : ApiController
    {
        public IHttpActionResult GetServiceHealth()
        {
            var num = new Random().Next(100, 500);
            Thread.Sleep(num);
            return Json("ryan test  1231233");
        }

        public async Task<IHttpActionResult> GetHealthInfo(string url, int? seconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var request = WebRequest.Create(url);

            var resp = await request.GetResponseAsync() as HttpWebResponse;
            stopwatch.Stop();
            var normal = !(seconds.HasValue && stopwatch.ElapsedMilliseconds > seconds.Value);
            var response = Request.CreateResponse(HttpStatusCode.OK, (double) stopwatch.ElapsedMilliseconds/1000);
            if (!normal || resp.StatusCode != HttpStatusCode.OK)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return ResponseMessage(response);
        }

        [HttpGet]
        public IHttpActionResult HealthStateMock()
        {
            return Json(MockHelper.GetServiceHealths());
        }

        [HttpGet]
        public IHttpActionResult InitialChart(string serviceName,TimeStep step = TimeStep.Second)
        {
            var data = ServiceLocator.Current.GetInstance<RateService>().GetRateData(step, serviceName);
            return Json(data);
        }

        [HttpGet]
        public IHttpActionResult GetServiceRate(string serviceName,TimeStep step = TimeStep.Second)
        {
            var data = ServiceLocator.Current.GetInstance<RateService>().GetRecentData(step,serviceName);
            return Json(data);
        }
    }
}