
using WebChat.Domain.Entities;

namespace WebChat.Application.Cqrs.Groups.Service
{
    public interface IGroupService
    {
       Group GetGroupById(int groupId);
        IEnumerable<Group> GetUserGroups(string userId);
    }
}
