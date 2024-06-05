import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HubConnection } from '@microsoft/signalr';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { FriendDto } from 'src/app/service/friend.dto.model';
import { User } from 'src/app/service/user.model';
import { CreateGroupDto } from 'src/app/service/create-group-dto-model.';
import { GroupDto } from 'src/app/service/group-dto.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css']
})
export class AddGroupComponent implements OnInit {
  hubConnection: HubConnection;
  currentUser: User;
  friends: FriendDto[] = [];
  groupForm: FormGroup;
  users: string[] = [];
  formData: FormData = new FormData();
  groupDto?: GroupDto;

  constructor(private fb: FormBuilder,private router:Router, private friendService: FriendServiceService, private clientservice: ClientServiceService) {
   
  }

 groupData: CreateGroupDto = {
    groupName: '',
    users:[''],
        createdGroupBy:'',
    groupimg: null,
    
  };

  ngOnInit(): void {
    this.friends.forEach(friend => friend.isSelected = false);

    this.clientservice.user.subscribe(user => this.currentUser = user);
    this.friendService.getFriends(this.currentUser.id).subscribe(
      (friends) => {
        this.friends = friends.data;
        console.log('User friends:', friends);
      },
      (error) => {
        console.error('Error getting friends', error);
      }
    );
  }
  createGroupOnSubmit(): void {
    const selectedUsers = this.friends.filter(friend => friend.isSelected).map(friend => friend.friendId);
    console.log('Selected Users:', selectedUsers);
    const formData = new FormData();
    formData.append('groupName', this.groupData.groupName);
    formData.append('groupimg', this.groupData.groupimg);
    formData.append('createdGroupBy', this.currentUser.id);
   // formData.append('UserImg', this.registerData.userImg);
   selectedUsers.forEach(userId => formData.append('users[]', userId));

   /*if (this.groupData.users) {
    this.groupData.users?.forEach(userId => formData.append('users[]', userId));
  }*/
  
 console.log(formData);
   this.friendService.createGroupAsync(formData).subscribe(
      response => {
        console.log('create group success successful', response);
        this.friendService.CreateGroup(response.data).then(()=>{
          console.log("invoke success");
        })
        this.router.navigate(['/chat/client']);
  
      },
      error => {
        console.error('Registration failed', error);
        // Handle registration error here
      }
    );
  }
  
  onFileSelected(event: any): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.groupData.groupimg = file;
    }
  }
}