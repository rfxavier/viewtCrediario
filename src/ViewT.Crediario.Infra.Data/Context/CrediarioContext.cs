using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ViewT.Crediario.Domain.Main.Entities;
using ViewT.Crediario.Domain.Main.Enums;
using ViewT.Crediario.Infra.Data.EntityConfig;

namespace ViewT.Crediario.Infra.Data.Context
{
    public class CrediarioContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Version> Versions { get; set; }


        public CrediarioContext()
            :base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CrediarioContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(250));

            modelBuilder.Configurations.Add(new DeviceConfig());
            modelBuilder.Configurations.Add(new EmailNotificationConfig());
            modelBuilder.Configurations.Add(new PersonConfig());
            modelBuilder.Configurations.Add(new TokenConfig());
            modelBuilder.Configurations.Add(new VersionConfig());

            modelBuilder.ComplexType<DeviceStatus>()
                .Ignore(r => r.Name);

            modelBuilder.ComplexType<DeviceOs>()
                .Ignore(r => r.Name);

            modelBuilder.ComplexType<PersonUserStatus>()
                .Ignore(r => r.Name);


            base.OnModelCreating(modelBuilder);
        }
    }
}