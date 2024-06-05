
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Authentication.Service;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Qureies.UserProfile
{
    internal class UserProfileQueryHandler : IQueryHandler<UserProfileQuery, BaseResponse>
    {
        private readonly IAuthService _authService;

        public UserProfileQueryHandler( IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<BaseResponse> Handle(UserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _authService.GetUSerProfile(request.userId);
                if (user == null)
                return await BaseResponse.NotFoundResponsAsync("User Not Found");
            var result = new UserProfileResponse(user.UserName, user.Name, user.PhoneNumber, user.ImgUrl);
            return await BaseResponse.SuccessResponseWithDataAsync(result);
        }
    }
}
