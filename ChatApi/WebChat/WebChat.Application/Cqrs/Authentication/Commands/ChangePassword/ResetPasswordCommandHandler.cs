
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Authentication.Commands.ChangePassword
{
    internal class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, BaseResponse>
    {
        public Task<BaseResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
