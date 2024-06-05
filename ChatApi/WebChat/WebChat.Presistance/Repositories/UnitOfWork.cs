using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebChat.Domain.Repositories;
using WebChat.Domain.Repository;
using WebChat.Persistence.ContextData;

namespace WebChat.Persistence.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private ChatDbContext _context;

        public UnitOfWork(ChatDbContext context)
        {
            _context = context;
            MessageRepository = new MessageRepository(_context);
            FriendRepository = new FriendRepository(_context);
            GroupRepositoy=new GroupRepository(_context);
        }

       public IGroupRepositoy GroupRepositoy { get; private set; }
       public IMessageRepository MessageRepository { get; private set; }
        public IFriendRepository FriendRepository { get; private set; }


        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(); 
        }
    }
}
