// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.StreamExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.IO;


#nullable enable
namespace PNI.Extensions
{
  public static class StreamExtensions
  {
    public static void CopyStream(
      this Stream sourceStream,
      Stream destinationStream,
      int bufferSize)
    {
      StreamExtensions.InternalCopyStream(sourceStream, destinationStream, bufferSize);
    }

    internal static void InternalCopyStream(
      Stream sourceStream,
      Stream destinationStream,
      int bufferSize)
    {
      byte[] buffer = new byte[bufferSize];
      for (int count = sourceStream.Read(buffer, 0, bufferSize); count > 0; count = sourceStream.Read(buffer, 0, bufferSize))
        destinationStream.Write(buffer, 0, count);
    }
  }
}
