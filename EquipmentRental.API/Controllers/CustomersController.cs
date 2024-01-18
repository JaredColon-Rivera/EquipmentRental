using AutoMapper;
using EquipmentRental.API.Data;
using EquipmentRental.API.Models.Domain;
using EquipmentRental.API.Models.DTO;
using EquipmentRental.API.Repositories;
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
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomersController(
            EquipmentRentalsDbContext dbContext, 
            ICustomerRepository customerRepository,
            IMapper mapper
        )
        {
            this.dbContext = dbContext;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        // GET ALL CUSTOMERS
        // localhost:1234/api/customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            // Get Data from Database - Domain Models
            var customersDomain = await customerRepository.GetAllCustomersAsync();

            // Map Domain Models to DTOs
            var customersDTO = mapper.Map<List<CustomerDTO>>(customersDomain);

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
            var customerDomain = await customerRepository.GetCustomerByIdAsync(id);

            if (customerDomain == null) return NotFound();

            // Map/Convert to Domain to DTO
            var customerDTO = mapper.Map<CustomerDTO>(customerDomain);

            // Return Customer DTO
            return Ok(customerDTO);
        }

        // POST to Create new Customer
        // POST: https//localhost:1234/api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] AddCustomerDTO addCustomerDTO)
        {
            // Map or convert DTO to domain model
            var customerDomainModel = mapper.Map<Customer>(addCustomerDTO);

            // Use domain model to create customer
            customerDomainModel = await customerRepository.CreateCustomerAsync(customerDomainModel);

            // Map domain back to dto
            var customerDTO = mapper.Map<CustomerDTO>(customerDomainModel);

            return CreatedAtAction(nameof(GetCustomerById), new {id = customerDTO.Id}, customerDTO);
        }

        // Update Customer
        // PUT: https://localhost:1234/api/customers/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            // Map DTO to domain model
            var customerDomainModel = mapper.Map<Customer>(updateCustomerDTO);
                
            // Check if customer exists
            customerDomainModel = await customerRepository.UpdateCustomerAsync(id, customerDomainModel);

            if (customerDomainModel == null) return NotFound();

            // Map domain back to dto
            var customerDTO = mapper.Map<CustomerDTO>(customerDomainModel);

            return Ok(customerDTO);


        }

        // Delete Region
        // DELETE: https://localhost:1234/api/customers/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            // Check if customer exists 
            var customerDomainModel = await customerRepository.DeleteCustomerAsync(id);

            if (customerDomainModel == null) return NotFound();

            // map domain back to dto
            var customerDTO = mapper.Map<CustomerDTO>(customerDomainModel);

            return Ok(customerDTO);


        }
    }
}
