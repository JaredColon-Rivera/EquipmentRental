using EquipmentRental.UI.Models;
using EquipmentRental.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
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

                var httpResponseMessage = await client.GetAsync("https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/customers");

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
                RequestUri = new Uri("https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/customers"),
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

        [HttpGet]
        public async Task<IActionResult> EditCustomer(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<CustomerDTO>($"https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/customers/{id}");

            if (response is not null) return View(response);

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(CustomerDTO request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/customers/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<CustomerDTO>();

            if (response is not null) return RedirectToAction("Index", "Customers");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(CustomerDTO request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/customers/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Customers");
            }
            catch(Exception ex)
            {
                // Console log
            }

            return View("EditCustomer");
           

        }
    }

       
}
