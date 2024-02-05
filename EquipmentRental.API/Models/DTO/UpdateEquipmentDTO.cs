using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.API.Models.DTO
{
    public class UpdateEquipmentDTO
    {
        // [Required]
       // [MaxLength(50, ErrorMessage = "Name of equipment can only be a maximum of 50 characters")]
        public string Name { get; set; }
       
        //[Required]
        //[MaxLength(150, ErrorMessage = "Name of equipment can only be a maximum of 150 characters")]
        public string Description { get; set; }
        
        //[Required]
        //[Range(1, 1000, ErrorMessage = "Rental price has be a value between 1 and 1000")]
        public double Price { get; set; }
       
        //[Required]
        public bool Rented { get; set; }

        public Guid? CustomerId { get; set; }
        public Guid LocationId { get; set; }

    }
}
