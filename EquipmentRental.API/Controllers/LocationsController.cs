using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.API.Controllers
{
    public class LocationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
