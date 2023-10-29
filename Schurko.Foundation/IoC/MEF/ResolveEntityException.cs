// Decompiled with JetBrains decompiler
// Type: PNI.Ioc.MEF.ResolveEntityException
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.ComponentModel.Composition.Primitives;


#nullable enable
namespace PNI.Ioc.MEF
{
  public class ResolveEntityException : Exception
  {
    public string ContractName { get; private set; }

    public ComposablePartCatalog Catalog { get; private set; }

    internal ResolveEntityException(
      string failedContract,
      ComposablePartCatalog catalog,
      string message,
      Exception innerException)
      : base(message, innerException)
    {
      this.ContractName = failedContract;
      this.Catalog = catalog;
    }
  }
}
