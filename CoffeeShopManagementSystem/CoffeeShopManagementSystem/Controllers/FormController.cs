using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopManagementSystem.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Employee()
        {
            return View();
        }
    }
}
