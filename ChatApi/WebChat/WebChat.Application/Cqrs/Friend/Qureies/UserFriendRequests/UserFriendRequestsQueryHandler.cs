
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Friend.Service;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Qureies.UserFriendRequests
{
    internal class UserFriendRequestsQueryHandler : IQueryHandler<UserfriendRequestsQuery, BaseResponse>
    {
        private readonly IFreindService _friendService;

        public UserFriendRequestsQueryHandler(IFreindService friendService)
        {
            _friendService = friendService;
        }

        public async Task<BaseResponse> Handle(UserfriendRequestsQuery request, CancellationToken cancellationToken)
        {
            var frindShips = _friendService.GetUserFriendRequests(request.userId);
            var frindResponse = frindShips.Select(f => new FriendRequestResponse
            {
                SenderName = f.User.Name,
                FriendRequestSenderId = f.UserId,
                FriendShipId = f.FriendshipId,
                imgUrl = f.User.ImgUrl,
                FriendRequestReciverId = f.FriendId,
            }).ToList();
            return await BaseResponse.SuccessResponseWithDataAsync(frindResponse);
        }
    }
}
