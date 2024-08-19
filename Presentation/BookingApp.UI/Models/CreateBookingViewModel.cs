namespace BookingApp.UI.Models
{
    public class CreateBookingViewModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public UInt16 GuestCount { get; set; }
    }
}
