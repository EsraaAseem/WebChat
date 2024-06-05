
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebChat.Domain.Entities;
using WebChat.Domain.Repository;
using WebChat.Domain.Shared;
using WebChat.Persistence.ContextData;

namespace WebChat.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;
        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }
        public async Task<FriendShip> GetFriendShipForMsg(int friendShipId)
        {
            return await _context.FriendShip.FirstOrDefaultAsync(f => f.FriendshipId == friendShipId);
        }
        public void CreateFreindMessage(FriendShip friendShip, FriendMessages message)
        {
            friendShip.AddFriendMessage(message);
        }

        public void CreateGroupMessage(GroupMessages message)
        {
            _context.Set<GroupMessages>().Add(message);

        }

        public bool GetGroupForMsg(int groupId)
        {
            return _context.Groups.Any(f => f.GroupId == groupId);
        }
        public async Task<GroupMessages?> GetGroupLastMsg(GroupMessages message)
        {
            return await _context.GroupMessages.Include(m => m.User).LastOrDefaultAsync();
        }
        public async Task<FriendMessages?> GetFriendMessage(int friendShipId, int messageId)
        {
            var msg = await _context.FriendShip.FirstOrDefaultAsync(f => f.FriendshipId == friendShipId);
            var message = msg.Messages.FirstOrDefault(m => m.MessageId == messageId);
            return message;
        }
        public async Task<GroupMessages> DelGroupMsg(int messageId)
        {
            return await _context.GroupMessages.FirstOrDefaultAsync(g => g.MessageId == messageId);

        }
    }
}
