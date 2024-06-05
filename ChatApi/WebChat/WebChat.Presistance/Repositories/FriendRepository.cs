using WebChat.Domain.Entities;
using WebChat.Domain.Repository;
using WebChat.Persistence.ContextData;
using Microsoft.EntityFrameworkCore;
using WebChat.Presistance.Specifications;
using WebChat.Presistance.Specifications.FriendsShip;
using Dapper;

namespace WebChat.Persistence.Repositories
{
    public class FriendRepository: IFriendRepository
    {
        private readonly ChatDbContext _context;

        public FriendRepository(ChatDbContext context)
        {
            _context = context;
        }
        public void AddFriendShip(FriendShip friendShip)
        {
            _context.Set<FriendShip>().Add(friendShip);

        }

        public async Task<AppUser?> GetFriendByPhone(string phone)
        {
            return await _context.AppUsers
                                     .Where(u => u.PhoneNumber == phone)
                                     .FirstOrDefaultAsync();
        }

        public async Task<FriendShip> GetFriendShip(int friendShipId)
        {
            var friendShip =await  ApplySpecification(new FriendShipByIdSpecification(friendShipId)).FirstOrDefaultAsync();
            return friendShip;
        }
        public async void ConfirmFriendRequest(int frindShipId)
        {
            await _context.Database.GetDbConnection().ExecuteAsync("UPDATE FriendShip SET RequestFriendConfirm=1 WHERE FriendshipId=@friendShipId",
                new { friendShipId=frindShipId });
        }
        public async Task<AppUser?> GetUserById(string userId)
        {
            return await _context.AppUsers
                                     .FromSqlRaw("SELECT * FROM AppUsers WHERE Id = {0}", userId)
                                     .FirstOrDefaultAsync();
        }
        private IQueryable<FriendShip> ApplySpecification(Specification<FriendShip> specification)
        {
            return SpecificationEvaluator.GetQuery(_context.Set<FriendShip>(), specification);
        }

        public async void DeleteFriendRequest(int friendShipId)
        {
            await _context.FriendShip.Where(f => f.FriendshipId == friendShipId).ExecuteDeleteAsync();
        }

    }
}
