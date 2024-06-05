using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.ComponentModel.DataAnnotations;
using WebChat.Domain.Shared.Statics;

namespace WebChat.Domain.Entities
{
    public class Message
    {

       [Key]
        public int MessageId { get;  internal set; }
        public string SenderId { get; internal set; }
        public string? Content { get; internal set; }
        public int IsDeleteBySender { get; internal set; } = MessageDeleteTyp.NotDelete;
        public int IsDeleteByReciver { get; internal set; } = MessageDeleteTyp.NotDelete;
        public DateTime MessageTime { get; internal set; }
        public void UpdateDeleteStatusForMsg(int deleteStatus)
        {
            if (deleteStatus == 3)
                IsDeleteByReciver = 1;
            else
                IsDeleteBySender = deleteStatus;

        }
    }
}
