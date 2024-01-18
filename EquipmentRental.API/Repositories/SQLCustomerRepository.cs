using EquipmentRental.API.Data;
using EquipmentRental.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRental.API.Repositories
{
    public class SQLCustomerRepository : ICustomerRepository
    {

        private readonly EquipmentRentalsDbContext dbContext;

        public SQLCustomerRepository(EquipmentRentalsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateCustomerAsync(Guid id, Customer customer)
        {
            var existingCustomer = await GetCustomerByIdAsync(id);

            if (existingCustomer == null) return null;

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            await dbContext.SaveChangesAsync();
            return existingCustomer;
        }

        public async Task<Customer?> DeleteCustomerAsync(Guid id)
        {
            var existingCustomer = await GetCustomerByIdAsync(id);

            if(existingCustomer == null) return null;

            dbContext.Customers.Remove(existingCustomer);
            await dbContext.SaveChangesAsync();
            return existingCustomer;
        }
    }
}
