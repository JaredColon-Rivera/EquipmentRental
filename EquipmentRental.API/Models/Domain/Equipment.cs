namespace EquipmentRental.API.Models.Domain
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get;set; }
        public Boolean Rented { get; set; }

        public Guid CustomerId { get; set; }
        public Guid LocationId { get; set; }

        public Customer Customer { get; set; }
        public Location Location { get; set; }
    }
}
