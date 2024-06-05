
using WebChat.Persistence.ContextData;
using WebChat.Application.Cqrs.Groups.Service;
using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Persistence.Services
{
    public class GroupQueryService:IGroupService
    {
        private static readonly Func<ChatDbContext, int, Group?> getById = EF.CompileQuery(
  (ChatDbContext context, int groupId) => context.Set<Group>().FirstOrDefault(x => x.GroupId == groupId));

        private static readonly Func<ChatDbContext, string, IEnumerable<Group>> getUserGroups = EF.CompileQuery(
    (ChatDbContext context, string userId) => context.Set<UserGroup>().Where(ug => ug.UserId == userId)
                        .Select(ug => ug.GroupUser)
                       );
        private readonly ChatDbContext _context;
        public GroupQueryService(ChatDbContext context)
        {
            _context = context;
        }
             
        public Group? GetGroupById(int groupId)
        {
            return getById(_context, groupId);
        }
        public IEnumerable<Group> GetUserGroups(string userId)
        {
            return getUserGroups(_context, userId);
        } 

    }

}
