using Microsoft.EntityFrameworkCore;
using WebChat.Application.Cqrs.Friend.Service;
using WebChat.Domain.Entities;
using WebChat.Persistence.ContextData;
using WebChat.Presistance.Specifications;
using WebChat.Presistance.Specifications.FriendsShip;

namespace WebChat.Persistence.Services
{
    public class FriendQueryService:IFreindService
    {

        private readonly ChatDbContext _context;
        public FriendQueryService(ChatDbContext context)
        {
            _context = context;
        }
        public async Task<FriendShip?> GetFriendId(int friendShipId)
        {
            return await _context.FriendShip
                                 .FromSqlRaw("SELECT * FROM FriendShip WHERE FriendshipId = {0}", friendShipId)
                                 .FirstOrDefaultAsync();
        }

        public  IQueryable<FriendShip> GetUserFriends(string userId)
        {
            var friendShips =  ApplySpecification(new FriendShipsByUserIdSpecification(userId));
            return friendShips;
        }
        public IQueryable<FriendShip> GetUserFriendRequests(string userId)
        {
            var friendShips = ApplySpecification(new FriendRequestsByUserIdSpecification(userId));
            return friendShips;
        }
        private IQueryable<FriendShip> ApplySpecification(Specification<FriendShip> specification)
        {
            return SpecificationEvaluator.GetQuery(_context.Set<FriendShip>(), specification);
        }
        public async Task<IEnumerable<FriendShip>> GetUserFriendsWithMessage(string userId)
        {
            var res = await ApplySpecification(new FriendShipsWithMsgSpecification(userId))
                                .ToListAsync();
            return res;
        }

       
        public async Task<IEnumerable<Group>> GetUserGroupsWithMessage(string userId)
        {
            var groups = await _context.UserGroups
               .Where(ug => ug.UserId == userId)
               .Select(ug => new
               {
                   ug.GroupUser.GroupId,
                   ug.GroupUser.GroupName,
                   ug.GroupUser.groupimgurl,
                   LastMessage = ug.GroupUser.Messages

                     .Where(m => (m.SenderId == userId && m.IsDeleteBySender == 0) ||
                        (m.SenderId != userId && !m.DeletedForUserIds.Any(dm => dm.UserId == userId) && m.IsDeleteBySender != 2))
                   .OrderByDescending(m => m.MessageTime).FirstOrDefault()
               })
               .ToListAsync();

            return groups.Select(g => Group.getGroupwithLastMsg(g.GroupId,g.GroupName, g.groupimgurl,g.LastMessage));
        }
        public IQueryable<UserGroup> GetUserGroups(string userId)
        {
            return _context.UserGroups
                             .Where(ug => ug.UserId == userId);
        }

        
    }
}
