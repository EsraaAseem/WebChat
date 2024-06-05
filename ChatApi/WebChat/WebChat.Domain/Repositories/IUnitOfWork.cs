
using WebChat.Domain.Repository;

namespace WebChat.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IMessageRepository MessageRepository { get; }
        IGroupRepositoy GroupRepositoy { get; }
        IFriendRepository FriendRepository { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
