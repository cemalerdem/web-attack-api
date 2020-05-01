using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Notion.Services.Abstract;
using Notion.Services.Abstract.Base;
using Notion.Services.Concrete;
using Notion.Services.Helper;

namespace Notion.Services.Middleware
{
    public static class IoContainerInjection
    {
        public static IServiceCollection ServiceLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IService, Service>();
            services.AddTransient<IEmailService, SendGridEmailSender>();
            services.AddAutoMapper(typeof(UserService).Assembly);
            return services;
        }
    }
}