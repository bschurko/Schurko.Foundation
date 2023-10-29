// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Identity.SecurityImpersonation
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace Schurko.Foundation.Identity.Impersonation
{
    public class SecurityImpersonation : IDisposable
    {
        private ICredentialProvider provider;

        public SecurityImpersonation(ICredentialProvider provider) => this.provider = provider;

        public void Dispose() => provider = null;

        public SecurityImpersonation GetImpersonation() => provider == null ? null : new SecurityImpersonation(provider);
    }
}
