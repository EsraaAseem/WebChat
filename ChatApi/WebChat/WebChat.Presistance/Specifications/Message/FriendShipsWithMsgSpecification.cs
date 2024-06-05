
using WebChat.Domain.Entities;

namespace WebChat.Presistance.Specifications.FriendsShip
{
    public class FriendShipsWithMsgSpecification : Specification<FriendShip>
    {
        public FriendShipsWithMsgSpecification(string userId) :
            base(f => (f.UserId == userId || f.FriendId == userId) && f.Messages.Any())
        {
            AddInclude(friend => friend.User);
            AddInclude(friend => friend.Friend);
        }
    }
}
