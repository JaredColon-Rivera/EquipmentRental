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
        public async Task<List<Equipment>> GetAllEquipmentsAsync()
        {
           return await dbContext.EquipmentRentals.ToListAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(Guid id)
        {
            return await dbContext.EquipmentRentals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Equipment> CreateEquipmentAsync(Equipment equipment)
        {
            await dbContext.EquipmentRentals.AddAsync(equipment);
            await dbContext.SaveChangesAsync();
            return equipment;
        }

        public async Task<Equipment?> UpdateEquipmentAsync(Guid id, Equipment equipment)
        {
            var existingEquipment = await GetEquipmentByIdAsync(id);

            if (existingEquipment == null) return null;

            existingEquipment.Name = equipment.Name;
            existingEquipment.Description = equipment.Description;
            existingEquipment.Price = equipment.Price;
            existingEquipment.Rented = equipment.Rented;
            existingEquipment.Customer = equipment.Customer;
            existingEquipment.Location = equipment.Location;

            await dbContext.SaveChangesAsync();
            return existingEquipment;
        }

        public async Task<Equipment?> DeleteEquipmentAsync(Guid id)
        {
            var existingEquipment = await GetEquipmentByIdAsync(id);

            if (existingEquipment == null) return null;

            dbContext.EquipmentRentals.Remove(existingEquipment);
            await dbContext.SaveChangesAsync();
            return existingEquipment;
        }

     

     
    }
}
