using System;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Commands.AddFriend
{
    public record AddFriendCommand(string userId,string friendPhone) :ICommand<BaseResponse>;   
}
