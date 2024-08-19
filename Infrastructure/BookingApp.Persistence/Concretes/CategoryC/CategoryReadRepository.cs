using BookingApp.Application.Abstracts.CategoryA;
using BookingApp.Domain.Entities;
using BookingApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistence.Concretes.CategoryC
{
	public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
	{
		public CategoryReadRepository(AppDbContext context) : base(context)
		{
		}
	}
}
