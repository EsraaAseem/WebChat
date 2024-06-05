using MapsterMapper;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Message.service;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Shared;


namespace WebChat.Application.Cqrs.Message.Qureies.GroupMessages
{
    internal class GroupMessagesQueryHandler : IQueryHandler<GroupMessagesQuery, BaseResponse>
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public GroupMessagesQueryHandler(IMessageService messageService,IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(GroupMessagesQuery request, CancellationToken cancellationToken)
        {
            var group = await _messageService.GetGroup(request.groupId);
            if (group == null)
                return await BaseResponse.NotFoundResponsAsync("this group not found");
            var messages=await _messageService.GetGroupMessages(request.groupId);
            var messagesRes = _mapper.Map<List<GroupMessageResponse>>(messages);
            return await BaseResponse.SuccessResponseWithDataAsync(messagesRes);
        }
    }
}
