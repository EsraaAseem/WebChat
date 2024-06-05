using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using WebChat.Application.Cqrs.Groups.Commands.CreateGroup;
using WebChat.Application.Cqrs.Groups.Qureies.GetGroupById;
using WebChat.Application.Cqrs.Groups.Qureies.GetUserGroups;
using WebChat.Application.Cqrs.Groups.Qureies.GroupImg;
using WebChat.Presentation.Abstractions;

namespace WebChat.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : ApiController
    {
        public GroupsController(ISender sender) : base(sender)
        {
        }

       [HttpPost("createGroup")]
        public async Task<IActionResult> CreateGroup([FromForm] CreateGroupCommand command,CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);       
            return Ok(result);
        }
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupById([FromRoute] GetGroupByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }
        [HttpGet("userGroups/{userId}")]
        public async Task<IActionResult> GetUserGroups([FromRoute] GetUserGroupsQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }
        [HttpGet("groupImg/{groupId}")]
        public async Task<IActionResult> getGroupImg([FromRoute] GroupImgQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
