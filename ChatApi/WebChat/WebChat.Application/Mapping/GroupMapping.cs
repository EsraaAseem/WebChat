using Mapster;
using WebChat.Application.Cqrs.Groups.Qureies.GetGroupById;
using WebChat.Application.Cqrs.Groups.Qureies.GetUserGroups;
using WebChat.Domain.Entities;

namespace WebChat.Application.Mapping
{
    internal class GroupMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GroupResponse,Group>();
            config.NewConfig<Group, UserGroupsForOnConnectResponse>();
        }
    }
}
