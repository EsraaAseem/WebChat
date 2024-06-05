
using WebChat.Domain.Entities;

namespace WebChat.Presistance.Specifications.FriendsShip
{
    public class FriendShipByIdSpecification : Specification<FriendShip>
    {
        public FriendShipByIdSpecification(int friendShipId) : base(friendship=>friendship.FriendshipId==friendShipId)
        {
            AddInclude(friend => friend.User);
            AddInclude(friend => friend.Friend);
        }
    }
}
