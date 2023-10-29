// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.DataRowExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Data;


#nullable enable
namespace PNI.Extensions
{
  public static class DataRowExtensions
  {
    public static bool ContainsColumn(this DataRow dataRow, string column) => DataRowExtensions.ContainsColumn(column, dataRow);

    public static bool ContainsColumn(this DataRow dataRow, Enum column) => DataRowExtensions.ContainsColumn(Enum.GetName(column.GetType(), (object) column), dataRow);

    private static bool ContainsColumn(string column, DataRow dataRow) => dataRow.Table.Columns.Contains(column);
  }
}
