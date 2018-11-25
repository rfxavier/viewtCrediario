using System.Data.Entity.ModelConfiguration;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Infra.Data.EntityConfig
{
    public class TokenConfig : EntityTypeConfiguration<Token>
    {
        public TokenConfig()
        {
            HasKey(t => t.TokenId);

            Property(d => d.DeviceOs.Value)
                .HasColumnName("DeviceOs")
                .HasColumnType("int");
        }
    }
}