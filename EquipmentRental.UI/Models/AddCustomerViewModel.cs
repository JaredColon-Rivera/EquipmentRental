using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.UI.Models
{
    public class AddCustomerViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
