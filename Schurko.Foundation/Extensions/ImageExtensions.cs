// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.ImageExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;


#nullable enable
namespace PNI.Extensions
{
  public static class ImageExtensions
  {
    private static volatile ConstructorInfo _propertyItemConstructor;
    private static readonly object _propertyItemConstructorLock = new object();

    public static PropertyItem CreatePropertyItem(this Image img, int? itemId)
    {
      PropertyItem propitem = (PropertyItem) null;
      if (itemId.HasValue)
        propitem = ((IEnumerable<PropertyItem>) img.PropertyItems).FirstOrDefault<PropertyItem>((Func<PropertyItem, bool>) (p => p.Id == itemId.Value));
      if (propitem == null)
      {
        propitem = ImageExtensions.CreatePropertyItem(itemId);
        img.SetPropertyItem(propitem);
      }
      return propitem;
    }

    private static PropertyItem CreatePropertyItem(int? itemId)
    {
      if (ImageExtensions._propertyItemConstructor == (ConstructorInfo) null)
      {
        lock (ImageExtensions._propertyItemConstructorLock)
        {
          if (ImageExtensions._propertyItemConstructor == (ConstructorInfo) null)
          {
            ConstructorInfo constructor = typeof (PropertyItem).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new Type[0], (ParameterModifier[]) null);
            if (constructor != (ConstructorInfo) null)
              ImageExtensions._propertyItemConstructor = constructor;
          }
        }
      }
      if (!(ImageExtensions._propertyItemConstructor != (ConstructorInfo) null))
        return (PropertyItem) null;
      PropertyItem propertyItem = (PropertyItem) ImageExtensions._propertyItemConstructor.Invoke((object[]) null);
      if (itemId.HasValue)
        propertyItem.Id = itemId.Value;
      return propertyItem;
    }
  }
}
