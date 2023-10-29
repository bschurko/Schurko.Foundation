using System;
using Schurko.Foundation.Concurrent.WorkerPool.Models;

namespace Schurko.Foundation.Tests.DemoWorkerPool
{
    public class AddSumJob : IJob
    {
        public AddSumJob()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public string Input { get; set; }
        public int Number { get; set; }
        public string Id { get; private set; }
        public Exception Exception { get; set; }
    }
}