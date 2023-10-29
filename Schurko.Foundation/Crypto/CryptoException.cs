// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Crypto.CryptoException
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace Schurko.Foundation.Crypto
{
  public class CryptoException : Exception
  {
    public CryptoException(string message)
      : base(message)
    {
    }
  }
}
