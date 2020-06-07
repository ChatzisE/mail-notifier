using System;
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
        private static string TOKEN;
        private const string SUBJECT = "your civil service appointment";
        public SendMailService(IConfiguration config)
        {;
            _config = config;
            TOKEN = config.GetValue<string>("token");
        }
        public async Task<Response> CreateAndSendMail(MailInfo info)
        {
            SendGridClient client = new SendGridClient(TOKEN);
            EmailAddress from = new EmailAddress("mail-notifier@easy-appointment.com", "Easy Apointment");
            EmailAddress to = new EmailAddress(info.email,null);
            string plainTextContent = @$"{info.userName}, {info.userSurname} \n
                                         Your appointment at {info.appointmentPlace}
                                         is succesfully arange for {info.appointmentDate.ToLongDateString()}";
            string htmlContent = "<strong>for any futher clarification please feel free to contact us!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, SUBJECT, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}