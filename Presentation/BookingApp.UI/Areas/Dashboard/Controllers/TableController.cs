using Microsoft.AspNetCore.Mvc;

namespace BookingApp.UI.Areas.Dashboard.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
