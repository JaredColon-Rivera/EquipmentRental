namespace EquipmentRental.UI.Models.DTO
{
    public class UpdateEquipmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Rented { get; set; }

        public Guid? CustomerId { get; set; }
        public Guid LocationId { get; set; }

    }
}
