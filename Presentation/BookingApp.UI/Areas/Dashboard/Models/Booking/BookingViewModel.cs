using BookingApp.Domain.Entities;

namespace BookingApp.UI.Areas.Dashboard.Models.Booking
{
    public class BookingViewModel
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime DateTime { get; set; }
        public string Time { get; set; }
        public UInt16 GuestCount { get; set; }
        public BookingStatus Status { get; set; }
    }
}
