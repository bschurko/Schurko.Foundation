using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Identity.Auth.Entity
{
    [Table("AspNetUsers")]
    public class AppUser
    {

        public AppUser()
        {
            Id = Guid.NewGuid().ToString();
            SecurityStamp = Guid.NewGuid().ToString();
        }


        /// <summary>
        /// Gets or sets the primary key for this user.
        /// </summary>
        /// [PersonalData]

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public virtual string Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
       /// [ProtectedPersonalData]
        public virtual string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the normalized user name for this user.
        /// </summary>
        public virtual string? NormalizedUserName { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        [ProtectedPersonalData]
        public virtual string? Email { get; set; }

        /// <summary>
        /// Gets or sets the normalized email address for this user.
        /// </summary>
        public virtual string? NormalizedEmail { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address.
        /// </summary>
        /// <value>True if the email address has been confirmed, otherwise false.</value>
        [PersonalData]
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        public virtual string? PasswordHash { get; set; }

        /// <summary>
        /// A random value that must change whenever a users credentials change (password changed, login removed)
        /// </summary>
        public virtual string? SecurityStamp { get; set; }

        /// <summary>
        /// A random value that must change whenever a user is persisted to the store
        /// </summary>
        public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        [ProtectedPersonalData]
        public virtual string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their telephone address.
        /// </summary>
        /// <value>True if the telephone number has been confirmed, otherwise false.</value>
        [PersonalData]
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if two factor authentication is enabled for this user.
        /// </summary>
        /// <value>True if 2fa is enabled, otherwise false.</value>
        [PersonalData]
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        /// <remarks>
        /// A value in the past means the user is not locked out.
        /// </remarks>
        public virtual DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        /// <value>True if the user could be locked out, otherwise false.</value>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        public virtual int AccessFailedCount { get; set; }


    }

    [Table("AspNetRoles")]
    public class ApplicationRole
    {
        public ApplicationRole() { }


        /// <summary>
        /// Initializes a new instance of <see cref="IdentityRole{TKey}"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        public ApplicationRole(string roleName)
        {
            Name = roleName;
        }

        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual string Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public virtual string? Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name for this role.
        /// </summary>
        public virtual string? NormalizedName { get; set; }

        /// <summary>
        /// A random value that should change whenever a role is persisted to the store
        /// </summary>
        public virtual string? ConcurrencyStamp { get; set; }

        /// <summary>
        /// Returns the name of the role.
        /// </summary>
        /// <returns>The name of the role.</returns>
        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }

    [Keyless]
    [Table("AspNetUserRoles")]
    public class ApplicationUserRole
    {
        public ApplicationUserRole() { }
        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a role.
        /// </summary>
        public virtual string UserId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
        public virtual string RoleId { get; set; } = default!;
    }

    [Table("AspNetUserClaims")]
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public ApplicationUserClaim() { }

        /// <summary>
        /// Gets or sets the identifier for this user claim.
        /// </summary>
        [Key]
        public virtual int Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the primary key of the user associated with this claim.
        /// </summary>
        public virtual string UserId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the claim type for this claim.
        /// </summary>
        public virtual string? ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the claim value for this claim.
        /// </summary>
        public virtual string? ClaimValue { get; set; }
    }

    [Keyless]
    [Table("AspNetUserLogins")]
    public class ApplicationUserLogin
    {
        public ApplicationUserLogin() { }
        /// <summary>
        /// Gets or sets the login provider for the login (e.g. facebook, google)
        /// </summary>
        public virtual string LoginProvider { get; set; } = default!;

        /// <summary>
        /// Gets or sets the unique provider identifier for this login.
        /// </summary>
        public virtual string ProviderKey { get; set; } = default!;

        /// <summary>
        /// Gets or sets the friendly name used in a UI for this login.
        /// </summary>
        public virtual string? ProviderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the user associated with this login.
        /// </summary>
        //[Key]
        public virtual string UserId { get; set; } = default!;
    }

    [Table("AspNetRoleClaims")]
    public class ApplicationRoleClaim
    {
        public ApplicationRoleClaim() { }

        /// <summary>
        /// Gets or sets the identifier for this role claim.
        /// </summary>
        [Key]
        public virtual int Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the of the primary key of the role associated with this claim.
        /// </summary>
        public virtual string RoleId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the claim type for this claim.
        /// </summary>
        public virtual string? ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the claim value for this claim.
        /// </summary>
        public virtual string? ClaimValue { get; set; }
    }

    [Table("AspNetUserTokens")]
    public class ApplicationUserToken
    {
        public ApplicationUserToken() { }
        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual string UserId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        public virtual string LoginProvider { get; set; } = default!;

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        public virtual string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        [ProtectedPersonalData]
        public virtual string? Value { get; set; }

    }
}
