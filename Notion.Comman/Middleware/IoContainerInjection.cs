using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Notion.Comman.RequestModels;
using Notion.Common.RequestModels;

namespace Notion.Comman.Middleware
{
    public static class IoContainerInjection
    {
        public static IServiceCollection CommonLayerDependencies(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UserRegisterRequest>, UserRegisterRequestValidator>();
            services.AddTransient<IValidator<UserLoginRequest>, UserLoginRequestValidator>();

            return services;
        }
    }
}