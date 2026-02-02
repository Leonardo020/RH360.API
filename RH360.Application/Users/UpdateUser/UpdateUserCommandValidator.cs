using FluentValidation;

namespace RH360.Application.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);
        }
    }
}
