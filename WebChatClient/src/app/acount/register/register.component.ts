import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, } from '@angular/forms';
import { Router } from '@angular/router';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { FriendServiceService } from 'src/app/service/friend-service.service';
import { Register } from 'src/app/service/register.model';
import { User } from 'src/app/service/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  RegisterForm:FormGroup;
  userdto:User;
  error:string=null;
  currentUser: User;
  formData: FormData = new FormData();


  
  registerData: Register = {
    password: '',
    name: '',
    phoneNumber: '',
    userName: '',
    userImg: null
  };

constructor(private acountserve:ClientServiceService,private router:Router,private frindService:FriendServiceService,private fb: FormBuilder){}
  ngOnInit(): void {
    //this.regForm();
    this.acountserve.user.subscribe(user => this.currentUser = user);

  }
  
registerUser(): void {
  const formData = new FormData();
  formData.append('Password', this.registerData.password);
  formData.append('Name', this.registerData.name);
  formData.append('PhoneNumber', this.registerData.phoneNumber);
  formData.append('UserName', this.registerData.userName);
  formData.append('UserImg', this.registerData.userImg);

  this.acountserve.registerUser(formData).subscribe(
    response => {
      console.log('Registration successful', response);
      this.router.navigate(['/chat/login']);

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
    this.registerData.userImg = file;
  }
}




onclose()
  {
    this.error=null;
  }
onCancel(){}
}
