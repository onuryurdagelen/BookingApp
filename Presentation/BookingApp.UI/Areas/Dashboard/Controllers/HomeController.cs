using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookingApp.UI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
		public HomeController()
		{
		}

		public IActionResult Index()
        {
			//await _hubContext.Clients.All.SendAsync("ReceiveMessage", "User", "Hello from the controller!");
			return View();
        }
    }
}
