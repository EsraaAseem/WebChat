using Microsoft.EntityFrameworkCore;
using WebChat.Application.Abstractions;
using WebChat.Domain.Entities;
using WebChat.Domain.Repository;
using WebChat.Domain.Shared.Statics;
using WebChat.Domain.Shared;
using WebChat.Persistence.ContextData;

namespace WebChat.Persistence.Repositories
{
    public class GroupRepository:IGroupRepositoy
    {
        private readonly ChatDbContext _context;
        public GroupRepository(ChatDbContext context)
        {
            _context = context;
        }
        public void CreateGroup(Group group)
        {
            _context.Set<Group>().Add(group);
        }
       
        public async Task<bool> CheckGroupExist(string groupName,string groupCreatedBy)
        {
            return await _context.Groups.AnyAsync(g => g.GroupName ==groupName && g.CreatedGroupBy == groupCreatedBy);
        }
    }
}
