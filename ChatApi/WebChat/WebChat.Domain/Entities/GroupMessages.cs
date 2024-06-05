
using System.Text.Json.Serialization;

namespace WebChat.Domain.Entities
{
    public class GroupMessages:Message
    {
        private readonly List<DeletedMessages> _deleteMessages = new();
     
        public GroupMessages(string senderId,int groupId,string content,DateTime messageTime)
        {
            SenderId = senderId;
            GroupId = groupId;
            Content = content;
            MessageTime = messageTime;
        }

        public int GroupId { get; private set; }
        public AppUser User { get; private set; }
        public IReadOnlyCollection<DeletedMessages>? DeletedForUserIds => _deleteMessages;
        public void updateDeleteMsg(string userId)
        {

            var delMsg = new DeletedMessages(userId);
            _deleteMessages.Add(delMsg);

        }
    }
}
