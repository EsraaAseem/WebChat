
using MapsterMapper;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Groups.Service;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Groups.Qureies.GetUserGroups
{
    internal class GetUserGroupsQueryHandler : IQueryHandler<GetUserGroupsQuery, BaseResponse>
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GetUserGroupsQueryHandler(IGroupService groupService,IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        public Task<BaseResponse> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = _groupService.GetUserGroups(request.userId);
            var response=_mapper.Map<List<UserGroupsForOnConnectResponse>>(groups);
            return BaseResponse.SuccessResponseWithDataAsync(response);
        }
    }
}
