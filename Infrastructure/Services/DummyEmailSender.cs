using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Kasnije ovdje može ići pravi email (SMTP)
            Console.WriteLine($"Sending email to: {email} / Subject: {subject}");
            return Task.CompletedTask;
        }
    }
}
