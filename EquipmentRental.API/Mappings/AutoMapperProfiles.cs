using AutoMapper;
using EquipmentRental.API.Models.Domain;
using EquipmentRental.API.Models.DTO;

namespace EquipmentRental.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<AddCustomerDTO, Customer>().ReverseMap();
            CreateMap<UpdateCustomerDTO, Customer>().ReverseMap();

            CreateMap<Equipment, EquipmentDTO>().ReverseMap();
        }
    }
}
