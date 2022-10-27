using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet_1._0.Application.DTOs.Validations
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.CodErp)
                .NotEmpty()
                .NotNull()
                .WithMessage("CodErp deve ser informado!");
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name deve ser informado!");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price deve ser informado");

        }
    }
}
