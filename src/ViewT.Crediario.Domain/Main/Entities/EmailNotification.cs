using System;
using ViewT.Crediario.Domain.Core.Models;

namespace ViewT.Crediario.Domain.Main.Entities
{
    public class EmailNotification : Entity
    {
        protected EmailNotification() { }
        public EmailNotification(Guid emailNotificationId, string emailFrom, string emailTo, string subject, string bodyText, string bodyHtml,
            DateTime dateToSend)
        {
            EmailNotificationId = emailNotificationId;
            EmailFrom = emailFrom;
            EmailTo = emailTo;
            Subject = subject;
            BodyText = bodyText;
            BodyHtml = bodyHtml;
            DateToSend = dateToSend;
            Sent = false;
            Active = true;
            DateCreated = DateTime.Now;
        }

        public Guid EmailNotificationId { get; private set; }
        public string EmailFrom { get; private set; }
        public string EmailTo { get; private set; }
        public string Subject { get; private set; }
        public string BodyText { get; private set; }
        public string BodyHtml { get; private set; }
        public DateTime DateToSend { get; private set; }
        public bool Sent { get; private set; }
    }
}