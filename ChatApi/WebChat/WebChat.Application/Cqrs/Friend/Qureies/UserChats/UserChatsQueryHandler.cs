
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebChat.Application.Abstractions.IInterfaces;
using WebChat.Application.Cqrs.Friend.Service;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Entities;
using WebChat.Domain.Shared;

namespace WebChat.Application.Cqrs.Friend.Qureies.UserChats
{
    internal class UserChatsQueryHandler : IQueryHandler<UserChatsQuery, BaseResponse>
    {
        private readonly IFreindService _friendService;

        public UserChatsQueryHandler(IFreindService friendService)
        {
            _friendService = friendService;
        }
        public async Task<BaseResponse> Handle(UserChatsQuery request, CancellationToken cancellationToken)
        {
            var friends = await _friendService.GetUserFriendsWithMessage(request.userId);
            var groups = await _friendService.GetUserGroupsWithMessage(request.userId);

            var friendsChats = friends.Select(friend => new UserChatsResponse
            {
                Id = friend.FriendshipId,
                Name = friend.UserId == request.userId ? friend.Friend.UserName : friend.User.UserName,
                imgUrl = friend.UserId == request.userId ? friend.Friend.ImgUrl : friend.User.ImgUrl,
                Type = "Friend",
                Message = friend.Messages
                  .Where(m => (m.SenderId == request.userId && m.IsDeleteBySender == 0) ||
                        (m.SenderId !=request.userId && m.IsDeleteByReciver == 0 && m.IsDeleteBySender != 2))

                .OrderByDescending(m => m.MessageTime)
                      .Select(m => new GroupFriendsMesagesResponse(m.MessageId, m.Content, m.MessageTime))
                      .FirstOrDefault()
            }).OrderByDescending(f => f.Message?.messageTime)
              .ToList();

            var friendsGroupsChats =
            
                groups.Select(group => new UserChatsResponse
            {
                Id = group.GroupId,
                Name = group.GroupName,
                imgUrl = group.groupimgurl,
                Type = "Group",
                Message =group.Messages.Select(m => new GroupFriendsMesagesResponse(m.MessageId, m.Content, m.MessageTime))
                      .FirstOrDefault()
                }).OrderByDescending(f => f.Message?.messageTime)
              .ToList();

            var combinedList = friendsChats.Concat(friendsGroupsChats).ToList();
            combinedList = combinedList.OrderByDescending(c => c.Message?.messageTime).ToList();

            return await BaseResponse.SuccessResponseWithDataAndMsgAsync(combinedList, "get data success");
        }

      

       
        /*  public async Task<BaseResponse> Handle(UserChatsQuery request, CancellationToken cancellationToken)
          {

              var friends = _friendService.GetUserFriendsWithMessage(request.userId);
              var groups = _friendService.GetUserGroupssWithMessage(request.userId);
              var friendsChats = friends.Select(friend => new UserChatsResponse
              {
                  Id = friend.FriendshipId,
                  Name = friend.UserId == request.userId ? friend.Friend.UserName : friend.User.UserName,
                  imgUrl = friend.UserId == request.userId ? friend.Friend.ImgUrl : friend.User.ImgUrl,
                  Type = "Friend",
                  Message = friend.Messages.OrderByDescending(m => m.MessageTime)
                        .Select(m => new GroupFriendsMesagesResponse(m.MessageId, m.Content, m.MessageTime))
                        .FirstOrDefault()
              }).OrderByDescending(f => f.Message.messageTime)
                    .ToList();
              var friendsGroupsChats = groups.Select( group => new UserChatsResponse
              {
                  Id = group.GroupId,
                  Name = group.GroupName,
                  imgUrl = group.groupimgurl,
                  Type = "Group",
                  Message = group.Messages.OrderByDescending(m => m.MessageTime)
                      .Select(m => new GroupFriendsMesagesResponse(m.MessageId, m.Content, m.MessageTime))
                      .FirstOrDefault()
              }).OrderByDescending(f => f.Message.messageTime)
                    .ToList();


              var combinedList = friendsChats.Concat(friendsGroupsChats).ToList();
              combinedList = combinedList.OrderByDescending(c => c.Message?.messageTime).ToList();
              return await BaseResponse.SuccessResponseWithDataAndMsgAsync(combinedList,"get data success");
          }*/
    }
}
