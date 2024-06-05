using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebChat.Application.Cqrs.Authentication.Qureies.UserProfile;
using WebChat.Application.Cqrs.Friend.Commands.AddFriend;
using WebChat.Application.Cqrs.Friend.Commands.FriendRequest;
using WebChat.Application.Cqrs.Friend.Qureies.GetFriendId;
using WebChat.Application.Cqrs.Friend.Qureies.UserChats;
using WebChat.Application.Cqrs.Friend.Qureies.UserFriendRequests;
using WebChat.Application.Cqrs.Friend.Qureies.UserFriendShips;

using WebChat.Presentation.Abstractions;

namespace WebChat.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class FriendsController : ApiController
    {
        public FriendsController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend([FromForm] AddFriendCommand command,CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);       
            return Ok(result);
        }
        [HttpGet("friendId/{friendShipId}/{userId}")]
        public async Task<IActionResult> GetFriendId(int friendShipId,string userId, CancellationToken cancellationToken)
        {
            var query = new GetFriendIdQuery(friendShipId, userId);
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }
        [HttpPost("confirmRequest")]
        public async Task<IActionResult> ConfirmFriendRequest(FriendRquestCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpGet("friends/{userId}")]
        public async Task<IActionResult> GetFriends([FromRoute] UserFriendShipQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }
        [HttpGet("friends/requests/{userId}")]
        public async Task<IActionResult> GetFriendRequests([FromRoute] UserfriendRequestsQuery query, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);
        }
        [HttpGet("user/messsages/{userId}")]
        public async Task<IActionResult> GetUserChatsMessages([FromRoute] UserChatsQuery query,CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(query, cancellationToken));
        }
        [HttpGet("user/profile/{userId}")]

        public async Task<IActionResult> GetUserProfile([FromRoute] UserProfileQuery query,CancellationToken cancellationToken)
        {
            return Ok(await Sender.Send(query, cancellationToken));
        }
    }
}
