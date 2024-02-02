using EquipmentRental.API.Models.Domain;

namespace EquipmentRental.API.Models.DTO
{
    public class EquipmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Boolean Rented { get; set; }

        public Guid CustomerId { get; set; }
        public Guid LocationId { get; set; }

        public new CustomerDTO? Customer { get; set; }
        public new LocationDTO Location { get; set; }
    }
}
