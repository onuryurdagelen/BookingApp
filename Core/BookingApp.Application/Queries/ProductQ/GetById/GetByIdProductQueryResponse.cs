using BookingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.ProductQ.GetById
{
	public class GetByIdProductQueryResponse
	{
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
