// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.IfStatement`1
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll


#nullable enable
namespace PNI.Extensions
{
  public class IfStatement<TSource>
  {
    public bool State { get; private set; }

    public TSource Context { get; private set; }

    internal IfStatement(bool state, TSource context)
    {
      this.State = state;
      this.Context = context;
    }

    public static implicit operator bool(IfStatement<TSource> obj) => obj.State;
  }
}
