using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Entities
{
	public class Table : BaseEntity
	{
        public string Title { get; set; }
        public Guid? BookingId { get; set; }
        public Booking? Booking { get; set; }

    }
}
