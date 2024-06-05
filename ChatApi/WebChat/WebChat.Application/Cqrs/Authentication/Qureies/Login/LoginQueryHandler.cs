
using System.IdentityModel.Tokens.Jwt;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Abstractions.IInterfaces.Services;
using WebChat.Application.Cqrs.Authentication.Responses;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Qureies.Login
{
    internal class LoginQueryHandler : IQueryHandler<LoginQuery, BaseResponse>
    {
        private readonly IUserManagerService _userManager;
        private readonly IJwtService _jwtService;

        public LoginQueryHandler(IUserManagerService userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public async Task<BaseResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.userName);

            if (user is null || ! _userManager.CheckPassword(user, request.password))
            {
                return await BaseResponse.NotFoundResponsAsync("there is not user Name or password");
            }

            var response = new AuthResponse();

            var jwtSecurityToken = await _jwtService.GenerateToken(user);
            response.UserName = user.UserName;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.isAuthenticated = true;
            response.ExpiresOn = jwtSecurityToken.ValidTo;
            response.Id = user.Id;
            response.imgUrl = user.ImgUrl;
            return await BaseResponse.SuccessResponseWithDataAndMsgAsync(response, "Login success");
        }
    }
}
