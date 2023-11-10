using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Schurko.Foundation.Identity.Auth.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Identity.Auth.Context
{
    public class IdentityDbContext : DbContext 
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public IdentityDbContext()
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }
        public DbSet<ApplicationUserLogin> ApplicationUserLogin { get; set; }
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }
        public DbSet<ApplicationUserToken> ApplicationUserToken { get; set; }

    }
}
