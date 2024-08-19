using System.ComponentModel.DataAnnotations;

namespace BookingApp.UI.Areas.Dashboard.Models.Category
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
    }
}
