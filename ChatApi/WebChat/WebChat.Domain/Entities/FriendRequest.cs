namespace WebChat.Domain.Entities
{
    public class FriendRequest
    {
        public int FrindRequestId { get; set; }
        public int FriendShipId { get; set; }
        public FriendShip FriendShip { get; set; }
    }
}
