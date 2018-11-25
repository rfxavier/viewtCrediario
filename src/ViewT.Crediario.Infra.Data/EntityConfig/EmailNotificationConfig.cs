using System.Data.Entity.ModelConfiguration;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Infra.Data.EntityConfig
{
    public class EmailNotificationConfig : EntityTypeConfiguration<EmailNotification>
    {
        public EmailNotificationConfig()
        {
            HasKey(e => e.EmailNotificationId);
        }   
    }
}