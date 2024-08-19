using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Entities
{
	public class Product:BaseEntity
	{
        public string Name { get; set; }
        public string? ImageLink { get; set; }
        public string IngredientsText { get; set; }
        public decimal Price { get; set; }

        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
