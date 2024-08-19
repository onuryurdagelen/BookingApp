using BookingApp.Application.Abstracts.BookingA;
using BookingApp.Application.Abstracts.CategoryA;
using BookingApp.Application.Abstracts.ProductA;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.UI.Hubs
{
	public class DashboardHub:Hub
	{
		private readonly ICategoryReadRepository _categoryReadRepository;
		private readonly IProductReadRepository _productReadRepository;
		private readonly IBookingReadRepository _bookingReadRepository;

		public DashboardHub(ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository, IBookingReadRepository bookingReadRepository)
		{
			_categoryReadRepository = categoryReadRepository;
			_productReadRepository = productReadRepository;
			_bookingReadRepository = bookingReadRepository;
		}

		public async Task SendCurrentCategoryCount()
		{

			int count = await _categoryReadRepository.GetAll().CountAsync();

			await Clients.All.SendAsync("GetCurrentCategoryCount", count);
		}
		public async Task SendCurrentProductCount()
		{
			int count = await _productReadRepository.GetAll().CountAsync();

			await Clients.All.SendAsync("GetCurrentProductCount", count);
		}
		public async Task SendCurrentBookingCount()
		{
			int count = await _bookingReadRepository.GetAll().CountAsync();

			await Clients.All.SendAsync("GetCurrentBookingCount", count);
		}
	}
}
