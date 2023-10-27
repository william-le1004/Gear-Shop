using Domain.Entities;
using Application.Common.ExtensionMethods;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Extensions;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Cart> ShoppingCart { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfiguration(new OrderDetailsConfigurations());

            //builder.ApplyConfiguration(new OrderConfigurations());

            builder.Seed();
        }

    }
}
