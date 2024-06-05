using MediatR;
using WebChat.Domain.Shared;

namespace WebChat.Application.Abstractions.IInterfaces;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, BaseResponse>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand,  BaseResponse>
    where TCommand : ICommand<TResponse>
{
}
