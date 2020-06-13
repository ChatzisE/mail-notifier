using System;
namespace mail_notifier.Models
{
    public class MailInfo
    {
        public string email{get;set;}
        public string userName {get;set;}
        public string userSurname {get;set;}
        public DateTime appointmentDate {get;set;}
        public string appointmentPlace {get;set;} 
    }
}