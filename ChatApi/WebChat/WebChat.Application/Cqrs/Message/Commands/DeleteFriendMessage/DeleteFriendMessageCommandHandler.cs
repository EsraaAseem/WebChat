
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Entities;
using WebChat.Domain.Repositories;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Commands.DeleteFriendMessage
{
    public class DeleteFriendMessageCommandHandler : ICommandHandler<DeleteFriendMessageCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteFriendMessageCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Handle(DeleteFriendMessageCommand request, CancellationToken cancellationToken)
        {
            var friendShip = await _unitOfWork.MessageRepository.GetFriendShipForMsg(request.friendShipId);

            if (friendShip == null)
                return await BaseResponse.NotFoundResponsAsync("not found friend ship");
               var message=friendShip.UpdateMessageDeleteStatus(request.messageId, request.isDeleteFor);
                await _unitOfWork.SaveChangesAsync();
                var msgDto = _mapper.Map<FriendMessageResponse>(message);

            return await BaseResponse.SuccessResponseWithDataAndMsgAsync(msgDto, "Message Delete Success");
        }
    }
}
