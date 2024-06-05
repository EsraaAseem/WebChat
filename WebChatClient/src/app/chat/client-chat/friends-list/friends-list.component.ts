
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HubConnection } from '@microsoft/signalr';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { UserChatsDto } from 'src/app/service/user-chats-dto.model';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-friends-list',
  templateUrl: './friends-list.component.html',
  styleUrls: ['./friends-list.component.css']
})
export class FriendsListComponent implements OnInit{

  users:any;
  chatUser:any;
  messages: any[] = [];
  displayMessages: any[] = []
  message: string
  hubConnection: HubConnection;
  currentUser: User;
  userChats:UserChatsDto[]=[];
  friendId:string;
  constructor(private router:Router, private friendService: FriendServiceService,private clientservice:ClientServiceService) {
  }
  ngOnInit(): void {
    this.clientservice.user.subscribe(user => this.currentUser = user);
    this.loadData();
   
  }
  getChat(name:string,id:number,type:string,groupImg:string)
  {
   if(type=="Friend")
    {
      const url = `/chat/client/friend/${id}`;
     this.router.navigate([url]);
    } else{
      const url = `/chat/client/group/${id}/${name}`;
      this.router.navigate([url]);
    }
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