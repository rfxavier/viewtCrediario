using System.Data.Entity.ModelConfiguration;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Infra.Data.EntityConfig
{
    public class VersionConfig : EntityTypeConfiguration<Version>
    {
        public VersionConfig()
        {
            HasKey(v => v.VersionId);

            Property(d => d.Os.Value)
                .HasColumnType("int");
        }
    }
}