using MapsterMapper;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Groups.Service;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Groups.Qureies.GetGroupById
{
    internal class GetGroupByIdQueryHandler : IQueryHandler<GetGroupByIdQuery, BaseResponse>
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public GetGroupByIdQueryHandler(IGroupService groupService,IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = _groupService.GetGroupById(request.groupId);
            if (group == null)
                return await BaseResponse.NotFoundResponsAsync("there no group with this id");
            var groupResponse = _mapper.Map<GroupResponse>(group);
            return await BaseResponse.SuccessResponseWithDataAsync(groupResponse);
        }
    }
}
