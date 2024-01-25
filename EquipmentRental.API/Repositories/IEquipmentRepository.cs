using EquipmentRental.API.Models.Domain;

namespace EquipmentRental.API.Repositories
{
    public interface IEquipmentRepository
    {
        Task<List<Equipment>> GetAllEquipmentRentalsAsync();
        Task<Equipment?> GetEquipmentRentalByIdAsync(Guid id);
        Task<Equipment> CreateEquipmentRentalAsync(Equipment equipment);
        Task<Equipment?> UpdateEquipmentRentalAsync(Guid id, Equipment equipment);
        Task<Equipment?> DeleteEquipmentRentalAsync(Guid id);
    }
}
