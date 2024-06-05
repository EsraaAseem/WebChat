using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Groups.Service;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Groups.Qureies.GroupImg
{
    internal class GroupImgHandler : IQueryHandler<GroupImgQuery, BaseResponse>
    {
        private readonly IGroupService _groupService;
        public GroupImgHandler( IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<BaseResponse> Handle(GroupImgQuery request, CancellationToken cancellationToken)
        {
            var group =  _groupService.GetGroupById(request.groupId);
            return await BaseResponse.SuccessResponseWithDataAsync(group.groupimgurl);
        }
    }
}
