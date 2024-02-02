namespace EquipmentRental.UI.Models
{
    public class AddEquipmentRentalViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Rented { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid LocationId { get; set; }
    }
}
