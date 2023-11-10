using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Schurko.Foundation.Identity;
using Schurko.Foundation.Identity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Tests.Identity
{
    public class IdentityContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {

        }

        public IdentityContext()
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = Schurko.Foundation.Utilities.StaticConfigurationManager.GetConfiguration();
            var connStr = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connStr);

        }
    }
}
