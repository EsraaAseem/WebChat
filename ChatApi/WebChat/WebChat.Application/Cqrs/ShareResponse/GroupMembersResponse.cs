using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.Application.Cqrs.ShareResponse
{
    public record GroupMembersResponse
    {
        public UserChatsResponse Group { get; set; }
        public List<string>? Users { get; set; }
    }
}
