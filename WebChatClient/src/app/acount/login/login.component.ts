import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first, Observable, Subscription } from 'rxjs';
import { Login } from 'src/app/service/login.model';
import { ClientServiceService } from 'src/app/service/client-service.service';
import { User } from 'src/app/service/user.model';
import { FriendServiceService } from 'src/app/service/friend-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  error:string=null;
  currentuser:Subscription;
   authobsuser:Observable<User>;
    login:Login={
      userName:'',
      password:''
    }
 // loginform:FormGroup
  constructor(private clientservice:ClientServiceService,private router:Router,private frindService:FriendServiceService){}
  ngOnInit(): void {
    //this.privateform();
   console.log(localStorage.getItem("token"));
   this.currentuser=this.clientservice.user.subscribe();
   
  }
  onSubmitForm(sub:NgForm){
    if(!sub.valid)
    {
      return;
    }    
    const userName=sub.value.userName;
    const password=sub.value.password;
      this.clientservice.Login(userName,password)
        .subscribe({
          next: (res) => {       
           localStorage.setItem('authToken', res.token);
           this.router.navigate(['/chat/client']);},
          error: (error) => {
            this.error = error;
            console.log("error")
            console.log(error)
          },
        });
      
  }
  onclose()
  {
    this.error=null;
  }
}
