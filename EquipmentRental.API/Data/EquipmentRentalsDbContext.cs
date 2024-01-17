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
    }
}
