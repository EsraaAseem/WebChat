
using MapsterMapper;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Message.Commands.CreateFriendMessage;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Entities;
using WebChat.Domain.Repositories;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Commands.CreateGroupMessage
{
    internal class CreateGroupMsgCommandHandler : ICommandHandler<CreateGroupMsgCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CreateGroupMsgCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Handle(CreateGroupMsgCommand request, CancellationToken cancellationToken)
        {
            var msgRepo = _unitOfWork.MessageRepository;
            var groupExist =  msgRepo.GetGroupForMsg(request.GroupId);
            if (!groupExist)
                return await BaseResponse.NotFoundResponsAsync("this group not found");
            var groupMessage = new GroupMessages(request.SenderId,request.GroupId ,request.Content, request.MessageTime);
            msgRepo.CreateGroupMessage(groupMessage);
            await _unitOfWork.SaveChangesAsync();
           
            var result=_mapper.Map<GroupMessageResponse>(groupMessage);
            return await BaseResponse.SuccessResponseWithDataAsync(result);
        }
    }
}
