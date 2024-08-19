using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.CategoryQ.GetAll
{
	public class GetAllCategoryQueryRequest:IRequest<GetAllCategoryQueryResponse>
	{
	}
}
