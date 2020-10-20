using FluentValidation;
using Reservations.Services.Reservations.Commands;

namespace Reservations.Services.Reservations.Validators
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
        }
    }
}
