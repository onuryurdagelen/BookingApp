using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.UI.Areas.Dashboard.Models.Product
{
	public class UpdateProductViewModel
	{
        public string Id { get; set; }
        [Display(Name = "Name")]
		public string Name { get; set; }
		[Display(Name = "Image Link")]
		public string? ImageLink { get; set; }
		[Display(Name = "Ingredients")]
		public string IngredientsText { get; set; }
		public decimal Price { get; set; }
		public string CategoryId { get; set; }
		[Display(Name = "Categories")]
		public List<SelectListItem>? Categories { get; set; }
	}
}
