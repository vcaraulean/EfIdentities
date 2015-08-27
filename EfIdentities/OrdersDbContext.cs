using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EfIdentities
{
    public class OrdersDbContext : DbContext
    {
        public static string ConnectionString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=OrderIdentities;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public OrdersDbContext() 
            : base(ConnectionString)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<OrderLine>()
                .HasKey(x => new {x.Id, x.OrderId})
                .Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder
                .Entity<Order>()
                .HasKey(x => x.Id)
                .HasMany(x => x.Lines);
        }
    }
}