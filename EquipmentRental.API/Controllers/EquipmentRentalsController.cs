using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.API.Controllers
{
    public class EquipmentRentalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
