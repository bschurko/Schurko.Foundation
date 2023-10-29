using PNI.Foundation.Concurrent.WorkerPool.DemoWorkerPool;
using PNI.Ioc.MEF;
using PNI.Tests.Ioc.MEF.Entities.Implementations;
using PNI.Tests.Ioc.MEF.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Tests.DemoWorkerPool
{
    [TestClass]
    public class AdminPoolTests
    {
        [TestMethod]
        public void AdministratorDemo_Test()
        {
            bool hasStarted = false;
            while(true)
            {
                if (!hasStarted)
                {
                    hasStarted = true;
                    var admin = new DemoAdministrator();
                    AddSumJob job = new AddSumJob()
                    {
                        Input = "Input Data",
                        Number = new Random().Next(1, 10),
                        Exception = null
                    };

                    admin.SubmitJob(job);
                }

                Task.Delay(3000);
            }
            
            
        }
    }
}
