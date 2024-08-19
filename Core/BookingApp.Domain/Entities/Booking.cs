using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Entities
{
    public class Booking: BaseEntity
    {
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime DateTime { get; set; }
        public string Time { get; set; }
        public UInt16 GuestCount { get; set; }
        public BookingStatus Status { get; set; } //Rezervasyon durumu

    }
    public enum BookingStatus
    {
        Pending, //Rezervasyonun onayı bekleniyor
        Approved, //Rezervasyon onaylandı
        Cancelled //Rezervasyon iptal edildi

    }
}
