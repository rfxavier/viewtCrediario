using System;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Domain.Tests.Unit.Main.Entities.Builders
{
    public class EmailNotificationBuilder
    {
        private Guid _emailNotificationId = Guid.Empty;
        private string _emailFrom = null;
        private string _emailTo = null;
        private string _subject = null;
        private string _bodyText = null;
        private string _bodyHtml = null;
        private DateTime _dateToSend = DateTime.MaxValue;
        private bool _sent = false;
        private bool _active = false;


        public EmailNotification Build()
        {
            var emailNotification = new EmailNotification(_emailNotificationId, _emailFrom, _emailTo, _subject, _bodyText, _bodyHtml, _dateToSend);

            return emailNotification;
        }

        public EmailNotificationBuilder WithEmailNotificationId(Guid emailNotificationId)
        {
            this._emailNotificationId = emailNotificationId;
            return this;
        }

        public EmailNotificationBuilder WithEmailTo(string emailTo)
        {
            this._emailTo = emailTo;
            return this;
        }

        public EmailNotificationBuilder WithBodyText(string bodyText)
        {
            this._bodyText = bodyText;
            return this;
        }


        public EmailNotificationBuilder WithDateToSend(DateTime dateToSend)
        {
            this._dateToSend = dateToSend;
            return this;
        }



        public static implicit operator EmailNotification(EmailNotificationBuilder instance)
        {
            return instance.Build();
        }

    }
}