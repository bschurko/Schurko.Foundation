// Decompiled with JetBrains decompiler
// Type: PNI.Concurrent.WorkerPool.IWorker`1
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using Schurko.Foundation.Concurrent.WorkerPool.Models;
using System.Threading.Tasks;


#nullable enable
namespace Schurko.Foundation.Concurrent.WorkerPool
{
    public interface IWorker<T> where T : IJob
    {
        string Id { get; }

        void Start();

        void Stop();

        Task Thread();
    }
}
