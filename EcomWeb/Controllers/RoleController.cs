using Microsoft.AspNetCore.Mvc;

namespace EcomWeb.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
