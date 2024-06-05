
using WebChat.Domain.Entities;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.service
{
    public interface IMessageService
    {
        Task<FriendShip> GetFriendMessages(int friendShipId);
        Task<Group> GetGroup(int groupId);
        Task<List<GroupMessages>> GetGroupMessages(int groupId);

    }
}
 