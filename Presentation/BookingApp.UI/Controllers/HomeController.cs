using BookingApp.Application.Commands.BookingC.Create;
using BookingApp.UI.Hubs;
using BookingApp.UI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace BookingApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<BookingHub> _hubContext;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, IHubContext<BookingHub> hubContext)
        {
            _logger = logger;
            _mediator = mediator;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveBooking", "data");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> SubmitBooking(CreateBookingViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var response = await _mediator.Send(new CreateBookingCommandRequest
                {
                    Date = model.Date,
                    EmailAddress = model.EmailAddress,
                    GuestCount = model.GuestCount,
                    Name = model.Name,
                    Phone = model.Phone,
                    Time = model.Time
                });
                if (response.Success)
                    return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> SendMessage()
        {
            // Example of calling the hub method from a controller
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "User", "Hello from the controller!");

            return View();
        }

    }
}
