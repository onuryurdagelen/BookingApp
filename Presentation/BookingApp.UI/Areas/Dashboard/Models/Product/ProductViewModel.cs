namespace BookingApp.UI.Areas.Dashboard.Models.Product
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? ImageLink { get; set; }
        public string IngredientsText { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
