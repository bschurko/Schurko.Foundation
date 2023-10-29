// Decompiled with JetBrains decompiler
// Type: PNI.Utilities.RetryAlgorithm
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace PNI.Utilities
{
  public static class RetryAlgorithm
  {
    private static readonly Random RandomNumber = new Random();

    public static int NthPower(int n, int ceiling = 8) => (int) Math.Pow(2.0, (double) Math.Min(n, ceiling));

    public static int Fibonacci(int n, int ceiling = 15)
    {
      int num1 = 0;
      int num2 = 1;
      for (int index = 0; index < n && index < ceiling; ++index)
      {
        int num3 = num1;
        num1 = num2;
        int num4 = num2;
        num2 = num3 + num4;
      }
      return num1;
    }

    public static int RandomZeroToNth(int n, int ceiling = 8) => RetryAlgorithm.RandomNumber.Next(0, RetryAlgorithm.NthPower(n, ceiling));

    public static int RandomHalfToNth(int n, int ceiling = 8) => RetryAlgorithm.RandomNumber.Next(RetryAlgorithm.NthPower(n - 1, ceiling), RetryAlgorithm.NthPower(n, ceiling));

    public static int RandomFibonacci(int n, int ceiling = 15) => RetryAlgorithm.RandomNumber.Next(RetryAlgorithm.Fibonacci(n - 1, ceiling), RetryAlgorithm.Fibonacci(n, ceiling));
  }
}
