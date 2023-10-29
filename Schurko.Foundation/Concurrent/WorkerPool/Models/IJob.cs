// Decompiled with JetBrains decompiler
// Type: PNI.Concurrent.WorkerPool.Models.IJob
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace Schurko.Foundation.Concurrent.WorkerPool.Models
{
    public interface IJob
    {
        string Id { get; }

        Exception Exception { get; set; }
    }
}
