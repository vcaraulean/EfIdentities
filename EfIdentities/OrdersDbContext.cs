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
    }
}