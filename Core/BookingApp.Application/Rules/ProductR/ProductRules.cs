using BookingApp.Application.Rules.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Rules.ProductR
{
	public class ProductRules:BaseRules
	{
		public Task ProductTitleMustNotBeSame(string requestTitle, IList<BookingApp.Domain.Entities.Product> products)
		{
			if (products.Any(x => string.Equals(x.Name,requestTitle,StringComparison.CurrentCultureIgnoreCase))) throw new ProductTitleMustNotBeSameException();

			return Task.CompletedTask;

		}
	}
}
