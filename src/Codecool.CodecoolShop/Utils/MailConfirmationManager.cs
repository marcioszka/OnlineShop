using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Utils
{
    public class MailConfirmationManager
    {
        public async Task Execute(string From, string To, string subject, 
            string plainTextContent, string htmlContent)
        {
            var apiKey = Environment.GetEnvironmentVariable("CodecoolShopeAPIkey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(From);
            var to = new EmailAddress(To);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
