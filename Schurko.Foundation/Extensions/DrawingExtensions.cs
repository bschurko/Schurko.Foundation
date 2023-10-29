// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.DrawingExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.Drawing;

namespace PNI.Extensions
{
  public static class DrawingExtensions
  {
    public static Rectangle ToRectangle(this RectangleF rectF) => new Rectangle((int) rectF.X, (int) rectF.Y, (int) rectF.Width, (int) rectF.Height);
  }
}
