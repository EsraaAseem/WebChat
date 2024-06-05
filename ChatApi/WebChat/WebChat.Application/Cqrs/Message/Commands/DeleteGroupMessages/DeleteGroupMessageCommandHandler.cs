using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Repositories;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Commands.DeleteGroupMessages
{
    internal class DeleteGroupMessageCommandHandler : ICommandHandler<DeleteGroupMessageCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteGroupMessageCommandHandler( IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteGroupMessageCommand request, CancellationToken cancellationToken)
        {
            var groupMsg = await _unitOfWork.MessageRepository.DelGroupMsg(request.messageId);

            if (groupMsg == null)
                return await BaseResponse.NotFoundResponsAsync("not found group msg");
            groupMsg.UpdateDeleteStatusForMsg(request.isDeleteFor);
            if(request.isDeleteFor==3)
            groupMsg.updateDeleteMsg(request.userId);
            await _unitOfWork.SaveChangesAsync();
            var msgDto = _mapper.Map<GroupMessageResponse>(groupMsg);

            return await BaseResponse.SuccessResponseWithDataAndMsgAsync(msgDto, "Message Delete Success");
        }
    }
}
