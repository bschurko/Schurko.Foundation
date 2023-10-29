// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Utilities.AppConfigSettings
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace Schurko.Foundation.Utilities
{
  public static class AppConfigSettings
  {
    private static Microsoft.Extensions.Configuration.ConfigurationManager _ConfigManager;

    public static Microsoft.Extensions.Configuration.ConfigurationManager? ConfigManager
    {
      get
      {
        AppConfigSettings._ConfigManager = new Microsoft.Extensions.Configuration.ConfigurationManager();
        return AppConfigSettings._ConfigManager;
      }
      private set => AppConfigSettings._ConfigManager = new Microsoft.Extensions.Configuration.ConfigurationManager();
    }

    public static T GetSetting<T>(string key, string def)
    {
      string str = System.Configuration.ConfigurationManager.AppSettings[key] ?? string.Empty;
      if (string.IsNullOrEmpty(str))
        str = def;
      return typeof (T).IsEnum ? (T) Enum.Parse(typeof (T), str) : (T) Convert.ChangeType((object) str, typeof (T));
    }
  }
}
