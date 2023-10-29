using Microsoft.AspNetCore.Identity;
using Schurko.Foundation.Identity.Auth.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Identity.Auth
{
    public abstract class RoleStoreBase<TRole> : IRoleStore<ApplicationUserRole>
    {
        public abstract Task<IdentityResult> CreateAsync(ApplicationUserRole role, CancellationToken cancellationToken);
        public abstract Task<IdentityResult> DeleteAsync(ApplicationUserRole role, CancellationToken cancellationToken);
        public abstract void Dispose();
        public abstract Task<ApplicationUserRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken);
        public abstract Task<ApplicationUserRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken);
        public abstract Task<string?> GetNormalizedRoleNameAsync(ApplicationUserRole role, CancellationToken cancellationToken);
        public abstract Task<string> GetRoleIdAsync(ApplicationUserRole role, CancellationToken cancellationToken);
        public abstract Task<string?> GetRoleNameAsync(ApplicationUserRole role, CancellationToken cancellationToken);
        public abstract Task SetNormalizedRoleNameAsync(ApplicationUserRole role, string? normalizedName, CancellationToken cancellationToken);
        public abstract Task SetRoleNameAsync(ApplicationUserRole role, string? roleName, CancellationToken cancellationToken);
        public abstract Task<IdentityResult> UpdateAsync(ApplicationUserRole role, CancellationToken cancellationToken);
    }
}
