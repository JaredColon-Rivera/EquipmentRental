using AutoMapper;
using EquipmentRental.API.Models.Domain;
using EquipmentRental.API.Models.DTO;

namespace EquipmentRental.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // Customer Mappings
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<AddCustomerDTO, Customer>().ReverseMap();
            CreateMap<UpdateCustomerDTO, Customer>().ReverseMap();

            // Equipment Mappings
            CreateMap<Equipment, EquipmentDTO>().ReverseMap();
            CreateMap<AddEquipmentDTO, Equipment>().ReverseMap();
            CreateMap<UpdateEquipmentDTO, Equipment>().ReverseMap();

            // Location Mappings
            CreateMap<Location, LocationDTO>().ReverseMap();
        }
    }
}
