import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { HubConnection } from '@microsoft/signalr';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { DeleteMessageDto } from 'src/app/service/delete-message-dto.model';
import { FrienChatdDto } from 'src/app/service/friend-chat-dto-model';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { UserChatsDto } from 'src/app/service/user-chats-dto.model';
import { UserProfileDto } from 'src/app/service/user-profile-dto.model';
import { User } from 'src/app/service/user.model';
//import { FriendWithChat } from 'src/app/service/friend-with-chat-model';

@Component({
  selector: 'app-friend-chat',
  templateUrl: './friend-chat.component.html',
  styleUrls: ['./friend-chat.component.css']
})

export class FriendChatComponent implements OnInit{
  friends:FrienChatdDto;
  loggedInUser = JSON.parse(localStorage.getItem("login-user"))
  users:any;
  chatUser:any;
  //friendmessages: FriendWithChat;
  message: string
  hubConnection: HubConnection;
  currentUser: User;
  id:number;
  friendId:string;
  friendName:string;
  friendImgUrl:string;
  senderId:string;
  content:string;
  messages:any[]=[]
  userChats:UserChatsDto[]=[];

  friendDto:FrienChatdDto;
  friendProfile:UserProfileDto;
  constructor( private friendService: FriendServiceService,private clientservice:ClientServiceService
    ,private route:ActivatedRoute,private router:Router) {
  }
  ngOnInit(): void {
   
    this.clientservice.user.subscribe(user => this.currentUser = user);
    this.route.params.subscribe((params:Params)=>{this.id=+params['friendshipId'];})
    
    this.friendService.getFriend(this.id,this.currentUser.id).subscribe(
      (friends) => {
        this.messages=friends.data.friendsChat;
        this.friendImgUrl=friends.data.senderImgUrl;
        this.friendName=friends.data.senderName;
        console.log(friends)
      },
      (error) => {
        console.error('Error getting friends', error);
      }
    );
   
    this.friendService.getMessageObservable().subscribe((message) => {
      this.messages.push(message);
    });
    this.friendService.getMessageOnDeletedObservable().subscribe((message) => {
      const index = this.messages.findIndex(msg => msg.messageId === message.messageId);
      if (index !== -1)
         {
        this.messages[index].isDeleteByReciver= message.isDeleteByReciver;
        this.messages[index].isDeleteBySender= message.isDeleteBySender;
      }
        });
  }
  sendMessage()
  {
    const messageToSend: FrienChatdDto = {
      friendShipId:this.id,
      senderId: this.currentUser.id,
      content: this.message
  };    
   this.friendService.sendMessage(messageToSend).then(()=>{
    this.message='';
    console.log("invoke success in send message")
    this.loadData();
  }).catch((error)=>{console.log(error)});
  }
  DeleteMessage(messageId:number,deleteType:number)
  {
    const msg:DeleteMessageDto={
      messageId:messageId,
      isDeleteFor:deleteType,
      friendShipId:this.id
    }
    this.friendService.deleteFriendMessage(msg).then(()=>{
      console.log("invoke success");
    })
  }
  loadData()
  {
    this.friendService.getUserChats(this.currentUser.id).subscribe(
      (chats) => {this.userChats=chats.data;},
      (error) => {console.error('Error getting friends', error);});
      this.friendService.getfriendWithChatObservable().subscribe((message) => {
        this.userChats.push(message);
      });
}
}
