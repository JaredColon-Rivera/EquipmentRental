using EquipmentRental.UI.Models;
using EquipmentRental.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure;
using System.Collections.Generic;

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

                var httpResponseMessage = await client.GetAsync("https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/equipmentrentals");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<EquipmentDTO>>());

            }
            catch(Exception e)
            {
                // Log the exception
            } 

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> AddEquipmentRental()
        {
            var vm = new AddEquipmentRentalViewModel();
            vm.Locations = new List<SelectListItem>
            {
                new SelectListItem{Value="58DBAAC0-95E6-489B-9114-596AC3CDE3B7", Text="Milwaukee"},
                new SelectListItem{Value="B5B23282-EED7-43DB-B998-8C9093A4AC3E", Text="Kenosha"},
                new SelectListItem{Value="6C2D872C-BA68-49C0-930B-ACD51B7D1CBC", Text="Racine"},
            };

            vm.RentedOptions = new List<SelectListItem>
            {
                new SelectListItem{Value="true", Text="true"},
                new SelectListItem{Value="false", Text="false"},
            };
            
            List <CustomerDTO> customers = new List<CustomerDTO>();

            try
            {
                // Get all equipment from web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/customers");

                httpResponseMessage.EnsureSuccessStatusCode();

                customers.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<CustomerDTO>>());

            }
            catch (Exception e)
            {
                // Log the exception
            }

            vm.Customers = new List<SelectListItem>();

            for (var i = 0; i < customers.Count(); i++)
            {
                vm.Customers.Add(new SelectListItem { Value = customers[i].Id.ToString(), Text = customers[i].FirstName + " " + customers[i].LastName });
            }


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddEquipmentRental(AddEquipmentRentalViewModel model)
        {

            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/equipmentrentals"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EquipmentDTO>();

            if (response is not null)
            {
                return RedirectToAction("Index", "EquipmentRentals");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditEquipmentRental(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<EquipmentDTO>($"https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/equipmentrentals/{id}");

            var vm = response;
            vm.Locations = new List<SelectListItem>
            {
                new SelectListItem{Value="58DBAAC0-95E6-489B-9114-596AC3CDE3B7", Text="Milwaukee"},
                new SelectListItem{Value="B5B23282-EED7-43DB-B998-8C9093A4AC3E", Text="Kenosha"},
                new SelectListItem{Value="6C2D872C-BA68-49C0-930B-ACD51B7D1CBC", Text="Racine"},
            };

            vm.RentedOptions = new List<SelectListItem>
            {
                new SelectListItem{Value="true", Text="true"},
                new SelectListItem{Value="false", Text="false"},
            };

            List<CustomerDTO> customers = new List<CustomerDTO>();

            try
            {
                var httpResponseMessage = await client.GetAsync("https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/customers");

                httpResponseMessage.EnsureSuccessStatusCode();

                customers.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<CustomerDTO>>());

            }
            catch (Exception e)
            {
                // Log the exception
            }

            vm.Customers = new List<SelectListItem>();

            for (var i = 0; i < customers.Count(); i++)
            {
                vm.Customers.Add(new SelectListItem { Value = customers[i].Id.ToString(), Text = customers[i].FirstName + " " + customers[i].LastName });
            }

            if (vm is not null) return View(vm);

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> EditEquipmentRental(EquipmentDTO request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/equipmentrentals/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<EquipmentDTO>();

            if (response is not null) return RedirectToAction("Index", "EquipmentRentals");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> DeleteEquipmentRental(EquipmentDTO request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://app-equipmentrental-eastus-dev-001.azurewebsites.net/api/equipmentrentals/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "EquipmentRentals");
            }
            catch (Exception ex)
            {
                // Console log
            }

            return View("EditEquipmentRental");


        }
    }
}
