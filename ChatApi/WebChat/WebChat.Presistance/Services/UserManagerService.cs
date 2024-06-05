using Azure.Core;
using Microsoft.EntityFrameworkCore;
using WebChat.Application.Abstractions.IInterfaces.Services;
using WebChat.Domain.Entities;
using WebChat.Persistence.ContextData;

namespace WebChat.Presistance.Services
{
    public class UserManagerService:IUserManagerService
    {
        private readonly ChatDbContext _context;
        private readonly IPasswordHashService _passwordHashService;
        //private readonly IPasswordHasher<AppUser> _passwordHasher;

        public UserManagerService(ChatDbContext context, IPasswordHashService passwordHashService)//, IPasswordHasher<AppUser> passwordHasher)
        {
            _context = context;
            _passwordHashService = passwordHashService;
            // _passwordHasher = passwordHasher;
        }

        public async Task<bool> CreateAsync(AppUser user, string password)
        {
           _passwordHashService.CreatePasswordHash(password,
                out byte[] passwordHash,
                out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
             _context.AppUsers.Add(user);
            return await _context.SaveChangesAsync()>0;

        }

        public async Task<AppUser> FindByNameAsync(string username)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<AppUser> FindByPhoneAsync(string phone)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
        }

        public bool CheckPassword(AppUser user, string password)
        {
            return
            _passwordHashService.VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt);
        }

        public async Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            if (CheckPassword(user, currentPassword))
            {
                _passwordHashService.CreatePasswordHash(newPassword,
                               out byte[] passwordHash,
                               out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt; UpdateUser(user);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
        public void UpdateUser(AppUser user)
        {
            _context.AppUsers.Update(user);

        }
    }
}
