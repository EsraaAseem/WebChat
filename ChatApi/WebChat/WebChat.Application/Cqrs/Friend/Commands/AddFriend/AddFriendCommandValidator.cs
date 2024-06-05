using FluentValidation;

namespace WebChat.Application.Cqrs.Friend.Commands.AddFriend
{
    public class AddFriendCommandValidator: AbstractValidator<AddFriendCommand>
    {
        public AddFriendCommandValidator()
        {
            RuleFor(x => x.friendPhone).NotEmpty().WithMessage("friend phone required");
            RuleFor(x => x.userId).NotEmpty().WithMessage("user Id  required");
        }
    }
}
