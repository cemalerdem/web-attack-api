using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Notion.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
    
}