using FluentValidation;

namespace WebChat.Application.Cqrs.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {

            RuleFor(x => x.name).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(x => x.userName).NotEmpty().NotNull().WithMessage("user name required");
            RuleFor(x => x.phoneNumber).NotEmpty().NotNull().WithMessage("user phone number required");
            RuleFor(x => x.userImg).NotEmpty().NotNull().WithMessage("user Image required");
            RuleFor(x => x.password).NotEmpty().NotNull().WithMessage("user password required");

        }
    }

}
