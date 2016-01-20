using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor.Model;
using Monitor.Service.ServiceModel;
using TimeStep = Monitor.Model.TimeStep;

namespace Monitor.Service.Utility
{
     public static class ModelConventer
    {
         public static ServiceRate ToServiceRate(this ServiceRateModel model)
         {
             return new ServiceRate(model.ServiceName,(TimeStep)model.RateTimeStep,model.TimeStamp,model.Rate);
         }

         public static ServiceRateModel ToServiceRateModel(this ServiceRate rate)
         {
             return new ServiceRateModel(rate.ServiceName, (ServiceModel.TimeStep) rate.RateTimeStep,rate.TimeStamp,rate.Rate);
         }
    }
}
