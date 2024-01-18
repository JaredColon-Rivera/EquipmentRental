using EquipmentRental.API.Models.Domain;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace EquipmentRental.API.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer?> UpdateCustomerAsync(Guid id, Customer customer);
        Task<Customer?> DeleteCustomerAsync(Guid id);
    }
}
