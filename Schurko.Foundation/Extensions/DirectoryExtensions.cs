// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.DirectoryExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.IO;


#nullable enable
namespace PNI.Extensions
{
  public static class DirectoryExtensions
  {
    public static bool IsEmpty(this DirectoryInfo dInfo) => dInfo.GetDirectories().Length == 0 && dInfo.GetFiles().Length == 0;
  }
}
