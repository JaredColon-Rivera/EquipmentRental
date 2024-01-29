using AutoMapper;
using EquipmentRental.API.CustomActionFilters;
using EquipmentRental.API.Data;
using EquipmentRental.API.Models.Domain;
using EquipmentRental.API.Models.DTO;
using EquipmentRental.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.API.Controllers
{
    // localhost:1234/api/equipmentrentals
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentRentalsController : Controller
    { 
        private readonly EquipmentRentalsDbContext dbContext;
        private readonly IEquipmentRepository equipmentRepository;
        private readonly IMapper mapper;

        public EquipmentRentalsController(
            EquipmentRentalsDbContext dbContext,
            IEquipmentRepository equipmentRepository,
            IMapper mapper
        )
        {
            this.dbContext = dbContext;
            this.equipmentRepository = equipmentRepository;
            this.mapper = mapper;
        }

        // GET ALL Equipment rentals
        // localhost:1234/api/equipmentrentals
        [HttpGet]
        public async Task<IActionResult> GetAllEquipmentRentals()
        {
            // Get Data from Database - Domain Models
            var equipmentsDomain = await equipmentRepository.GetAllEquipmentRentalsAsync();

            // Map Domain Models to DTOs
            var equipmentsDTO = mapper.Map<List<EquipmentDTO>>(equipmentsDomain);

            // Return DTOs
            return Ok(equipmentsDTO);
        }

        // GET Equipment BY ID
        // localhost:1234/api/equipmentrentals/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEquipmentRentalById([FromRoute] Guid id)
        {
            // Get Equipment Domain
            var equipmentDomain = await equipmentRepository.GetEquipmentRentalByIdAsync(id);

            if (equipmentDomain == null) return NotFound();

            // Map/Convert to Domain to DTO
            var equipmentDTO = mapper.Map<EquipmentDTO>(equipmentDomain);

            // Return Equipment DTO
            return Ok(equipmentDTO);
        }

        // POST to Create new Equipment
        // POST: https//localhost:1234/api/equipmentrentals
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateEquipmentRental([FromBody] AddEquipmentDTO addEquipmentDTO)
        {
            // Map or convert DTO to domain model
            var equipmentDomainModel = mapper.Map<Equipment>(addEquipmentDTO);

            // Use domain model to create Equipment
            equipmentDomainModel = await equipmentRepository.CreateEquipmentRentalAsync(equipmentDomainModel);

            // Map domain back to dto
            var equipmentDTO = mapper.Map<EquipmentDTO>(equipmentDomainModel);

            return Ok(equipmentDTO);
        }

        // Update Equipment
        // PUT: https://localhost:1234/api/equipmentrentals/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateEquipmentRental([FromRoute] Guid id, [FromBody] UpdateEquipmentDTO updateEquipmentDTO)
        {
            // Map DTO to domain model
            var equipmentDomainModel = mapper.Map<Equipment>(updateEquipmentDTO);

            // Check if Equipment exists
            equipmentDomainModel = await equipmentRepository.UpdateEquipmentRentalAsync(id, equipmentDomainModel);

            if (equipmentDomainModel == null) return NotFound();

            // Map domain back to dto
            var equipmentDTO = mapper.Map<EquipmentDTO>(equipmentDomainModel);

            return Ok(equipmentDTO);


        }

        // Delete Equipment
        // DELETE: https://localhost:1234/api/equipmentrentals/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEquipmentRental([FromRoute] Guid id)
        {
            // Check if Equipment exists 
            var equipmentDomainModel = await equipmentRepository.DeleteEquipmentRentalAsync(id);

            if (equipmentDomainModel == null) return NotFound();

            // map domain back to dto
            var equipmentDTO = mapper.Map<EquipmentDTO>(equipmentDomainModel);

            return Ok(equipmentDTO);


        }
        
    }
}
