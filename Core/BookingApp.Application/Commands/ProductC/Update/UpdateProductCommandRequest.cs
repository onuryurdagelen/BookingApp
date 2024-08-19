using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.ProductC.Update
{
	public class UpdateProductCommandRequest:IRequest<UpdateProductCommandResponse>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string? ImageLink { get; set; }
		public string IngredientsText { get; set; }
		public decimal Price { get; set; }
		public string CategoryId { get; set; }
	}
}
