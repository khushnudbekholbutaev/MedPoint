using Microsoft.EntityFrameworkCore;
using MedPoint.Domain.Entities.Catalogs;
using MedPoint.Domain.Entities.Categories;
using MedPoint.Domain.Entities.Medications;
using MedPoint.Domain.Entities.OrderDetails;
using MedPoint.Domain.Entities.Orders;
using MedPoint.Domain.Entities.Payments;
using MedPoint.Domain.Entities.Users;
using System.Reflection.Emit;
using MedPoint.Domain.Entities.Banners;
using MedPoint.Domain.Entities.CartItems;
using MedPoint.Domain.Entities.Favorites;

namespace MedPoint.Data.DbContexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments {  get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Users) 
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasMany(o => o.Details)  
                .WithOne(od => od.Order) 
                .HasForeignKey(od => od.OrderId)  
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Medication>()
                .HasMany(m => m.Details)
                .WithOne(od => od.Medication)
                .HasForeignKey(od => od.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Banner>()
                .HasOne(c => c.Category)
                .WithMany(m => m.Banners)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Banner>()
                .HasOne(b => b.Medication)
                .WithMany(m => m.Banners)
                .HasForeignKey(b => b.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
