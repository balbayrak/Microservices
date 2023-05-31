using FluentValidation;
using Microservices.Product.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Product.Application.Features.Commands.Product.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.createProductDto).NotEmpty().WithMessage(ExceptionMessages.PRODUCT_CREATEDTO_NOT_EMPTY);
            RuleFor(p => p.createProductDto.Name).NotEmpty().WithMessage(ExceptionMessages.PRODUCT_NAME_EMPTY);
            RuleFor(p => p.createProductDto.Code).NotEmpty().WithMessage(ExceptionMessages.PRODUCT_CODE_EMPTY);

        }
    }
}
