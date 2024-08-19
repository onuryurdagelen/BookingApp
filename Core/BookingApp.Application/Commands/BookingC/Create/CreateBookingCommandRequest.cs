using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.BookingC.Create
{
    public class CreateBookingCommandRequest:IRequest<CreateBookingCommandResponse>
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public UInt16 GuestCount { get; set; }
    }
}
