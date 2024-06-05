import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-add-friend',
  templateUrl: './add-friend.component.html',
  styleUrls: ['./add-friend.component.css']
})
export class AddFriendComponent implements OnInit{
  friendForm: FormGroup;
  currentUser: User;
  friends:any[];
  constructor(private fb: FormBuilder,private router:Router, private friendService: FriendServiceService,private clientservice:ClientServiceService) {
    this.friendForm = this.fb.group({
      phoneNumber: ['', Validators.required],
    });
  }
  ngOnInit(): void {

    this.clientservice.user.subscribe(user => this.currentUser = user);
    this.friendService.getFriendsRequests(this.currentUser.id).subscribe(
      (friends) => {this.friends=friends.data},(error) => { console.error('Error getting friends', error);}
    );
    this.friendService.getfriendRequestsObservable().subscribe((request) => {
      this.friends.push(request);
      console.log("update friend request form observal"+request)
    });
  }

  addFriend() {
    if (this.friendForm.valid) {
      const friendRequest = {
        friendPhone: this.friendForm.get('phoneNumber').value,
        userId: this.currentUser.id, 
      };     
      this.friendService.CreateFriendRequest(friendRequest).then(()=>{
        console.log("invoke"),
        this.router.navigate(['/chat/client'])

      }).catch((error)=>{console.log(error)});
    } 
    else 
    {
      console.error('Invalid form data');
    }
  }
}


