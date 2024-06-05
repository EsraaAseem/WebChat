using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Groups.Qureies.GroupImg
{
    public record GroupImgQuery(int groupId):IQuery<BaseResponse>;
}
