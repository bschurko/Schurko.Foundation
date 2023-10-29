// Decompiled with JetBrains decompiler
// Type: PNI.Concurrent.WorkerPool.Worker`1
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using Microsoft.Extensions.Logging;
using PNI.Concurrent.WorkerPool.Models;

using System;
using System.Threading;
using System.Threading.Tasks;


#nullable enable
namespace PNI.Concurrent.WorkerPool
{
    internal class Worker<T> : IWorker<T> where T : IJob
    {
        private readonly Administrator<T> _administrator;

        private CancellationTokenSource _cancellationTokenSource;

        private Task _backgroundTask;
        private bool _backgroundTaskInitialized;
        private object _backgroundTaskSyncLock;

        private ILoggerFactory loggerFactory = (ILoggerFactory)new LoggerFactory();
        private Microsoft.Extensions.Logging.ILogger? _logger;

        private Microsoft.Extensions.Logging.ILogger Logger => this._logger ?? (this._logger = this.loggerFactory.CreateLogger("AdministratorPool"));

        public Worker(Administrator<T> administrator)
        {
            if (administrator == null) throw new ArgumentNullException("administrator");

            _administrator = administrator;
            Id = Guid.NewGuid().ToString("N");
        }

        public string Id { get; private set; }

        /// <summary>
        /// Start worker.  Worker will create background thread and wait for the job.
        /// </summary>
        public void Start()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Dispose();

            _cancellationTokenSource = new CancellationTokenSource();
            LazyInitializer.EnsureInitialized(
                ref _backgroundTask,
                ref _backgroundTaskInitialized,
                ref _backgroundTaskSyncLock,
                () => DoWork(_cancellationTokenSource.Token));
        }

        /// <summary>
        /// Return current thread
        /// </summary>
        /// <returns></returns>
        public Task Thread()
        {
            return _backgroundTask;
        }

        /// <summary>
        /// Stop the worker.  Current job will get cancelled
        /// </summary>
        public void Stop()
        {
            _cancellationTokenSource.Cancel();

            _backgroundTask.Dispose();
            _backgroundTask = null;
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        private Task DoWork(CancellationToken token)
        {
            return Task.Factory.StartNew(async () =>
            {
                var log = Logger;
                try
                {
                    while (true)
                    {
                        token.ThrowIfCancellationRequested();

                        //administrator give me the job.
                        log.LogInformation("Waiting for the new job... blocking.....");
                        var job = _administrator.GetNextJob(this);
                        token.ThrowIfCancellationRequested();

                        try
                        {
                            log.LogInformation(string.Format("Working on job ID: '{0}'...", job.Id));

                            await _administrator.JobProcessorAsync(job);

                            log.LogInformation(string.Format("job ID: '{0}' completed...", job.Id));
                        }
                        catch (Exception ex)
                        {
                            job.Exception = ex;
                            log.LogError("Processing job caused exception.", ex);
                        }
                        finally
                        {
                            _administrator.AckJobComplete(job);
                            log.LogInformation(string.Format("Notify job ID: '{0}' is completed...", job.Id));
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    log.LogInformation(string.Format("Worker '{0}' terminated.", Id));
                    throw;
                }

            }, TaskCreationOptions.LongRunning);
        }
    }
}
