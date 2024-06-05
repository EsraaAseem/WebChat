
using Microsoft.EntityFrameworkCore;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Friend.Responses;
using WebChat.Domain.Repositories;
using WebChat.Domain.Repository;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Commands.FriendRequest
{
    internal class FriendRequestCommandHandler : ICommandHandler<FriendRquestCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FriendRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(FriendRquestCommand request, CancellationToken cancellationToken)
        {
            var friendRepo = _unitOfWork.FriendRepository;
            var friendShip = await friendRepo.GetFriendShip(request.friendShipId);
            if (friendShip is null)
                return await BaseResponse.NotFoundResponsAsync("this friend ship not found");
            if (request.confirmRequest == true)
            {
                friendRepo.ConfirmFriendRequest(request.friendShipId);
                var friendres = new FriendResponse
                {
                    FriendShipId = friendShip.FriendshipId,
                    FriendName = request.friendId == friendShip.UserId ? friendShip.User.Name : friendShip.Friend.Name,
                    imgUrl = request.friendId == friendShip.UserId ? friendShip.User.ImgUrl : friendShip.Friend.ImgUrl,
                    FriendId = request.friendId
                };
                return await BaseResponse.SuccessResponseWithDataAndMsgAsync(friendres, " Accept Your Request");
            }
            else
            {
                friendRepo.DeleteFriendRequest(request.friendShipId);
                return await BaseResponse.SuccessResponseWithMessageAsync("Refused Your Request");
            }
        }
    }
}
