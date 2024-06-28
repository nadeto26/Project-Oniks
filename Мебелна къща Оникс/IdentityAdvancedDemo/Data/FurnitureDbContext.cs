using IdentityAdvancedDemo.Data.IdentityModels;
using IdentityAdvancedDemo.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
 

namespace IdentityAdvancedDemo.Data
{
    public class FurnitureDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public FurnitureDbContext(DbContextOptions<FurnitureDbContext> options)
            : base(options)
        {
        }

        public DbSet<Furnitures> Furnitures { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<FurnitureBuier> FurnitureBuyers { get; set; } = null!;
        public DbSet<Discount> Discounts { get; set; } = null!;
        public DbSet<Accessories> Accessories { get; set; } = null!;
        public DbSet<DiscountBuyer> DiscountBuyers { get; set; } = null!;
        public DbSet<AccessoryBuyer> AccesoaryBuyers { get; set; } = null!;
        public DbSet<Messages> Messages { get; set; } = null!;
        public DbSet<DeliveryDetails> DeliveryDetails { get; set; } = null!;
        public DbSet<Orders> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<FurnitureBuier>()
               .HasKey(e => new { e.BuyerId, e.FurnitureId });

            builder.Entity<AccessoryBuyer>()
              .HasKey(e => new { e.BuyerId, e.AccesoaryId });

            builder.Entity<DiscountBuyer>()
              .HasKey(e => new { e.BuyerId, e.DiscountId });

            builder.Entity<FurnitureBuier>()
           .HasOne(fb => fb.Buyer)
           .WithMany()
           .HasForeignKey(fb => fb.BuyerId);

            builder.Entity<FurnitureBuier>()
           .HasOne(fb => fb.Furnitures)
           .WithMany()
           .HasForeignKey(fb => fb.FurnitureId)
           .OnDelete(DeleteBehavior.Cascade);


            builder
             .Entity<Category>()
             .HasData(new Category()
             {
                 Id = 1,
                 Name = "Кухня"
             },
             new Category()
             {
                 Id = 2,
                 Name = "Спалня"
             },
             new Category()
             {
                 Id = 3,
                 Name = "Детска стая"
             },
             new Category()
             {
                 Id = 4,
                 Name = "Маси"
             },
             new Category()
             {
                 Id = 5,
                 Name = "Столове"
             },
             new Category()
             {
                 Id = 6,
                 Name = "Гардероби"
             },
              new Category()
              {
                  Id = 7,
                  Name = "Офис"
              },
              new Category()
              {
                  Id = 8,
                  Name = "Други"
              });

            base.OnModelCreating(builder);
        }
    }
}