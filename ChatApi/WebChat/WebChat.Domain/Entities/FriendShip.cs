
namespace WebChat.Domain.Entities
{
    public class FriendShip
    {
        private readonly List<FriendMessages> _friendMessages = new();

        public FriendShip(string userId,string friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }

      

        public int FriendshipId { get; set; }
        public string UserId { get; private set; }
        public AppUser User { get; set; }
        public string FriendId { get; private set; }
        public AppUser Friend { get; set; }
        public bool RequestFriendConfirm { get; private set; }=false;
        public IReadOnlyCollection<FriendMessages>? Messages => _friendMessages;
        public void AddFriendMessage(FriendMessages message)
        {
            _friendMessages.Add(message);
        }
        public FriendMessages UpdateMessageDeleteStatus(int messageId, int deleteStatus)
        {
            var message = _friendMessages.FirstOrDefault(m => m.MessageId == messageId);
            if (message != null)
            {
                message.UpdateDeleteStatusForMsg(deleteStatus);
            }
            return message;
        }

    }
   
}
