using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.ProductC.Create
{
	public class CreateProductCommandValidator:AbstractValidator<CreateProductCommandRequest>
	{

        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithName("Başlık");

            RuleFor(x => x.IngredientsText)
                .NotEmpty()
                .WithName("Malzemeler");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithName("Fiyat");

        }

    }
}
