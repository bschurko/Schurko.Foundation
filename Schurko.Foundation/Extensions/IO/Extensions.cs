// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Extensions.IO.Extensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.Text;


#nullable enable
namespace Schurko.Foundation.Extensions.IO
{
  public static class Extensions
  {
    public static byte[] ToUtf8ByteArray(this string str) => Encoding.UTF8.GetBytes(str);

    public static string ToUtf8String(this byte[] bytes) => Encoding.UTF8.GetString(bytes);
  }
}
