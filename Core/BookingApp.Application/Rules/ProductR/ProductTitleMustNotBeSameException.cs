using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.Bases;

namespace BookingApp.Application.Rules.Product
{
    public class ProductTitleMustNotBeSameException:BaseException
	{
        public ProductTitleMustNotBeSameException() : base("Product title must be unique!") { }
    }
}
