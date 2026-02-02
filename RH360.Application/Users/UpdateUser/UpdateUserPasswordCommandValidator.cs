using FluentValidation;

namespace RH360.Application.Users.UpdateUser
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(255);
        }
    }
}
