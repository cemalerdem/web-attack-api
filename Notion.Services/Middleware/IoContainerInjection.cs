using Microsoft.Extensions.DependencyInjection;
using Notion.Services.Abstract;
using Notion.Services.Abstract.Base;
using Notion.Services.Concrete;

namespace Notion.Services.Middleware
{
    public static class IoContainerIntejction
    {
        public static IServiceCollection ServiceLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IService, Service>();

            return services;
        }
    }
}