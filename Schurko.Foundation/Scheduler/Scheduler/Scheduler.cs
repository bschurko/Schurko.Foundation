using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VivaVictoria.Retask.Interfaces;

namespace VivaVictoria.Retask.Scheduler
{
    public class Scheduler : BackgroundService
    {
        private class JobEntry
        {
            public IJob Job { get; set; }
            public DateTime NextRun { get; set; }
        }

        private readonly IList<JobEntry> entries;
        private readonly IScheduleSettings scheduleSettings;

        public Scheduler(IEnumerable<IJob> jobs, IScheduleSettings scheduleSettings)
        {
            entries = jobs.Select(j => new JobEntry { 
                Job = j,
                NextRun = default 
            }).ToList();
            this.scheduleSettings = scheduleSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;

                try
                {
                    var executableEntries = entries.Where(j => j.NextRun.Subtract(now).Ticks < scheduleSettings.MaxDifference.Ticks);
                    foreach (var entry in executableEntries)
                    {
                        var job = entry.Job;
                        _ = job.Do();

                        entry.NextRun = job.NextRun(now);
                    }
                }
                catch (Exception ex)
                {
                    if (!scheduleSettings.HideExceptions)
                    {
                        throw ex;
                    }
                }

                if (!entries.Any())
                {
                    break;
                }

                var timeout = entries.Min(j => j.NextRun).Subtract(now);
                if (timeout.Ticks > scheduleSettings.MaxTimeout.Ticks)
                {
                    await Task.Delay(timeout, stoppingToken);
                }
            }
        }
    }
}
