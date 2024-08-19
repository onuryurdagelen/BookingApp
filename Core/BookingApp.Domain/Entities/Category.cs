using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Entities
{
	public class Category:BaseEntity
	{
        public string Name { get; set; }

        #region navigation
        public ICollection<Product> Foods { get; set; } = new List<Product>();
        #endregion
    }
}
