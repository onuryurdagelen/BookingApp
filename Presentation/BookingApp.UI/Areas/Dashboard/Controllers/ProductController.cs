using BookingApp.Application.Commands.CategoryC.Delete;
using BookingApp.Application.Commands.ProductC.Create;
using BookingApp.Application.Commands.ProductC.Delete;
using BookingApp.Application.Commands.ProductC.Update;
using BookingApp.Application.Queries.CategoryQ.GetAll;
using BookingApp.Application.Queries.ProductQ;
using BookingApp.Application.Queries.ProductQ.GetAll;
using BookingApp.Application.Queries.ProductQ.GetById;
using BookingApp.UI.Areas.Dashboard.Models.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingApp.UI.Areas.Dashboard.Controllers
{
	[Area("Dashboard")]
	public class ProductController : Controller
	{
		private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
		{
			var response = await _mediator.Send(new GetAllProductQueryRequest());
			var products = response.Products.Select(x => new ProductViewModel 
			{ 
				Id = x.Id.ToString(),
				ImageLink = x.ImageLink,
				IngredientsText = x.IngredientsText,
				IsActive = x.IsActive,
				Name = x.Name,
				Price = x.Price
			}).ToList();
			return View(products);
		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var response = await _mediator.Send(new GetAllCategoryQueryRequest());
			
			var model = new CreateProductViewModel();
			model.Categories = response.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
			
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductViewModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _mediator.Send(new CreateProductCommandRequest
				{
					CategoryId = model.CategoryId,
					ImageLink = model.ImageLink,
					IngredientsText = model.IngredientsText,
					Name = model.Name,
					Price = model.Price
				});

				if (response.Success)
					return RedirectToAction(nameof(Index));

				return View(model);
			}
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Update(string id)
		{
			var response = await _mediator.Send(new GetByIdProductQueryRequest() { Id = id });

			var model = new UpdateProductViewModel
			{
				CategoryId = response.Product.CategoryId!.ToString(),
				Id = response.Product.Id.ToString(),
				Price = response.Product.Price,
				ImageLink = response.Product.ImageLink,
				IngredientsText = response.Product.IngredientsText,
				Name = response.Product.Name,
                Categories = response.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
            };
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Update(UpdateProductViewModel model)
		{
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateProductCommandRequest
                {
					Id = model.Id,
                    CategoryId = model.CategoryId,
                    ImageLink = model.ImageLink,
                    IngredientsText = model.IngredientsText,
                    Name = model.Name,
                    Price = model.Price
                });

                if (response.Success)
                    return RedirectToAction(nameof(Index));

                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteProductCommandRequest { Id = id });

            if (response.Success)
                return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
