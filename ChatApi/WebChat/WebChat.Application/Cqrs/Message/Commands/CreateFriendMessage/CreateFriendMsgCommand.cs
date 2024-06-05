using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;
using System.Text.Json.Serialization;

namespace WebChat.Application.Cqrs.Message.Commands.CreateFriendMessage
{
    public record CreateFriendMsgCommand :ICommand<BaseResponse>
    {
       
        public string SenderId { get; init; }
        public string Content { get; init; }
        public int FriendShipId { get; init; }
        [JsonIgnore]
        public DateTime MessageTime { get; init; } = DateTime.Now;

    };
   
}
