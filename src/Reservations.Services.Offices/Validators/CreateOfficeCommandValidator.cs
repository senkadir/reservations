using FluentValidation;
using Reservations.Services.Offices.Commands;

namespace Reservations.Services.Offices.Validators
{
    public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
    {
        public CreateOfficeCommandValidator()
        {
            RuleFor(x => x.Location).NotEmpty()
                                    .MaximumLength(250);

            RuleFor(x => x.CloseTime).GreaterThan(x => x.OpenTime);
        }
    }
}
