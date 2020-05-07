using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notion.DAL.Entity.Concrete.Admin;
using System;
using Notion.DAL.Entity.Concrete.User;

namespace Notion.DAL.Context
{
    public class AppDataContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
        UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options)
        {
        }

        public DbSet<RequestModel> RequestModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRoles =>
            {
                userRoles.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRoles.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRoles.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }

    }
}