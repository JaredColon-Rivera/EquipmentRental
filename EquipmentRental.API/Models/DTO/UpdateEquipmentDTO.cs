namespace EquipmentRental.API.Models.DTO
{
    public class UpdateEquipmentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Boolean Rented { get; set; }

        public Guid? CustomerId { get; set; }
        public Guid LocationId { get; set; }
    }
}
