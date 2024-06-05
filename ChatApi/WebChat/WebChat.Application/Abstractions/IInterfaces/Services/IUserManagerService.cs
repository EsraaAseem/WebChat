using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Domain.Entities;

namespace WebChat.Application.Abstractions.IInterfaces.Services
{
    public interface IUserManagerService
    {
        Task<bool> CreateAsync(AppUser user, string password);
        Task<AppUser> FindByNameAsync(string username);
        Task<AppUser> FindByPhoneAsync(string phone);
        bool CheckPassword(AppUser user, string password);
        Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        void UpdateUser(AppUser user);

    }
}
