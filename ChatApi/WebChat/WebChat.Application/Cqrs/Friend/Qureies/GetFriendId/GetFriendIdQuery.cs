using System;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Qureies.GetFriendId
{
    public record GetFriendIdQuery(int friendShipId, string userId):IQuery<BaseResponse>;
}
