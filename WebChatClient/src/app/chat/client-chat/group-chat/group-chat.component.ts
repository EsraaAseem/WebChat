
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { HubConnection } from '@microsoft/signalr';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { CreateGroupMessageDto } from 'src/app/service/create-group-message.model';
import { DelGroupMsg } from 'src/app/service/del-group-msg-dto.model';
import { FrienChatdDto } from 'src/app/service/friend-chat-dto-model';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { GroupFriendMessageDto } from 'src/app/service/group-friend-message-dto.model';
import { GroupMessagesDto } from 'src/app/service/group-messages-dto.model';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-group-chat',
  templateUrl: './group-chat.component.html',
  styleUrls: ['./group-chat.component.css']
})
export class GroupChatComponent  implements OnInit{
  friends:FrienChatdDto;
  loggedInUser = JSON.parse(localStorage.getItem("login-user"))
  users:any;
  chatUser:any;
  messages: GroupMessagesDto[] = [];
  displayMessages: any[] = []
  message: string
  hubConnection: HubConnection;
  currentUser: User;
  groupId:number;
  groupName:string;
  senderId:string;
  content:string;
  friendDto:FrienChatdDto;
  groupImg:any;
  constructor( private friendService: FriendServiceService,private clientservice:ClientServiceService,private route:ActivatedRoute,private router:Router) {
  }
  ngOnInit(): void {
    
    this.clientservice.user.subscribe(user => this.currentUser = user);
    console.log("current user Id"+this.currentUser.id)
    this.route.params.subscribe((params:Params)=>{
      this.groupId=+params['groupId'];
      this.groupName=params['groupName'];
      //this.groupImg=params["groupImg"];
      this.friendService.getGroupImg(+params['groupId']).subscribe((e)=>{
        this.groupImg=e.data
      console.log("group img"+e.data)
      })

    })
    this.friendService.getGroup(this.groupId).subscribe(
      (group) => {
         this.messages=group.data;
         console.log(this.messages)
      },
      (error) => {
        console.error('Error getting group messages', error);
      }
    );
    this.friendService.getGroupMessageObservable().subscribe((message) => {
      this.messages.push(message);
    });

    this.friendService.getGroupMsgOnDelObservable().subscribe((message) => {
      const index = this.messages.findIndex(msg => msg.messageId === message.messageId);
      console.log(message);
         if (index !== -1) {
          this.messages[index].isDeleteByReciver= message.isDeleteByReciver;
          this.messages[index].isDeleteBySender= message.isDeleteBySender;
          this.messages[index].deletedForUserIds=message.deletedForUserIds;
          console.log("from index"+this.messages[index])
          }
        });
       
  }
  sendMessage()
  {
    const messageToSend: CreateGroupMessageDto = {
      groupId:this.groupId,
      senderId: this.currentUser.id,
      content: this.message,
      groupName:this.groupName
      
  };    
    console.log(messageToSend);
   this.friendService.sendGroupMessage(messageToSend).then(()=>{
    this.message='';
    console.log("invoke success")
  }).catch((error)=>{console.log(error)});
  }
  DeleteGroupMessage(messageId:number,deleteType:number,userId?:string)
  {
    const msg:DelGroupMsg={
      messageId:messageId,
      isDeleteFor:deleteType,
      groupName:this.groupName,
      userId:userId
    }
    this.friendService.deleteGroupMessage(msg).then(()=>{
      console.log("invoke success");
    })
  }
  isUserDeleted(groupMessage: GroupMessagesDto): boolean {
    return groupMessage.deletedForUserIds?.some(userDelGroupMsgDto => userDelGroupMsgDto.userId === groupMessage.senderId);
  }
  
}

