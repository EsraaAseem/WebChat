
using System.IdentityModel.Tokens.Jwt;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Abstractions.IInterfaces.Services;
using WebChat.Application.Cqrs.Authentication.Responses;
using WebChat.Domain.Entities;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Commands.Register
{
    internal class RegisterCommandHandler : ICommandHandler<RegisterCommand, BaseResponse>
    {
        private readonly IUserManagerService _userManager;
        private readonly IMediaService _mediaService;
        private readonly IJwtService _jwtService;

        public RegisterCommandHandler(IUserManagerService userManager, IMediaService mediaService,IJwtService jwtService)
        {
            _userManager = userManager;
            _mediaService = mediaService;
            _jwtService = jwtService;
        }

        public async Task<BaseResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByNameAsync(request.userName) is not null && await _userManager.FindByPhoneAsync(request.phoneNumber) is not null)
            {
                return await BaseResponse.BadRequestResponsAsync("User Already Exist");
            }
            var imgUrl = await _mediaService.UploadImageAsync(request.userImg, "UsersImages");

            var user = new AppUser(request.userName, request.phoneNumber, imgUrl, request.name);
            
          var result=  await _userManager.CreateAsync(user, request.password);
           
            if (result)
            {
                var jwtSecurityToken =await  _jwtService.GenerateToken(user);
                 _userManager.UpdateUser(user);
                var response= new AuthResponse
                {
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    isAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    UserName = user.UserName,
                    imgUrl = imgUrl
                };
                return await BaseResponse.SuccessResponseWithDataAndMsgAsync(response, "User Register Success");
            }
            return await BaseResponse.BadRequestResponsAsync("error in user register");
        }
    }
}
