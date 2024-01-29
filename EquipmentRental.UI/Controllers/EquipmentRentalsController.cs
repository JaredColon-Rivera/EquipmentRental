using EquipmentRental.UI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.UI.Controllers
{
    public class EquipmentRentalsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public EquipmentRentalsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {

            List<EquipmentDTO> response = new List<EquipmentDTO>();

            try
            {
                // Get all equipment from web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7099/api/equipmentrentals");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<EquipmentDTO>>());

            }
            catch(Exception e)
            {
                // Log the exception
            } 

            return View(response);
        }
    }
}
