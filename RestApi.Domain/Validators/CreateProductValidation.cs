using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Domain.Model.Validators
{
    public class CreateProductValidator : AbstractValidator<Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(3, 60).WithMessage("O campo {PropertyName} deve ter entre 3 e 60 caracteres s");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que 0");
            
            RuleFor(x => x.Description).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que 0");
        }

        public override ValidationResult Validate(ValidationContext<Product> context)
        {
            var validationResult = base.Validate(context);

            if (!validationResult.IsValid)
            {
                RaiseValidationException(context, validationResult);
            }

            return validationResult;
        }
    }
}
