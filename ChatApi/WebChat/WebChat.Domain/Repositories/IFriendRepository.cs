

using WebChat.Domain.Entities;
using WebChat.Domain.Shared;

namespace WebChat.Domain.Repository
{
    public interface IFriendRepository
    {
        Task<AppUser> GetUserById(string userId);
        Task<AppUser> GetFriendByPhone(string phone);
        Task<FriendShip> GetFriendShip(int friendShipId);
        void AddFriendShip(FriendShip friendShip);
        void ConfirmFriendRequest(int frindShipId);
        void DeleteFriendRequest(int friendShipId);
    

    }
}
