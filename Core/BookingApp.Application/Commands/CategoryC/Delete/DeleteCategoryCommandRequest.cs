using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.CategoryC.Delete
{
	public class DeleteCategoryCommandRequest:IRequest<DeleteCategoryCommandResponse>
	{
        public string Id { get; set; }
    }
}
