

namespace WebChat.Application.Cqrs.Groups.Qureies.GetUserGroups
{
    public record UserGroupsForOnConnectResponse
    {
        public string GroupName { get; set; }
        public int GroupId { get; set; }
    }
}
