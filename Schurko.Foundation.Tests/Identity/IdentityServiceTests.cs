using System.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schurko.Foundation.Identity.Auth.Context;
using Schurko.Foundation.Identity.Auth.Entity;
using Schurko.Foundation.Identity.Auth.Repository;
using Schurko.Foundation.IoC.DI;
using Schurko.Foundation.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Schurko.Foundation.Tests.Identity
{
    [TestClass]
    public class IdentityServiceTests
    {
        private IHost _host;
        private IdentityDbContext _context;
        private IdentityService _service;

        [TestInitialize]
        public void Init()
        {
            _host = Foundation.IoC.DI.IoC.InitDependency((s) =>
            {
                var config = StaticConfigurationManager.GetConfiguration();
                // Add services to the container.
                var connectionString = config.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

                s.AddDbContext<IdentityDbContext>(options =>
                    options.UseSqlServer(connectionString));

                var dbContextOptions = new DbContextOptions<IdentityDbContext>();
                var identityDbContext = new IdentityDbContext(dbContextOptions);

                s.AddScoped<Microsoft.EntityFrameworkCore.DbContext, IdentityDbContext>();

                s.AddScoped<IIdentityService, IdentityService>();
            });

            _context = _host.GetService<IdentityDbContext>();
            _service = (IdentityService)_host.GetService<IIdentityService>();
        }

        [TestMethod]
        public async Task Get_AspNetUsers_GetByName_And_ID_Test()
        {
            string username = "bschurko";

            var user = await _service.FindUserByUserNameAsync(username).ConfigureAwait(false);
             
            Assert.IsNotNull(user);

            var u = await _service.FindUserByIdAsync(user.Id).ConfigureAwait(false);
            Assert.IsNotNull(u);
        }

        [TestMethod]
        public async Task Create_Role_Test()
        {
            ApplicationRole role = new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "User"
            };

            var roleResult = await _service.CreateRoleAsync(role).ConfigureAwait(false);

            Assert.IsNotNull(roleResult);
            Assert.IsTrue(roleResult != null && roleResult.Name.Equals(role.Name));

            var roles = await _service.GetRolesAsync().ConfigureAwait(false);

            Assert.IsNotNull(roles);

            var foundRole = roles.FirstOrDefault(cc => cc.Id == role.Id);

            Assert.IsNotNull(foundRole);
        }

        [TestMethod]
        public async Task Create_UserClaim_Test()
        {
            ApplicationUserClaim claim = new ApplicationUserClaim()
            {
                UserId = "876679cf-3b9a-429e-ab83-9af7491c5190",
                ClaimType = "Role",
                ClaimValue = "Admin"
            };

            var c = await _service.CreateUserClaimAsync(claim).ConfigureAwait(false);

            Assert.IsNotNull(c);

            var userClaims = await _service.GetUserClaimsAsync(claim.UserId).ConfigureAwait(false);

            Assert.IsNotNull(userClaims);

            var foundClaim = userClaims.FirstOrDefault(cc => cc.UserId == claim.UserId);

            Assert.IsNotNull(foundClaim);
        }

        [TestMethod]
        public async Task Create_Role_Claim_Test()
        {
            ApplicationRoleClaim roleClaim = new ApplicationRoleClaim()
            {
                RoleId = "7a2da350-68c1-44cc-a2b9-96fad8b3b9d5",
                ClaimType = "Role",
                ClaimValue = "Admin"
            };

            var claim = await _service.CreateRoleClaimAsync(roleClaim).ConfigureAwait(false);

            Assert.IsNotNull(claim);

            var roleClaims = await _service.GetRoleClaimsAsync(claim.RoleId).ConfigureAwait(false);

            Assert.IsNotNull(roleClaims);

            var foundRole = roleClaims.FirstOrDefault(cc => cc.RoleId == roleClaim.RoleId);

            Assert.IsNotNull(foundRole);

            Assert.IsTrue(foundRole.ClaimValue == claim.ClaimValue);

        }

        [TestMethod]
        public async Task Validate_User_Password()
        {
            var u = await _service.FindUserByUserNameAsync("bschurko").ConfigureAwait(false);
            Assert.IsNotNull(u);
            string id = "876679cf-3b9a-429e-ab83-9af7491c5190";
            var user = await _service.FindUserByIdAsync(id).ConfigureAwait(false);
            Assert.IsNotNull(user);

            string password = "schurko";
            string username = "bschurko";
            var isValidPassword = await _service.IsValidUserPasswordAsync(username, password).ConfigureAwait(false);
            Assert.IsTrue(isValidPassword);
        }
    }
}
