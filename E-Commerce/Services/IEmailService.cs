using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public interface IEmailService
    {
        Task SendEmail ( string to, string subject, string bodyPlain, string bodyHtml );

    }

    public class SendGridEmailService : IEmailService
    {
        private IConfiguration Configuration { get; }

        public SendGridEmailService ( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public async Task SendEmail(string to, string subject, string plainTextContent, string bodyHtml)
            {
                var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("ymclapp@gmail.com");
            //var from = new EmailAddress("test@example.com", "Example User");
            //var subject = "Sending with SendGrid is Fun";
            //var to = new EmailAddress("test@example.com", "Example User");
                var to = new EmailAddress(toEmail);
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }
}
}
