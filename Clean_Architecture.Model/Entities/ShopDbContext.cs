using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Model.Entities
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<AccountClient> AccountClients { get; set; }
        public virtual DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Product");
                e.HasKey(p => p.ProductId);
            });
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Category");
                e.HasKey(p => p.CategoryId);
            });
            modelBuilder.Entity<AccountClient>(e =>
            {
                e.ToTable("AccountClient");
                e.HasKey(p => p.Id);
            });
            modelBuilder.Entity<FavoriteProduct>(e =>
            {
                e.ToTable("FavoriteProduct");
                e.HasKey(p => p.Id);
                e.HasOne(p => p.Product).WithMany(fr=>fr.FavoriteProducts).HasForeignKey(p => p.ProductId);
                e.HasOne(u => u.AccountClient).WithMany(fr => fr.FavoriteProducts).HasForeignKey(u => u.UserId);
            });
            modelBuilder.Entity<Cart>(e =>
            {
                e.ToTable("Cart");
                e.HasKey(p => p.Id);
                e.HasOne(c => c.Product).WithMany(p => p.Carts).HasForeignKey(p => p.ProductId);
                e.HasOne(u => u.AccountClient).WithMany(fr => fr.Carts).HasForeignKey(u => u.UserId);
            });
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(p => p.Id);
                e.HasOne(u => u.AccountClient).WithMany(fr => fr.Orders).HasForeignKey(u => u.UserId);
            });
            modelBuilder.Entity<OrderDetail>(e =>
            {
                e.ToTable("OrderDetail");
                e.HasKey(p => p.Id);
                e.HasOne(c => c.Order).WithMany(p => p.OrderDetails).HasForeignKey(p => p.OderId);
                e.HasOne(u => u.Product).WithMany(fr => fr.OrderDetails).HasForeignKey(u => u.ProductId);
            });
        }
    }
}
