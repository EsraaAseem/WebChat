using MediatR;
using Microsoft.AspNetCore.SignalR;
using WebChat.Application.Cqrs.Friend.Commands.AddFriend;
using WebChat.Application.Cqrs.Friend.Commands.FriendRequest;
using WebChat.Application.Cqrs.Friend.Qureies.GetFriendId;
using WebChat.Application.Cqrs.Groups.Commands.CreateGroup;
using WebChat.Application.Cqrs.Groups.Qureies.GetUserGroups;
using WebChat.Application.Cqrs.Message.Commands.CreateFriendMessage;
using WebChat.Application.Cqrs.Message.Commands.CreateGroupMessage;
using WebChat.Application.Cqrs.Message.Commands.DeleteFriendMessage;
using WebChat.Application.Cqrs.Message.Commands.DeleteGroupMessages;
using WebChat.Application.Cqrs.Message.Qureies.GroupMessages;
using WebChat.Application.Cqrs.ShareResponse;
using WebChat.Domain.Shared;

namespace WebChat.Presentation.Hubs
{
    public class ChatHub:Hub
    {
        public static Dictionary<string, string> connectedUsers = new Dictionary<string, string>();
        protected readonly ISender Sender;
        public ChatHub(ISender sender)
        {
           Sender=sender;
        }
        public async Task OnConnectUser(string userId)//GetUserGroupsQuery query,CancellationToken cancellationToken)
        {
            var query = new GetUserGroupsQuery(userId);
            if (connectedUsers.ContainsKey(userId))
            {
                connectedUsers[userId] = Context.ConnectionId;
            }
            else
            {
                connectedUsers.Add(userId, Context.ConnectionId);
            }
            await Clients.All.SendAsync("UpdateConnectedUsers", connectedUsers);
            var result = await Sender.Send(query);
            var groups = (List<UserGroupsForOnConnectResponse>)result.Data;

            foreach (var group in groups)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, group.GroupName);
            }

        }
        public async Task AcceptFriendRequest(FriendRquestCommand command)
        {
            var result = await Sender.Send(command);
            if (connectedUsers.ContainsKey(command.friendId))
            {
                string connectionReciverId = connectedUsers[command.friendId];
                await Clients.Client(connectionReciverId).SendAsync("ReceiveMessageNotifaction", result.Message);

                if (result.Data is not null)
                {
                    await Clients.Client(connectionReciverId).SendAsync("ReceiveNewFriend", result.Data);
                }
            }
        }
        public async Task  SendFriendRequest(AddFriendCommand command)
        {
            var result = await Sender.Send(command);
            FriendRequestResponse friend =(FriendRequestResponse) result.Data;
            
            if (connectedUsers.ContainsKey(friend.friendRequestReciverId))
            {
                string connectionReciverId = connectedUsers[friend.friendRequestReciverId];
                string connectionSenderId = connectedUsers[friend.friendRequestSenderId];
                await Clients.Clients(connectionReciverId, connectionReciverId).SendAsync("ReceiveFriendRequest",friend); 
            }
            else
            {
               await BaseResponse.NotFoundResponsAsync("user not found in dictionary");
            }
        }
        public async  Task SendMessage(CreateFriendMsgCommand command)
        {
            var result = await Sender.Send(command);
            var message= (FriendMessageResponse) result.Data;
            var query = new GetFriendIdQuery(command.FriendShipId, message.SenderId);
            var resultRec = await Sender.Send(query);
            var reciverId = (string)resultRec.Data;
            string connectionSenderId = connectedUsers[message.SenderId];

            if (connectedUsers.ContainsKey(reciverId))
             {
               string connectionReciverId = connectedUsers[reciverId];
                await Clients.Clients(connectionReciverId, connectionSenderId).SendAsync("ReceiveMessage", message);


            }
            else
            {
                await Clients.Client(connectionSenderId).SendAsync("ReceiveMessage", message);

            }
        }
        
        public async Task SendGroupMessage(CreateGroupMsgCommand query)
        {
            var res = await Sender.Send(query);
            await Clients.Group(query.GroupName).SendAsync("ReceiveGroupMessage", res.Data);
        }
        public async Task CreateGroup(GroupMembersResponse resonse)
        {
            foreach (var user in resonse.Users)
            {
                if (connectedUsers.ContainsKey(user))
                {
                    string connectionId = connectedUsers[user];
                    await Groups.AddToGroupAsync(connectionId, resonse.Group.Name);
                }
            }
            await Clients.Group(resonse.Group.Name).SendAsync("ReciveGroup", resonse.Group);
        }
        
       
        public async Task DeleteFriendMessage(DeleteFriendMessageCommand command)
        {
            var message =  await Sender.Send(command);
            var msg=(FriendMessageResponse) message.Data;
            var query = new GetFriendIdQuery(command.friendShipId,msg.SenderId);
            var result = await Sender.Send(query);
            var reciverId = (string)result.Data;
            string connectionSenderId = connectedUsers[msg.SenderId];

            if (connectedUsers.ContainsKey(reciverId))
            {
                string connectionReciverId =connectedUsers[reciverId];

                await Clients.Clients(connectionReciverId, connectionSenderId).SendAsync("DeleteMessage", msg);
            }
            else
            {
                await Clients.Client(connectionSenderId).SendAsync("DeleteMessage", msg);
            }
        }

        public async Task DeleteGroupMessage(DeleteGroupMessageCommand command)
        {
            var message = await Sender.Send(command);
            await Clients.Group(command.groupName).SendAsync("DeleteGroupMessage", message.Data);

        }
      
    }

}