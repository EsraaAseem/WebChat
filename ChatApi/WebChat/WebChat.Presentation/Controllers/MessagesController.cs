using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using WebChat.Application.Cqrs.Groups.Commands.CreateGroup;
using WebChat.Application.Cqrs.Message.Commands.CreateFriendMessage;
using WebChat.Application.Cqrs.Message.Commands.CreateGroupMessage;
using WebChat.Application.Cqrs.Message.Commands.DeleteFriendMessage;
using WebChat.Application.Cqrs.Message.Commands.DeleteGroupMessages;
using WebChat.Application.Cqrs.Message.Qureies.GetFriendMessages;
using WebChat.Application.Cqrs.Message.Qureies.GroupMessages;
using WebChat.Presentation.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebChat.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : ApiController
    {
        public MessagesController(ISender sender) : base(sender)
        {
        }
        [HttpPost("createMesssage")]
        public async Task<IActionResult> CreateMessage(CreateFriendMsgCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("createGroupMesssage")]
        public async Task<IActionResult> CreateGroupMessage(CreateGroupMsgCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpGet("friend/{friendShipId}/{userId}")]
        public async Task<IActionResult> GetFriend([FromRoute] GetFriendMsgsQuery query, CancellationToken cancellationToken)
        {
            var res = await Sender.Send(query, cancellationToken);
            return Ok(res);
        }
        [HttpGet("getGroupMesssages/{groupId}")]
        public async Task<IActionResult> GetGroupMessages([FromRoute] GroupMessagesQuery query, CancellationToken cancellationToken)
        {
            var res = await Sender.Send(query, cancellationToken);
            return Ok(res);
        }
        [HttpDelete("deleteMesssage")]
        public async Task<IActionResult> DeleteMessage(DeleteFriendMessageCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("deleteGroupMesssage")]
        public async Task<IActionResult> DeleteGroupMessage(DeleteGroupMessageCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
    
        }
    }
}
