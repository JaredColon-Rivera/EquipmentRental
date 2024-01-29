using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.API.Models.DTO
{
    public class AddCustomerDTO
    {
        [Required]
        [MaxLength(20, ErrorMessage = "First Name can only be a maximum of 20 characters")]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(20, ErrorMessage = "Last Name can only be a maximum of 20 characters")]
        public string LastName { get; set; }
       
        [Required]
        [EmailAddress]
        public string Email { get; set; }
      
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
