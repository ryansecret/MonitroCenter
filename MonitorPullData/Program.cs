using System;
using System.Timers;
using Monitor.Service;
using Monitor.Service.ServiceModel;
using Monitor.Service.Utility;

namespace MonitorPullData
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //清楚失效数据，比如只保留1天数据
            var service = new RateService();
            service.DropData();
            //设置timer定时更新

            var serviceConfig = MockHelper.GetServiceHealths();
            foreach (var cf in serviceConfig)
            {
                var timer = new Timer(1000*(int) cf.RateTimeStep);
                timer.Elapsed += (dd, e) =>
                {
                    var num = new Random().Next(100, 1000);

                    var serviceRate = new ServiceRateModel("ryan", TimeStep.Minute, DateTime.Now, num);

                    service.Add(serviceRate);
                };
                timer.Start();
            }

            Console.Read();
        }
    }
}