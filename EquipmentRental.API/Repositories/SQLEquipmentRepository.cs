using EquipmentRental.API.Data;
using EquipmentRental.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRental.API.Repositories
{
    public class SQLEquipmentRepository : IEquipmentRepository
    {

        private readonly EquipmentRentalsDbContext dbContext;

        public SQLEquipmentRepository(EquipmentRentalsDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Equipment>> GetAllEquipmentRentalsAsync()
        {
           return await dbContext.EquipmentRentals.Include("Customer").Include("Location").ToListAsync();
        }

        public async Task<Equipment?> GetEquipmentRentalByIdAsync(Guid id)
        {
            return await dbContext.EquipmentRentals.Include("Customer").Include("Location").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Equipment> CreateEquipmentRentalAsync(Equipment equipment)
        {
            await dbContext.EquipmentRentals.AddAsync(equipment);
            await dbContext.SaveChangesAsync();
            return equipment;
        }

        public async Task<Equipment?> UpdateEquipmentRentalAsync(Guid id, Equipment equipment)
        {
            var existingEquipment = await GetEquipmentRentalByIdAsync(id);

            if (existingEquipment == null) return null;

            existingEquipment.Name = equipment.Name;
            existingEquipment.Description = equipment.Description;
            existingEquipment.Price = equipment.Price;
            existingEquipment.Rented = equipment.Rented;
            existingEquipment.CustomerId = equipment.CustomerId;
            existingEquipment.LocationId = equipment.LocationId;

            await dbContext.SaveChangesAsync();
            return existingEquipment;
        }

        public async Task<Equipment?> DeleteEquipmentRentalAsync(Guid id)
        {
            var existingEquipment = await GetEquipmentRentalByIdAsync(id);

            if (existingEquipment == null) return null;

            dbContext.EquipmentRentals.Remove(existingEquipment);
            await dbContext.SaveChangesAsync();
            return existingEquipment;
        }

     

     
    }
}
