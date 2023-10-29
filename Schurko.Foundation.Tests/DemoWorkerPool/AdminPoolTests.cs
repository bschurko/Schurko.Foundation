
using PNI.Foundation.Concurrent.WorkerPool.DemoWorkerPool;
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
            var admin = new DemoAdministrator();

            while (true)
            {
                AddSumJob job = new AddSumJob()
                {
                    Input = "Input Data",
                    Number = new Random().Next(1, 10),
                    Exception = null
                };

                admin.SubmitJob(job);
            }
             
        }
    }
}
