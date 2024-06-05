using FluentValidation;


namespace WebChat.Application.Cqrs.Authentication.Qureies.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.userName).NotEmpty().NotNull().WithMessage("user name required");
            RuleFor(x => x.password).NotEmpty().WithMessage("user password required");
            RuleFor(x => x.password).MinimumLength(4).WithMessage("user password must more than 6 charchter");
        }
    }
}
