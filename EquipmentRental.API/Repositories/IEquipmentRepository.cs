using EquipmentRental.API.Models.Domain;

namespace EquipmentRental.API.Repositories
{
    public interface IEquipmentRepository
    {
        Task<List<Equipment>> GetAllEquipmentsAsync();
        Task<Equipment?> GetEquipmentByIdAsync(Guid id);
        Task<Equipment> CreateEquipmentAsync(Equipment equipment);
        Task<Equipment?> UpdateEquipmentAsync(Guid id, Equipment equipment);
        Task<Equipment?> DeleteEquipmentAsync(Guid id);
    }
}
