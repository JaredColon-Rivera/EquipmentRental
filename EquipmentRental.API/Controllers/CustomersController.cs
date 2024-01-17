using EquipmentRental.API.Data;
using EquipmentRental.API.Models.Domain;
using EquipmentRental.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EquipmentRental.API.Controllers
{
    // localhost:1234/api/customers
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly EquipmentRentalsDbContext dbContext;

        public CustomersController(EquipmentRentalsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL CUSTOMERS
        // localhost:1234/api/customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            // Get Data from Database - Domain Models
            var customersDomain = await dbContext.Customers.ToListAsync();

            // Map Domain Models to DTOs
            var customersDTO = new List<CustomerDTO>();
            foreach (var customerDomain in customersDomain)
            {
                customersDTO.Add(new CustomerDTO()
                {
                    Id = customerDomain.Id,
                    FirstName = customerDomain.FirstName,
                    LastName = customerDomain.LastName,
                    Email = customerDomain.Email,
                    PhoneNumber = customerDomain.PhoneNumber
                });
            }

            // Return DTOs
            return Ok(customersDTO);
        }

        // GET CUSTOMER BY ID
        // localhost:1234/api/customers/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] Guid id)
        {
            // Get Customer Domain
            var customerDomain = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customerDomain == null) return NotFound();

            // Map/Convert to Domain to DTO
            var customerDTO = new CustomerDTO
            {
                Id = customerDomain.Id,
                FirstName = customerDomain.FirstName,
                LastName = customerDomain.LastName,
                Email = customerDomain.Email,
                PhoneNumber = customerDomain.PhoneNumber
            };

            // Return Customer DTO
            return Ok(customerDTO);
        }

        // POST to Create new Customer
        // POST: https//localhost:1234/api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] AddCustomerDTO addCustomerDTO)
        {
            var customerDomainModel = new Customer
            {
                FirstName = addCustomerDTO.FirstName,
                LastName = addCustomerDTO.LastName,
                Email = addCustomerDTO.Email,
                PhoneNumber = addCustomerDTO.PhoneNumber
            };

            await dbContext.Customers.AddAsync(customerDomainModel);
            await dbContext.SaveChangesAsync();

            // map domain back to dto
            var customerDTO = new CustomerDTO
            {
                Id = customerDomainModel.Id,
                FirstName = customerDomainModel.FirstName,
                LastName = customerDomainModel.LastName,
                Email = customerDomainModel.Email,
                PhoneNumber = customerDomainModel.LastName
            };

            return CreatedAtAction(nameof(GetCustomerById), new {id = customerDomainModel.Id}, customerDTO);
        }

        // Update Customer
        // PUT: https://localhost:1234/api/customers/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            // Check if customer exists
            var customerDomainModel = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customerDomainModel == null) return NotFound();

            // Map DTO to Domain Model
            customerDomainModel.FirstName = updateCustomerDTO.FirstName;
            customerDomainModel.LastName = updateCustomerDTO.LastName;
            customerDomainModel.Email = updateCustomerDTO.Email;
            customerDomainModel.PhoneNumber = updateCustomerDTO.PhoneNumber;

            await dbContext.SaveChangesAsync();

            // map domain back to dto
            var customerDTO = new CustomerDTO
            {
                Id = customerDomainModel.Id,
                FirstName = customerDomainModel.FirstName,
                LastName = customerDomainModel.LastName,
                Email = customerDomainModel.Email,
                PhoneNumber = customerDomainModel.LastName
            };

            return Ok(customerDTO);


        }

        // Delete Region
        // DELETE: https://localhost:1234/api/customers/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            var customerDomainModel  = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customerDomainModel == null) return NotFound();

            // Delete customer
            dbContext.Customers.Remove(customerDomainModel);
            await dbContext.SaveChangesAsync();

            // map domain back to dto
            var customerDTO = new CustomerDTO
            {
                Id = customerDomainModel.Id,
                FirstName = customerDomainModel.FirstName,
                LastName = customerDomainModel.LastName,
                Email = customerDomainModel.Email,
                PhoneNumber = customerDomainModel.LastName
            };

            return Ok(customerDTO);


        }
    }
}
