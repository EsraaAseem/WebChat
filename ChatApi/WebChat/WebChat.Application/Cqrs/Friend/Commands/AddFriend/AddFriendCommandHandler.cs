
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Entities;
using WebChat.Domain.Repositories;
using WebChat.Domain.Repository;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Commands.AddFriend
{
    internal class AddFriendCommandHandler : ICommandHandler<AddFriendCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddFriendCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            var friendRepo = _unitOfWork.FriendRepository;
            var friend = await friendRepo.GetFriendByPhone(request.friendPhone);
            var user = await friendRepo.GetUserById(request.userId);
            if (user is null || friend is null)
            {
                return await BaseResponse.BadRequestResponsAsync("user or friend id is wrong");
            }
            var friendShip=new FriendShip(user.Id, friend.Id);
             friendRepo.AddFriendShip(friendShip);
          await _unitOfWork.SaveChangesAsync();
            var friendResponse=new FriendRequestResponse(friendShip.FriendshipId,user.Name,user.Id,friend.Id,user.ImgUrl);
            return await BaseResponse.SuccessResponseWithDataAsync(friendResponse);
        }
    }
}
