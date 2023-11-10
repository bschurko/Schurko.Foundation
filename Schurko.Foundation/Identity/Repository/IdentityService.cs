using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schurko.Foundation.Identity.Context;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Schurko.Foundation.Logging;
using Microsoft.Extensions.Logging;
using System.Data.Entity;
using Schurko.Foundation.Identity.Entity;

namespace Schurko.Foundation.Identity.Repository
{
    public class IdentityService : IIdentityService
    {
        private readonly Context.IdentityDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.IPasswordHasher<AppUser> _passwordHasher;
        public IdentityService(Context.IdentityDbContext context,
            Microsoft.AspNetCore.Identity.IPasswordHasher<AppUser> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new AppUser(), password);
        }

        public async Task<ApplicationRole> CreateRoleAsync(ApplicationRole role)
        {
            if (role == null) throw new ArgumentException(nameof(role));

            _context.ApplicationRole.Add(role);
            _context.SaveChanges();

            return role;
        }

        public async Task<ApplicationRoleClaim> CreateRoleClaimAsync(ApplicationRoleClaim claim)
        {
            if (claim == null) throw new ArgumentNullException(nameof(claim));

            _context.ApplicationRoleClaim.Add(claim);
            _context.SaveChanges();

            return claim;
        }

        public async Task<AppUser> CreateUserAsync(AppUser user, string password = null)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (!string.IsNullOrEmpty(password))
            {
                var hashedPassword = _passwordHasher.HashPassword(user, password);
                user.PasswordHash = hashedPassword;
            }

            _context.AppUsers.Add(user);
            _context.SaveChanges();

            return user;
        }

        public async Task<ApplicationUserClaim> CreateUserClaimAsync(ApplicationUserClaim claim)
        {
            if (claim == null) throw new ArgumentNullException(nameof(claim));

            _context.ApplicationUserClaim.Add(claim);
            _context.SaveChanges();

            return claim;
        }

        public async Task<ApplicationUserRole> CreateUserRoleAsync(string userId, string roleId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId))
                throw new Exception("Supplied argument [id] is null");

            var role = new ApplicationUserRole() { RoleId = roleId, UserId = userId };

            _context.ApplicationUserRole.Add(role);
            _context.SaveChanges();

            return role;
        }

        public async Task<AppUser> FindUserByIdAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("Supplied argument [id] is null");

                return _context.AppUsers.FirstOrDefault(cc => cc.Id == id);
            }
            catch (Exception ex)
            {
                Log.Logger.LogError("FindUserByIdAsync Exception", ex);
                throw;
            }
        }

        public async Task<AppUser> FindUserByUserNameAsync(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username)) throw new Exception("Supplied argument [username] is null");

                var user = _context.AppUsers.FirstOrDefault(
                    cc => cc.UserName != null &&
                    cc.UserName == username);

                return user;
            }
            catch (Exception ex)
            {
                Log.Logger.LogError("FindUserByUserNameAsync Exception", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationRoleClaim>> GetRoleClaimsAsync(string roleId)
        {
            if (roleId == null) throw new Exception(nameof(roleId));

            return _context.ApplicationRoleClaim.Where(cc => cc.RoleId == roleId).ToList();
        }

        public async Task<IEnumerable<ApplicationRole>> GetRolesAsync()
        {
            return _context.ApplicationRole.ToList();
        }

        public async Task<IEnumerable<ApplicationUserRole>> GetRolesByUserIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException(nameof(id));

            return _context.ApplicationUserRole.Where(cc => cc.UserId == id).ToList();
        }

        public async Task<IEnumerable<ApplicationUserClaim>> GetUserClaimsAsync(string userId)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            return _context.ApplicationUserClaim.Where(cc => cc.UserId.Equals(userId)).ToList();
        }

        public async Task<bool> IsValidUserPasswordAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            var user = await FindUserByUserNameAsync(username).ConfigureAwait(false);

            if (user == null || string.IsNullOrEmpty(user.PasswordHash)) return false;

            var isValid = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            return isValid == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success;
        }

        public async Task<bool> IsHashPasswordValidAsync(string hash, string password)
        {
            if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(password)) return false;
            AppUser user = new AppUser()
            {
                PasswordHash = hash
            };

            string hashPass = _passwordHasher.HashPassword(user, password);

            return hash.Equals(hashPass);
        }
    }

    public interface IIdentityService
    {
        #region Interface Methods

        public string HashPassword(string password);

        public Task<AppUser> CreateUserAsync(AppUser user, string password = null);

        public Task<AppUser> FindUserByUserNameAsync(string username);

        public Task<bool> IsValidUserPasswordAsync(string username, string password);

        public Task<AppUser> FindUserByIdAsync(string id);

        public Task<ApplicationRole> CreateRoleAsync(ApplicationRole role);

        public Task<IEnumerable<ApplicationRole>> GetRolesAsync();

        public Task<IEnumerable<ApplicationUserRole>> GetRolesByUserIdAsync(string id);

        public Task<ApplicationUserRole> CreateUserRoleAsync(string userId, string roleId);

        public Task<ApplicationUserClaim> CreateUserClaimAsync(ApplicationUserClaim claim);

        public Task<ApplicationRoleClaim> CreateRoleClaimAsync(ApplicationRoleClaim claim);

        public Task<IEnumerable<ApplicationUserClaim>> GetUserClaimsAsync(string userId);

        public Task<IEnumerable<ApplicationRoleClaim>> GetRoleClaimsAsync(string roleId);

        public Task<bool> IsHashPasswordValidAsync(string hash, string password);

        #endregion
    }
}
