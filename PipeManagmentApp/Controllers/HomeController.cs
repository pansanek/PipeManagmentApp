using Microsoft.AspNetCore.Mvc;

namespace PipeManagmentApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
