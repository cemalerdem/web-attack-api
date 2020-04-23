using System;
using System.Net;
using System.Net.Mail;
using Notion.Services.Abstract;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Extensions.Configuration;

namespace Notion.Services.Helper
{
    public class SendGridEmailSender  : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject,string message)
        {
            var apiKey = _configuration["Email:SendGridAPIKey"];
            var fromMail = _configuration["Email:FromMail"];
            var senderName = _configuration["Email:Name"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromMail, senderName);
            var to = new EmailAddress(toEmail);
            var htmlContent = "<strong>" + message + "</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, htmlContent);

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            //msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);
        }
    }
}