using EquipmentRental.API.Models.Domain;

namespace EquipmentRental.API.Models.DTO
{
    public class LocationDTO
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string StoreName { get; set; }
    }
}
