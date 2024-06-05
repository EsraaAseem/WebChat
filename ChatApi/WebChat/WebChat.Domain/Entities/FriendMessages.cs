
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace WebChat.Domain.Entities
{
    [Owned]
    public class FriendMessages:Message
    {
        public FriendMessages(string senderId,string content,DateTime messageTime)
        {
            SenderId=senderId;
            Content=content;
            MessageTime=messageTime;
        }
     

        public bool IsRead { get; set; }=false;
     
    }
}
