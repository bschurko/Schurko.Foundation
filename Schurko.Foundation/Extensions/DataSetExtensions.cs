// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.DataSetExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.Data;


#nullable enable
namespace PNI.Extensions
{
  public static class DataSetExtensions
  {
    public static bool HasData(this DataSet dsSet) => DataSetExtensions.CalculateHasData(dsSet, 0);

    public static bool HasData(this DataSet dsSet, int tableNumber) => DataSetExtensions.CalculateHasData(dsSet, tableNumber);

    private static bool CalculateHasData(DataSet dsSet, int tableNumber) => dsSet != null && dsSet.Tables.Count != 0 && dsSet.Tables.Count >= tableNumber + 1 && dsSet.Tables[tableNumber].Rows.Count > 0;
  }
}
