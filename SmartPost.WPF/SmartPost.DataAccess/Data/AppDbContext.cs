using Microsoft.EntityFrameworkCore;
using SmartPost.Domain.Entities.Brands;
using SmartPost.Domain.Entities.CancelOrders;
using SmartPost.Domain.Entities.Cards;
using SmartPost.Domain.Entities.Categories;
using SmartPost.Domain.Entities.InventoryLists;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Entities.StorageProducts;
using SmartPost.Domain.Entities.Users;

namespace SmartPost.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<StorageProduct> StorageProducts { get; set; }
        public DbSet<StokProduct> StokProducts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<InventoryList> InventoryLists { get; set; }
        public DbSet<CancelOrder> CancelOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StorageProduct>(entity =>
            {
                entity.HasOne(s => s.Brand)
                      .WithMany(b => b.StorageProducts)
                      .HasForeignKey(s => s.BrandId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Category)
                      .WithMany(c => c.StorageProducts)
                      .HasForeignKey(s => s.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StokProduct>(entity =>
            {
                entity.HasOne(s => s.Brand)
                      .WithMany(b => b.StokProducts)
                      .HasForeignKey(s => s.BrandId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Category)
                      .WithMany(c => c.StokProducts)
                      .HasForeignKey(s => s.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Users)
                      .WithMany(u => u.StokProducts)
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasOne(c => c.Users)
                      .WithMany(u => u.Cards)
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InventoryList>(entity =>
            {
                entity.HasOne(i => i.Brand)
                      .WithMany(b => b.InventoryLists)
                      .HasForeignKey(i => i.BrandId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(i => i.Category)
                      .WithMany(c => c.InventoryLists)
                      .HasForeignKey(i => i.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=localhost;" +
                                      "Database=ShopMarket;" +
                                      "Username=postgres;" +
                                      "Password=1234;Port=5432;";

            optionsBuilder.UseNpgsql(connectionString);
        }*/
    }
}
