

using WebChat.Domain.Entities;

namespace WebChat.Presistance.Specifications.Message
{
    public class GetFriendMsgsSpecification : Specification<FriendShip>
    {
        public GetFriendMsgsSpecification(int friendShipId) :
            base(f => f.FriendshipId == friendShipId)
        {
            AddInclude(friend => friend.User);
            AddInclude(friend => friend.Friend);
        }
    } 

    
}
