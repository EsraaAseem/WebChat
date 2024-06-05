using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Commands.DeleteGroupMessages
{
    public record DeleteGroupMessageCommand(int messageId,int isDeleteFor,string groupName,string? userId):ICommand<BaseResponse>;
  
}
