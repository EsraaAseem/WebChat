import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { FriendDto } from 'src/app/service/friend.dto.model';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit{
  currentUser: User;
  friends:FriendDto[]=[];
  constructor(private router:Router, private friendService: FriendServiceService,private clientservice:ClientServiceService) {
  }
  ngOnInit(): void {
    this.clientservice.user.subscribe(user => this.currentUser = user);
    this.friendService.getFriends(this.currentUser.id).subscribe(
      (friends) => {
        this.friends=friends.data;
        console.log('User friends:', friends);
      },
      (error) => {
        console.error('Error getting friends', error);
      }
    );

    this.friendService.getfriendsObservable().subscribe((request) => {
      this.friends.push(request);
      console.log("update friends form observal"+request)
    });
  }

  getChat(id:number)
  {
   
      const url = `/chat/client/friend/${id}`;
     this.router.navigate([url]);    
     
  }
}
