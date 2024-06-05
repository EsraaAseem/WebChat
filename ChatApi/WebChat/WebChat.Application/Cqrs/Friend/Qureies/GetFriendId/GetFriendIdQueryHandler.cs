using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Friend.Service;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Qureies.GetFriendId
{
    internal class GetFriendIdQueryHandler : IQueryHandler<GetFriendIdQuery, BaseResponse>
    {
        private readonly IFreindService _freindService;

        public GetFriendIdQueryHandler(IFreindService freindService)
        {
            _freindService = freindService;
        }

        public async Task<BaseResponse> Handle(GetFriendIdQuery request, CancellationToken cancellationToken)
        {
            var friendShip =await _freindService.GetFriendId(request.friendShipId);
            if (friendShip is null)
                return await BaseResponse.NotFoundResponsAsync("friend ship not found");
            var friendId = friendShip.UserId == request.userId ? friendShip.FriendId : friendShip.UserId;
            return await BaseResponse.SuccessResponseWithDataAsync(friendId);
        }
    }
}
