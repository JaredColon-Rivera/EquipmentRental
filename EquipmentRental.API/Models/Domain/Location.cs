namespace EquipmentRental.API.Models.Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string StoreName { get; set; }
    }
}
