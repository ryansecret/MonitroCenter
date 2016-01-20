using System.Collections.Generic;

namespace Monitor.Service.ServiceModel
{
    /// <summary>
    /// 检查服务状态
    /// </summary>
    public class ServiceHealth
    {
        public ServiceHealth()
        {
            ServiceCheckUrls=new List<HealthItem>();
        }
        public List<HealthItem> ServiceCheckUrls { get; set; }

        public TimeStep RateTimeStep { get; set; }

        public string RateUrl { get; set; }
        public string ServiceName { get; set; }
    }

    public class HealthItem
    {
        public string Url { get; set; }
         
        public int TimeLimit { get; set; }
    }
   
    
}