using EquipmentRental.UI.Models;
using EquipmentRental.UI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace EquipmentRental.UI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {

            List<CustomerDTO> response = new List<CustomerDTO>();

            try
            {
                // Get all equipment from web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7099/api/customers");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<CustomerDTO>>());

            }
            catch (Exception e)
            {
                // Log the exception
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddCustomerViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7099/api/customers"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<CustomerDTO>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Customers");
            }

            return View();
        }
    }

       
}
