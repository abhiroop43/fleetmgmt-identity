using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FleetMgmt.Identity.Infrastructure.Data
{
    public static class EmailNotificationHelper
    {
        public static async Task<bool> SendEmailNotification(string apiKey, string emailSubject, string emailHtmlBody, string emailPlainTextBody, List<EmailRecipientDto> recipients)
        {
            // var apiKey = _configuration.GetSection("SendGridAPIKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("no-reply@fleetmanagement.com", "Fleet Management System");
            var tos = new List<EmailAddress>();
            foreach (var recipient in recipients)
            {
                tos.Add(new EmailAddress(recipient.RecipientEmailAddress, recipient.RecipientName));
            }
            var subject = emailSubject; //"Test Email with SendGrid";
            var htmlContent = emailHtmlBody; //"<strong>HTML text for the Test Email</strong>";
            // var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, emailPlainTextBody, htmlContent, false);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}