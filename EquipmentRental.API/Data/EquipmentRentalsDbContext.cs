using EquipmentRental.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRental.API.Data
{
    public class EquipmentRentalsDbContext: DbContext
    {
        public EquipmentRentalsDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {


        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Equipment> EquipmentRentals { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Locations

            // Chicago, Milwaukee, Kenosha

            var locations = new List<Location>()
            {
                new Location()
                {
                    Id=Guid.Parse("6c2d872c-ba68-49c0-930b-acd51b7d1cbc"),
                    StoreName="Mike's Outdoor",
                    City="Chicago",
                    StateCode="IL"
                },
                new Location()
                {
                    Id=Guid.Parse("58dbaac0-95e6-489b-9114-596ac3cde3b7"),
                    StoreName="Alex's Pro shop",
                    City="Milwaukee",
                    StateCode="WI"
                },
                  new Location()
                {
                    Id=Guid.Parse("b5b23282-eed7-43db-b998-8c9093a4ac3e"),
                    StoreName="Bob's Camping Store",
                    City="Kenosha",
                    StateCode="WI"
                }
            };

            //Seed locations to the database
            modelBuilder.Entity<Location>().HasData(locations);

            
        }
    }
}
