using BookingApp.Application.Commands.CategoryC.Create;
using BookingApp.Application.Commands.CategoryC.Delete;
using BookingApp.Application.Commands.CategoryC.Update;
using BookingApp.Application.Queries.CategoryQ.GetAll;
using BookingApp.Application.Queries.CategoryQ.GetById;
using BookingApp.UI.Areas.Dashboard.Models.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.UI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
	public class CategoryController : Controller
	{
		private readonly IMediator mediator;

		public CategoryController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpGet]
		public  IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateCategoryViewModel model)
		{
			var request = new CreateCategoryCommandRequest();
			request.Name = model.Name;
			var response = await mediator.Send(request);

			if (response.Success)
				return RedirectToAction(nameof(Index));

			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Update(string id)
		{
			var response = await mediator.Send(new GetByIdCategoryQueryRequest { Id = id });
			var model = new CategoryUpdateViewModel
			{
				Id = response.Category.Id.ToString(),
				Name = response.Category.Name,
			};
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Update(CategoryUpdateViewModel model)
		{
			var response = await mediator.Send(new UpdateCategoryCommandRequest { Id = model.Id, Name = model.Name });

			if (response.Success)
				return RedirectToAction(nameof(Index));

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			var response = await mediator.Send(new DeleteCategoryCommandRequest { Id = id });

			if(response.Success)
				return RedirectToAction(nameof(Index));

			return View();
		}

		public async Task<IActionResult> Index()
		{
            var response = await mediator.Send(new GetAllCategoryQueryRequest());

            var result = response.Categories.ToList().Select(x => new CategoryViewModel {Id = x.Id.ToString(), IsActive = x.IsActive, IsDeleted = x.IsDeleted, Name = x.Name, UpdatedDate = x.UpdatedDate }).ToList();

            return View(result);
		}
	}
}
