﻿using Schurko.Foundation.Concurrent.WorkerPool.Models;
using System;

namespace Schurko.Foundation.Scheduler.Interfaces
{
    public class JobEntry : IJob
    {
        public JobEntry()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public JobEntry(string input, int number)
        {
            Id = Guid.NewGuid().ToString("N");
            Input = input;
            Number = number;
        }

        public JobEntry(string input, int number, Action JobAction)
        {
            Id = Guid.NewGuid().ToString("N");
            Input = input;
            Number = number;
            this.JobAction = JobAction;
        }

        public JobEntry(Action JobAction)
        {
            Id = Guid.NewGuid().ToString("N");
            this.JobAction = JobAction;
        }

        public string? Input { get; set; }
        public int Number { get; set; }
        public string Id { get; private set; }
        public Exception? Exception { get; set; }
        public Action? JobAction { get; set; }
     
    }
}