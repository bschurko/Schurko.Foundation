// Decompiled with JetBrains decompiler
// Type: PNI.Caching.CacheSettings
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using Microsoft.Extensions.Logging;
using PNI.Extensions;
using Schurko.Foundation.Logging;
using System;
using System.Configuration;


#nullable enable
namespace PNI.Caching
{
  public abstract class CacheSettings
  {
    public static T GetCacheSetting<T>(string cacheName, string setting, T defaultValue)
    {
      string str = (string) null;
      if (!string.IsNullOrEmpty(cacheName))
        str = ConfigurationManager.AppSettings["PNI.Cache.{0}.{1}".FormatString((object) cacheName, (object) setting)];
      if (str == null)
        str = ConfigurationManager.AppSettings["PNI.Cache.{0}".FormatString((object) setting)];
      if (str == null)
        return defaultValue;
      if (!typeof (T).IsEnum)
        return (T) Convert.ChangeType((object) str, typeof (T));
      try
      {
        return (T) Enum.Parse(typeof (T), str, true);
      }
      catch (Exception ex)
      {
       Log.Logger.LogError("Could not parse cache setting from App.config.", ex, (object) cacheName, (object) setting, (object) str);
        return defaultValue;
      }
    }

    public bool Locked { get; private set; }

    public void Lock() => this.Locked = true;
  }
}
