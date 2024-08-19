namespace BookingApp.UI.Areas.Dashboard.Models.Category
{
    public class CategoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
