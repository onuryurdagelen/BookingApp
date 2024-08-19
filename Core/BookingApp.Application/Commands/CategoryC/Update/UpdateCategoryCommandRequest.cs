using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.CategoryC.Update
{
	public class UpdateCategoryCommandRequest:IRequest<UpdateCategoryCommandResponse>
	{
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
