using System.IdentityModel.Tokens.Jwt;
using WebChat.Domain.Entities;

namespace WebChat.Application.Abstractions.IInterfaces.Services
{
    public interface IJwtService
    {
         Task<JwtSecurityToken> GenerateToken(AppUser user);
    }
}
