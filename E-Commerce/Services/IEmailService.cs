using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

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
                var apiKey = Configuration["SendGrid:ApiKey"]
                    ?? throw new InvalidOperationException("SendGrid:ApiKey not found!");
                var client = new SendGridClient(apiKey);
                var fromEmail = Configuration["Email:From"]  //have to set in user secrets (vs below hardcoding) for development - done
                    ?? throw new InvalidOperationException("Email:From not found!");
            //var from = new EmailAddress("ymclapp@gmail.com");
            //var from = new EmailAddress("test@example.com", "Example User");
            //var subject = "Sending with SendGrid is Fun";
            //var to = new EmailAddress("test@example.com", "Example User");
                var to = new EmailAddress(toEmail);
                //var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>Thank you!</strong>";
                var msg = MailHelper.CreateSingleEmail(fromEmail, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            if(!response.IsSuccessStatusCode)
            {
                string responseBody = await response.Body.ReadAsStringAsync();
                //TODO:  Include more info to troubleshoot
            }
            }
        }
    }
}
}
