using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectroShop.Models
{
    public class ShopDbContext : IdentityDbContext<User> // inherits from DbContext.
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
        {    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>().HasOne(d => (Product)d.RelatedProduct).WithMany(c => c.Stores).HasForeignKey(c => c.ProductId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProperty> CategoryProperties { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Guarantee> Guarantees { get; set; }
        public DbSet<ImageGallery> ImageGalleries { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PollResult> PollResults { get; set; }
        public DbSet<PollTitle> PollTitles { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Portal> Portals { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyOption> PropertyOptions { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreOrder> StoreOrders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagUsage> TagUsages { get; set; }
        public DbSet<User> User { get; set; }
    }    

}
