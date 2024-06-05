using System;

using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Qureies.UserChats
{
    public record UserChatsQuery(string userId):IQuery<BaseResponse>;
   
}
