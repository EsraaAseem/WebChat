
using MapsterMapper;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Entities;
using WebChat.Domain.Repositories;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Message.Commands.CreateFriendMessage
{
    internal class CreateFriendMsgCommandHandler:ICommandHandler<CreateFriendMsgCommand,BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateFriendMsgCommandHandler( IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Handle(CreateFriendMsgCommand request, CancellationToken cancellationToken)
        {
            var msgRepo = _unitOfWork.MessageRepository;
            var friendShip = await msgRepo.GetFriendShipForMsg(request.FriendShipId);
            if (friendShip == null)
               return await BaseResponse.NotFoundResponsAsync("this friend Ship not found");
            var friendMessage = new FriendMessages(request.SenderId,request.Content,request.MessageTime);
            msgRepo.CreateFreindMessage(friendShip, friendMessage);
            await _unitOfWork.SaveChangesAsync();
            var response = _mapper.Map<FriendMessageResponse>(friendMessage);
            return await BaseResponse.SuccessResponseWithDataAsync(response);
        }
    }
}
