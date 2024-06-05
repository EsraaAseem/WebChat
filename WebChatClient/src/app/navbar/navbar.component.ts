import { Component, OnInit } from '@angular/core';
import { User } from '../service/user.model';
import { Observable, Subscription, first } from 'rxjs';
import { Router } from '@angular/router';
import { ClientServiceService } from '../service/client-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  public gfg = false;
  currentUser: User;
  constructor(private clientservice:ClientServiceService,private router:Router){}
  ngOnInit(): void {
    this.clientservice.user.subscribe(user => 
      this.currentUser = user);
    //console.log("current user"+JSON.stringify(this.currentUser)); 
   }
  
    onLogout(){
      this.clientservice.LogOut();
      this.router.navigate(['/chat-logo']);
    }
}
