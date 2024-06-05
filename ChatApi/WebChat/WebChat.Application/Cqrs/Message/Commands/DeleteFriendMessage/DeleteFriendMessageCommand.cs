
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Commands.DeleteFriendMessage
{
    public record DeleteFriendMessageCommand(int messageId,int isDeleteFor,int friendShipId):ICommand<BaseResponse>;
   
}
