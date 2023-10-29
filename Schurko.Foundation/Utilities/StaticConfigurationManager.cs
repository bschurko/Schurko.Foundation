// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Utilities.StaticConfigurationManager
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using Microsoft.Extensions.Configuration;
using System.IO;


#nullable enable
namespace Schurko.Foundation.Utilities
{
  public static class StaticConfigurationManager
  {
    private static IConfiguration _AppSetting;

    public static IConfiguration? AppSetting
    {
      get => StaticConfigurationManager._AppSetting != null ? (IConfiguration) null : (StaticConfigurationManager._AppSetting = StaticConfigurationManager.GetConfiguration());
      private set
      {
        if (StaticConfigurationManager._AppSetting == null)
          return;
        StaticConfigurationManager._AppSetting = (IConfiguration) new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
      }
    }

    public static IConfiguration GetConfiguration(string? appSettingsName = null)
    {
      StaticConfigurationManager._AppSetting = (IConfiguration) new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(appSettingsName == null ? "appsettings.json" : appSettingsName, true, true).Build();
      return StaticConfigurationManager._AppSetting;
    }
  }
}
