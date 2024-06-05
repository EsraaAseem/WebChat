import { Component, OnInit } from '@angular/core';
import { User } from './service/user.model';
import { ClientServiceService } from './service/client-service.service';
import { Router } from '@angular/router';
import { FriendServiceService } from './service/friend-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  messages: any[] = [];
  currentUser: User;
  constructor( private friendService: FriendServiceService,private clientservice:ClientServiceService) {
  }
  ngOnInit(): void {
    //this.friendService.getUsers().subscribe(()=>{})
    this.clientservice.user.subscribe(user => this.currentUser = user);
    /*this.friendService.AddConnectedUsers(this.currentUser.id).then((e)=>{
      console.log("in client chat add user success"+e)
    })*/
  }

}
