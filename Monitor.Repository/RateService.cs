using System;
using System.Collections.Generic;
using System.Linq;
using Monitor.Model;
using Monitor.Model.Repository;
using Monitor.Service.ServiceModel;
using Monitor.Service.Utility;
using TimeStep = Monitor.Service.ServiceModel.TimeStep;

namespace Monitor.Service
{
    public class RateService
    {
        readonly IRepository<ServiceRate> _rateRepository;
        public RateService()
        {
             _rateRepository=new MongoRepository<ServiceRate>();
        }

        public void Add(IEnumerable<ServiceRateModel> serviceRate)
        {
            _rateRepository.Add(serviceRate.Select(d=>d.ToServiceRate()));
        }

        public void Add(ServiceRateModel serviceRate)
        {
            _rateRepository.Add(serviceRate.ToServiceRate());
        }

        /// <summary>
        /// 删除昨天之前的数据
        /// </summary>
        public void DropData()
        {
            _rateRepository.Delete(d=>d.TimeStamp<DateTime.Today.AddDays(-1));
        }

        public IEnumerable<ServiceRateModel> GetRateData(TimeStep timeStep, string serviceName = "ryan")
        {
            var data = _rateRepository.Where(d => d.ServiceName == serviceName);
            IList<ServiceRate> list = new List<ServiceRate>();
            
            switch (timeStep)
            {
                 case  TimeStep.Second:
                    list = data.Where(d => d.TimeStamp > DateTime.Now.AddSeconds(-10)).ToList();
                    break;
                 case  TimeStep.Minute:
                    var endTime = DateTime.UtcNow.ToDateTime();
                    var ag = data.Where(d => d.TimeStamp > DateTime.Now.AddMinutes(-10)&&d.TimeStamp < endTime).ToList();
                   
                    list =ag.GroupBy(
                            d =>
                                d.TimeStamp.ToDateTime()).ToList()
                        .Select(
                            d =>
                            {
                                var m = d.FirstOrDefault();
                                return new ServiceRate(m.ServiceName, m.RateTimeStep, d.Key, d.Sum(i => i.Rate));
                            }).ToList();
                    break;

            }
            return list.Select(d=>d.ToServiceRateModel());
        }

        public ServiceRateModel GetRecentData(TimeStep timeStep, string serviceName = "ryan")
        {
            var data = _rateRepository.Where(d => d.ServiceName == serviceName);
            switch (timeStep)
            {
                case TimeStep.Second:
                    return data.Last(d => d.TimeStamp < DateTime.Now).ToServiceRateModel();
                case TimeStep.Minute:
                    var endTime = DateTime.UtcNow.ToDateTime();
                    return
                        data.Where(d => d.TimeStamp >= endTime.AddMinutes(-1) && d.TimeStamp < endTime)
                            .ToList()
                            .GroupBy(d => d.TimeStamp.ToDateTime())
                            .Select(
                                d =>
                                {
                                    var m = d.FirstOrDefault();
                                    return new ServiceRate(m.ServiceName, m.RateTimeStep, d.Key, d.Sum(i => i.Rate));
                                }).FirstOrDefault().ToServiceRateModel();
            }
            return null;
        }

        public IEnumerable<ServiceRateModel> GetRateTest()
        {
            var data=_rateRepository.Where(d => d.TimeStamp > DateTime.Now.AddSeconds(-10)).OrderBy(d=>d.TimeStamp).Select(d => d.ToServiceRateModel()).ToList();
            return data;

        }
    }
}
