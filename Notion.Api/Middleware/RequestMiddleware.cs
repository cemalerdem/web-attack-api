using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.SignalR;
using Notion.Api.Helpers;
using Notion.Comman.Dtos;
using Notion.Services.Abstract;
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
                method = context.Request.Method,
                path = context.Request.Path,
                query = context.Request.QueryString.ToString(),
                statusCode = context.Response.StatusCode.ToString(),
                requestPayload = requestBodyText,
                timestamp = ConvertDateTimeToTimestamp(DateTime.UtcNow)
            };
            //var predictionResult = KerasPrediction.GetPredictionResponse(request);
            //request.result = predictionResult.Result;
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

        private static double ConvertDateTimeToTimestamp(DateTime value)
        {
            TimeSpan epoch = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //return the total seconds (which is a UNIX timestamp)
            return (double)epoch.TotalSeconds;
        }


    }
}