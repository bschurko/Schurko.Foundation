// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.EventExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace PNI.Extensions
{
  public static class EventExtensions
  {
    public static bool Raise(this EventHandler @event, object sender, EventArgs args)
    {
      if (@event == null)
        return false;
      @event(sender, args);
      return true;
    }

    public static bool Raise<T>(this EventHandler<T> @event, object sender, T args) where T : EventArgs
    {
      if (@event == null)
        return false;
      @event(sender, args);
      return true;
    }
  }
}
