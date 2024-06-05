
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Qureies.GetFriendMessages
{
    public record GetFriendMsgsQuery(int friendShipId, string userId) :IQuery<BaseResponse>;
   
}
