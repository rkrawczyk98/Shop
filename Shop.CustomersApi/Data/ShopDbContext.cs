using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.UsersApi.Models;
using System.Reflection.Emit;

namespace Shop.UsersApi.Data
{
    public class ShopDbContext : IdentityDbContext<ApplicationUser ,ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(e => e.Id)
                    .HasName("PK_User");
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.Roles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.HasKey(e => e.Id)
                    .HasName("PK_Role");
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            builder.Entity<ApplicationUserClaim>(b =>
            {
                b.HasOne(e => e.User)
                    .WithMany(au => au.Claims);
            });

            builder.Entity<ApplicationUserLogin>(b =>
            {
                b.HasOne(e => e.User)
                    .WithMany(au => au.Logins);
            });

            builder.Entity<ApplicationUserRole>(b =>
            {
                b.HasOne(e => e.User)
                    .WithMany(au => au.Roles);
            });

            builder.Entity<ApplicationUserToken>(b =>
            {
                b.HasOne(e => e.User)
                    .WithMany(au => au.Tokens);
            });
        }
    }
}
