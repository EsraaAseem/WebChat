import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { AcceptFriendRequestDto } from 'src/app/service/Accept-friend-request-dto.model';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { FriendRequestResponseDto } from 'src/app/service/friend-request-response-dto.model';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { FriendDto } from 'src/app/service/friend.dto.model';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-friend-request',
  templateUrl: './friend-request.component.html',
  styleUrls: ['./friend-request.component.css']
})
export class FriendRequestComponent implements OnInit {
  currentUser: User;
  friends:FriendRequestResponseDto[]=[];
  friendDto:FriendDto;
  constructor( private friendService: FriendServiceService,private router:Router,private clientservice:ClientServiceService) {
  }
  
  ngOnInit(): void {
    //this.friendService.getUsers().subscribe((e)=>{ })
    this.clientservice.user.subscribe(user => this.currentUser = user);
    this.friendService.getFriendsRequests(this.currentUser.id).subscribe(
      (friends) => {this.friends=friends.data},(error) => { console.error('Error getting friends', error);}
    );
    this.friendService.getfriendRequestsObservable().subscribe((request) => {
      this.friends.push(request);
      console.log("request"+request)
      console.log("update friend request form observal"+request)
    });
  }
  acceptRequest(friendId:string,friendShipId:number)
  {
   const friendResponse:AcceptFriendRequestDto={
    friendShipId:friendShipId,
    friendId:friendId,
    confirmRequest:true
   }
   console.log("friend response"+friendResponse)
   this.friendService.AcceptFriendRequest(friendResponse).then(()=>{

    console.log("Accept friend on invoke")
    this.friendService.getFriendsRequests(this.currentUser.id).subscribe(
      (friends) =>
        
        {this.friends=friends.data},
      
      (error) => { console.error('Error getting friends', error);}
    );
  //  this.router.navigate(['/chat/client'])

   })
  }
  refusedRequest(friendId:string,friendShipId:number)
  {
    const friendResponse:AcceptFriendRequestDto={
      friendShipId:friendShipId,
      friendId:friendId,
      confirmRequest:false
     }
     this.friendService.AcceptFriendRequest(friendResponse).then(()=>{
      console.log("Refuased friend on invoke")
      this.friendService.getFriendsRequests(this.currentUser.id).subscribe(
        (friends) => {this.friends=friends.data},(error) => { console.error('Error getting friends', error);}
      );
      //this.router.navigate(['/chat/client'])
     })
  }
}
