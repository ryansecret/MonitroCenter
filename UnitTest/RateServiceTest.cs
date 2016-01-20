using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monitor.Service;
using Monitor.Service.ServiceModel;

namespace UnitTest
{
    [TestClass]
    public class RateServiceTest
    {
        [TestMethod]
        public void GetRateDataTest()
        {
             RateService rateService=new RateService();
             var data= rateService.GetRateData(TimeStep.Minute);
            Assert.IsTrue(data.Any());
        }
    }
}
