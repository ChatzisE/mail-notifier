using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using mail_notifier.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace mail_notifier.Services
{
    public class SendMailService
    {
        private static IConfiguration _config;
        private const string SUBJECT = "Your civil service appointment";
        public SendMailService(IConfiguration config)
        {
            ;
            _config = config;
        }
        public async Task<Response> CreateAndSendMail(MailInfo info)
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            SendGridClient client = new SendGridClient(apiKey);
            EmailAddress from = new EmailAddress("itp18406@hua.gr", "Easy Apointment");
            EmailAddress to = new EmailAddress(info.email, null);
            string correctDt = ConvertToAthensTime(info.appointmentDate);
            string htmlContent = @"<strong>" + info.userName + " " + info.userSurname + "</strong>" +
                                     "<br>Your appointment at  <ins>" + info.appointmentPlace +
                                     "</ins> has been  succesfully aranged for <ins>" + correctDt + "</ins>" +
                                     "<br> <strong>For any futher clarification please feel free to contact us!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, SUBJECT, null, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response;
        }
        private string ConvertToAthensTime(DateTime input)
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Europe/Athens");
            DateTime targetTime = TimeZoneInfo.ConvertTime(input, est);
            Console.WriteLine(targetTime.ToString("MM/dd/yy H:mm"));
            return targetTime.ToString("MM/dd/yy H:mm");
        }
    }
}