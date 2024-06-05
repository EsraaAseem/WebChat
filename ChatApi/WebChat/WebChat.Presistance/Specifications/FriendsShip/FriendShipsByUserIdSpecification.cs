
using WebChat.Domain.Entities;

namespace WebChat.Presistance.Specifications.FriendsShip
{
    public class FriendShipsByUserIdSpecification :Specification<FriendShip>
    {
        public FriendShipsByUserIdSpecification(string userId) :
            base(f => (f.UserId == userId || f.FriendId == userId) && f.RequestFriendConfirm == true)
        {

        }
    }
}
