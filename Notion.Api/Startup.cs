using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Notion.Api.Helper.ConfigureService;
using Notion.DAL.Context;
using Notion.Services.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Notion.Api.Middleware;
using FluentValidation.AspNetCore;
using Notion.Comman.Middleware;
using Notion.DAL.Extensions;
using Notion.Api.Helpers;

namespace Notion.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Swagger Config
            services.SwaggerConfiguration();

            services.AddDbContext<AppDataContext>(x => x.UseSqlServer
            (Configuration.GetConnectionString("DefaultConnection")));

            //DAL Layer
            services.DalLayerDependencies();

            //Service Layer
            services.ServiceLayerDependencies();

            //Comman Layer
            services.CommonLayerDependencies();
            services.ConfigureCors();

            services.AddControllers().AddFluentValidation();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["AppSettings:JwtIssuer"],
                        ValidAudience = Configuration["AppSettings:JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireMemberRole", policy => policy.RequireRole("Admin, Member"));
            });

            services.AddSignalR();
            //AddAzureSignalR("Endpoint=https://notionapisignalrservice.service.signalr.net;AccessKey=A/39QiysIjWME2eKZ6LeMa75rWl61drI63qLD4YLnho=;Version=1.0;");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseMiddleware<RequestMiddleware>();
            app.UseMiddleware<ResponseMiddleware>();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            //app.UseAzureSignalR(routes =>
            //{
            //    routes.MapHub<NotificationHub>("/notificationhub");

            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notificationhub");
                endpoints.MapControllers();
            });

            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
