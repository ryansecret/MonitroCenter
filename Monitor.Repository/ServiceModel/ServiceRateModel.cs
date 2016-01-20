using System;

namespace Monitor.Service.ServiceModel
{
    public class ServiceRateModel
    {
        public ServiceRateModel(string serviceName, TimeStep timeStep, DateTime stamp, int rate)
        {
            this.ServiceName = serviceName;
            this.RateTimeStep = timeStep;
            this.TimeStamp = stamp;
            this.Rate = rate;
        }
        public string ServiceName { get;private set; }

        public TimeStep RateTimeStep { get;private set; }
        public DateTime TimeStamp { get;private set; }
        public int Rate { get;private set; } 
    }

    public enum TimeStep
    {
        Hour=3600, Minute=60, Second=1
    }
}
