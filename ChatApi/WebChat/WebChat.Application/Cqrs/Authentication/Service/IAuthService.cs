using System;
using WebChat.Domain.Entities;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Service
{
    public interface IAuthService
    {
        Task<AppUser> GetUSerProfile(string userid);
    }
}
