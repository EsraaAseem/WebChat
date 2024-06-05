using MediatR;
using WebChat.Domain.Shared;

namespace WebChat.Application.Abstractions.IInterfaces;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery,BaseResponse>
    where TQuery : IQuery<TResponse>
{
}