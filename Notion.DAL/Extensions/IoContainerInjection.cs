using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Notion.DAL.Context;
using Notion.DAL.Entity.Concrete;

namespace Notion.DAL.Extensions
{
    public static class IoContainerInjection
    {
        public static IServiceCollection DalLayerDependencies(this IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>();

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<AppDataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

            services.ConfigureApplicationCookie(o => {
                o.ExpireTimeSpan = TimeSpan.FromDays(5);
                o.SlidingExpiration = true;
            });

            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3));
            return services;
        }
    }
}