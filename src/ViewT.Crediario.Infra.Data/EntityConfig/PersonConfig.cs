using System.Data.Entity.ModelConfiguration;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Infra.Data.EntityConfig
{
    public class PersonConfig : EntityTypeConfiguration<Person>
    {
        public PersonConfig()
        {
            HasKey(p => p.PersonId);

            HasOptional(p => p.Token)
                .WithRequired(t => t.Person);

            Property(p => p.PersonUserStatus.Value)
                .HasColumnName("UserStatus")
                .HasColumnType("int");
        }
    }
}