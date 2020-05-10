using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.SignalR;
using Notion.Api.Helpers;
using Notion.Comman.Dtos;
using Notion.Services.Abstract;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Notion.Api.Middleware
{
    public class RequestMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IServiceProvider _serviceProvider;
        public RequestMiddleware(RequestDelegate next, IHubContext<NotificationHub> hubContext, IServiceProvider serviceProvider)
        {
            _next = next;
            _hubContext = hubContext;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = context.Request.GetDisplayUrl();
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            
            var request = new RequestDto
            {
                CreatedBy = context.User?.FindFirstValue(ClaimTypes.Email),
                MethodType = context.Request.Method,
                Path = context.Request.Path,
                QueryParameter = context.Request.QueryString.ToString(),
                StatusCode = context.Response.StatusCode.ToString(),
                RequestPayload = requestBodyText,
                CreatedAtUTC = DateTime.UtcNow
            };
            await SaveRequestToDb(request);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            string notification = JsonConvert.SerializeObject(request);
            await _hubContext.Clients.All.SendAsync("notification", notification);
            
            await _next(context);
            context.Request.Body = originalRequestBody;
        }

        private async Task SaveRequestToDb(RequestDto request)
        {
            using var scope = _serviceProvider.CreateScope();
            var adminService = scope.ServiceProvider.GetRequiredService<IAdminService>();
            await adminService.SaveRequestModelToDatabase(request);
        }

        

    }
}