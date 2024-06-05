import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@microsoft/signalr';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-client-chat',
  templateUrl: './client-chat.component.html',
  styleUrls: ['./client-chat.component.css']
})
export class ClientChatComponent implements OnInit{

  loggedInUser = JSON.parse(localStorage.getItem("login-user"))
  users:any;
  chatUser:any;
  messages: any[] = [];
  displayMessages: any[] = []
  message: string
  hubConnection: HubConnection;
  currentUser: User;
  constructor( private friendService: FriendServiceService,private clientservice:ClientServiceService) {
  }
  ngOnInit(): void {
    this.clientservice.user.subscribe(user => this.currentUser = user);
    this.friendService.AddConnectedUsers(this.currentUser.id).then((e)=>{
      console.log("in client chat add user success"+e)
    });
  }
}
