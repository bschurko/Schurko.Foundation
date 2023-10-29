// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.EnumeratorWrapper`1
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;


#nullable enable
namespace PNI.Extensions
{
  public class EnumeratorWrapper<T> : IEnumerator<T>, IEnumerator, IDisposable
  {
    private readonly IEnumerator _enumerator;

    public EnumeratorWrapper(IEnumerator enumerator) => this._enumerator = enumerator;

    public T Current => (T) this._enumerator.Current;

    public void Dispose()
    {
      if (!(this._enumerator is IDisposable))
        return;
      ((IDisposable) this._enumerator).Dispose();
    }

    object IEnumerator.Current => this._enumerator.Current;

    public bool MoveNext() => this._enumerator.MoveNext();

    public void Reset() => this._enumerator.Reset();
  }
}
