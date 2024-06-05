
using WebChat.Application.Abstractions.IInterfaces;

using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Qureies.GroupMessages
{
    public record GroupMessagesQuery(int groupId,string? groupName):IQuery<BaseResponse>;
   
}
