
using MapsterMapper;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Message.service;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Qureies.GetFriendMessages
{
    internal class GetFriendMsgsQueryHandler : IQueryHandler<GetFriendMsgsQuery, BaseResponse>
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public GetFriendMsgsQueryHandler(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(GetFriendMsgsQuery request, CancellationToken cancellationToken)
        {
            var friendExist =await _messageService.GetFriendMessages(request.friendShipId);
            if (friendExist == null)
            {
                return await BaseResponse.NotFoundResponsAsync("friend Ship Not Found");
            }
            var friend = new FriendWithMsgsResponse();
            friend.FriendShipId = friendExist.FriendshipId;
            friend.FriendId = friendExist.UserId ==request.userId ? friendExist.FriendId : friendExist.UserId;
            friend.SenderName = friendExist.UserId == request.userId ? friendExist.Friend.Name : friendExist.User.Name;
            friend.SenderImgUrl = friendExist.UserId == request.userId ? friendExist.Friend.ImgUrl : friendExist.User.ImgUrl;
            if(friendExist.Messages.Count > 0)
            friend.FriendsChat = _mapper.Map<List<FriendMessageResponse>>(friendExist.Messages);
            return await BaseResponse.SuccessResponseWithDataAsync(friend);
        }
    }
}
