using System.Data.Entity.ModelConfiguration;
using ViewT.Crediario.Domain.Main.Entities;

namespace ViewT.Crediario.Infra.Data.EntityConfig
{
    public class DeviceConfig : EntityTypeConfiguration<Device>
    {
        public DeviceConfig()
        {
            HasKey(d => d.DeviceId);

            HasRequired(d => d.Person)
                .WithMany(p => p.Devices)
                .HasForeignKey(d => d.PersonId);

            Property(d => d.DeviceStatus.Value)
                .HasColumnName("DeviceStatus")
                .HasColumnType("int");

            Property(d => d.DeviceOs.Value)
                .HasColumnName("DeviceOs")
                .HasColumnType("int");

        }
    }
}