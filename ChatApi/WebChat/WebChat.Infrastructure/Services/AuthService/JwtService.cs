using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebChat.Application.Abstractions.IInterfaces.Services;
using WebChat.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace WebChat.Infrastructure.Services.AuthService
{
    public class JwtService:IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<JwtSecurityToken> GenerateToken(AppUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Id", user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"])),
                signingCredentials: creds);

            return token; //new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
