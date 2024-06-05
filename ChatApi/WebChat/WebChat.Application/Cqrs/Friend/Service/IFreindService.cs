
using WebChat.Domain.Entities;

namespace WebChat.Application.Cqrs.Friend.Service
{
    public interface IFreindService
    {
        Task<FriendShip?> GetFriendId(int friendShipId);
        IQueryable<FriendShip> GetUserFriends(string userId);
        Task< IEnumerable<FriendShip>> GetUserFriendsWithMessage(string userId);
       Task< IEnumerable<Group>> GetUserGroupsWithMessage(string userId);
        IQueryable<UserGroup> GetUserGroups(string userId);
        IQueryable<FriendShip> GetUserFriendRequests(string userId);
    }
}
