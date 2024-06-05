using Microsoft.EntityFrameworkCore;
using WebChat.Application.Cqrs.Message.service;
using WebChat.Domain.Entities;
using WebChat.Persistence.ContextData;
using WebChat.Presistance.Specifications;
using WebChat.Presistance.Specifications.Message;

namespace WebChat.Persistence.Services
{
    public class MessageQueryService:IMessageService
    {
        private readonly ChatDbContext _context;
        public MessageQueryService(ChatDbContext context)
        {
            _context = context;
        }
        public async Task<FriendShip> GetFriendMessages(int friendShipId)
        {
            return await ApplySpecification(new GetFriendMsgsSpecification(friendShipId)).FirstOrDefaultAsync();
        }
        private IQueryable<FriendShip> ApplySpecification(Specification<FriendShip> specification)
        {
            return SpecificationEvaluator.GetQuery(_context.Set<FriendShip>(), specification);
        }
        public async Task<Group> GetGroup(int groupId)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.GroupId == groupId);

        }
        public async Task<List<GroupMessages>> GetGroupMessages(int groupId)
        {
            return await  _context.GroupMessages.Include(m=>m.User).Where(g => g.GroupId == groupId).ToListAsync();

        }
    }
}
