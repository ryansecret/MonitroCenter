using System.Collections.Generic;
using Monitor.Service.ServiceModel;

namespace Monitor.Service.Utility
{
    public class MockHelper
    {
        public static IList<ServiceHealth> GetServiceHealths()
        {
            List<ServiceHealth> serviceHealths=new List<ServiceHealth>();
            ServiceHealth serviceHealth=new ServiceHealth();
            serviceHealth.ServiceName = "ryan";
            string url = "http://localhost:14709/api/Monitor/GetServiceHealth";
            serviceHealth.RateUrl = "http://localhost:14709/api/Monitor/GetServiceRate";
            serviceHealth.RateTimeStep=TimeStep.Second;
            
            serviceHealth.ServiceCheckUrls.Add(new HealthItem(){Url =url,TimeLimit = 5000});
            serviceHealth.ServiceCheckUrls.Add(new HealthItem(){Url =url,TimeLimit = 300});
            serviceHealths.Add(serviceHealth);
            serviceHealths.Add(serviceHealth);
            serviceHealths.Add(serviceHealth);
            return serviceHealths;
        }
    }
}