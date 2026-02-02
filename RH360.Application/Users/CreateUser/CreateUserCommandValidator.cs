using FluentValidation;
using MediatR;
using RH360.Application.Users.GetUserByEmail;

namespace RH360.Application.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IMediator mediator)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200)
                .MustAsync(async (email, cancellation) =>
                {
                    // chama seu handler:
                    var user = await mediator.Send(new GetUserByEmailQuery(email), cancellation);
                    return user is null;
                })
                .WithMessage("O e-mail informado já está em uso.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(255);

      
        }
    }
}
