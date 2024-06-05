

using Mapster;
using WebChat.Application.Cqrs.Message.Commands.CreateFriendMessage;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Entities;

namespace WebChat.Application.Mapping
{
    internal class MessageMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateFriendMsgCommand,FriendMessages >();
            config.NewConfig<FriendMessages,FriendMessageResponse>();
            config.NewConfig<GroupMessages, GroupMessageResponse>()
                          .Map(dest => dest.SenderName, src => src.User.Name)
                          .Map(dest => dest.SenderImgUrl, src => src.User.ImgUrl);
        }
    }
    
}
