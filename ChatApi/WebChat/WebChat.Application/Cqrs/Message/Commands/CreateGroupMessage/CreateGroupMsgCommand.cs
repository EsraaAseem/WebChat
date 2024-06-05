
using System.Text.Json.Serialization;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Commands.CreateGroupMessage
{
    public record CreateGroupMsgCommand:ICommand<BaseResponse>
    {
        public string SenderId { get; set; }
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public DateTime MessageTime { get; set; } = DateTime.Now;
    }
}
