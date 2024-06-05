import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-chat-logo',
  templateUrl: './chat-logo.component.html',
  styleUrls: ['./chat-logo.component.css']
})
export class ChatLogoComponent implements OnInit{

  currentUser: User;
  constructor( private friendService: FriendServiceService,private clientservice:ClientServiceService) {
  }
  ngOnInit(): void {
    this.clientservice.user.subscribe(user => this.currentUser = user);
   /* if (this.currentUser!=null)
      {
    this.friendService.AddConnectedUsers(this.currentUser.id).then((e)=>{
      console.log("in client chat add user success"+e)
    })
  }*/
  }
}
