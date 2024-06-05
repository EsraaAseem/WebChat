
using WebChat.Domain.Entities;

namespace WebChat.Domain.Repository
{
    public interface IMessageRepository
    {
       void CreateFreindMessage(FriendShip friendShip,FriendMessages message);
       Task<FriendShip> GetFriendShipForMsg(int friendShipId);
        void CreateGroupMessage(GroupMessages message);
        bool GetGroupForMsg(int groupId);
        Task< GroupMessages> GetGroupLastMsg(GroupMessages message);
        Task<FriendMessages> GetFriendMessage(int friendShipId,int messageId);
        Task<GroupMessages> DelGroupMsg(int messageId);
    }
}
