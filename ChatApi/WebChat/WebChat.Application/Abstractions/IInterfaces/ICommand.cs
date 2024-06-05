using MediatR;
using WebChat.Domain.Shared;

namespace WebChat.Application.Abstractions.IInterfaces;

public interface ICommand : IRequest<BaseResponse>
{
}

public interface ICommand<TResponse> : IRequest<BaseResponse>
{
}
