using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Identity.Auth
{
 

    public abstract class UserStoreBase<TUser> : Microsoft.AspNetCore.Identity.IUserStore<AppUser>
    {
        public abstract Task<Microsoft.AspNetCore.Identity.IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken);
        public abstract Task<Microsoft.AspNetCore.Identity.IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken);
        public abstract void Dispose();
        public abstract Task<AppUser?> FindByIdAsync(string userId, CancellationToken cancellationToken);
        public abstract Task<AppUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        public abstract Task<string?> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken);
        public abstract Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken);
        public abstract Task<string?> GetUserNameAsync(AppUser user, CancellationToken cancellationToken);
        public abstract Task SetNormalizedUserNameAsync(AppUser user, string? normalizedName, CancellationToken cancellationToken);
        public abstract Task SetUserNameAsync(AppUser user, string? userName, CancellationToken cancellationToken);
        public abstract Task<Microsoft.AspNetCore.Identity.IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken);
    }
}
