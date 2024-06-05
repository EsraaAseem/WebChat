
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Friend.Responses;
using WebChat.Application.Cqrs.Friend.Service;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Qureies.UserFriendShips
{
    internal class UserFriendShipQueryHandler : IQueryHandler<UserFriendShipQuery, BaseResponse>
    {
        private readonly IFreindService _freindService;

        public UserFriendShipQueryHandler(IFreindService freindService)
        {
            _freindService = freindService;
        }
        public async Task<BaseResponse> Handle(UserFriendShipQuery request, CancellationToken cancellationToken)
        {
            var frindShips = _freindService.GetUserFriends(request.userId);
            var frindResponse = frindShips.Select(friend => new FriendResponse
            {
                FriendShipId = friend.FriendshipId,
                FriendId = friend.UserId == request.userId ? friend.FriendId : friend.UserId,
                FriendName = friend.UserId == request.userId ? friend.Friend.UserName : friend.User.UserName,
                imgUrl = friend.UserId == request.userId ? friend.Friend.ImgUrl : friend.User.ImgUrl,
            }).ToList();
            return await BaseResponse.SuccessResponseWithDataAsync(frindResponse);
        }
    }
}
