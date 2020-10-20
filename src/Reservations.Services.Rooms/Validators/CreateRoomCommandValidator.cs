using FluentValidation;
using Reservations.Services.Rooms.Commands;

namespace Reservations.Services.Offices.Validators
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(250);

            RuleFor(x => x.Capacity).GreaterThan(0);

            RuleFor(x => x.ChairCount).GreaterThanOrEqualTo(0);
        }
    }
}
