
using WebChat.Domain.Entities;

namespace WebChat.Presistance.Specifications.FriendsShip
{
    public class FriendRequestsByUserIdSpecification : Specification<FriendShip>
    {
        public FriendRequestsByUserIdSpecification(string userId) :
            base(f => f.FriendId == userId && f.RequestFriendConfirm == false)
        {
        }
    }
}
