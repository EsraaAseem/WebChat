using MediatR;
using WebChat.Domain.Shared;

namespace WebChat.Application.Abstractions.IInterfaces;

public interface IQuery<TResponse> : IRequest<BaseResponse>
{
}