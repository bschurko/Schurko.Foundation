// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Identity.ICredentialProvider
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll


#nullable enable
using Schurko;

namespace Schurko.Foundation.Identity.Impersonation
{
    public interface ICredentialProvider
    {
        string Domain { get; }

        string User { get; }

        string Password { get; }
    }
}
