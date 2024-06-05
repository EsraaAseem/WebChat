using FluentValidation;


namespace WebChat.Application.Cqrs.Friend.Commands.FriendRequest
{
    public class FriendRequestCommandValidator : AbstractValidator<FriendRquestCommand>
    {
        public FriendRequestCommandValidator()
        {

           RuleFor(x => x.friendId).NotEmpty().WithMessage("Friend Id  required");
            RuleFor(x => x.friendShipId).NotEmpty().WithMessage("Friend Ship Id  required");


        }
    }
}
