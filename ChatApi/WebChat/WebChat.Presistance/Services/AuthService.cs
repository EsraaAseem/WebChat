using Microsoft.EntityFrameworkCore;
using WebChat.Application.Cqrs.Authentication.Service;
using WebChat.Domain.Entities;
using WebChat.Persistence.ContextData;

namespace WebChat.Presistance.Services
{
    public class AuthService : IAuthService
    {
        private readonly ChatDbContext _context;

        public AuthService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUSerProfile(string userId)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
