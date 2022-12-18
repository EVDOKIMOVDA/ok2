using Microsoft.EntityFrameworkCore;

namespace PizzaDelivery.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients{ get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Delivery> Deliveries{ get; set; }
        public DbSet<Pizza> Pizzas{ get; set; }
        public DbSet<Restaurant> Restaurants{ get; set; }
        public DbSet<Dop_uslugi> Dop_Uslugis{ get; set; }
    }
}
