using EquipmentRental.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EquipmentRental.UI.Models
{
    public class AddEquipmentRentalViewModel : EquipmentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Rented { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid LocationId { get; set; }

        public List<SelectListItem> Locations { get; set; }
        public List<SelectListItem> RentedOptions { get; set; }
        public List<SelectListItem> Customers { get; set; }

    }
}
