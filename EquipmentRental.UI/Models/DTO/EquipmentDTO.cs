namespace EquipmentRental.UI.Models.DTOs
{
    public class EquipmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Rented { get; set; }

    }
}
