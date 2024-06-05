import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
//import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { AcountModule } from './acount/acount.module';
import { RouterModule } from '@angular/router';
//import { AuthInterceptor } from './service/auth.interceptor';
import { NavbarComponent } from './navbar/navbar.component';
//import { TextInputComponent } from './service/text-input/text-input.component';
//import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ChatLogoComponent } from './chat/chat-logo/chat-logo.component';
import { AppRoutingModule } from './app-routing.module';
import { AcountModule } from './acount/acount.module';
import { ClientChatComponent } from './chat/client-chat/client-chat.component';
import { FriendsComponent } from './chat/friends/friends.component';
import { AddFriendComponent } from './chat/add-friend/add-friend.component';
import { FriendsListComponent } from './chat/client-chat/friends-list/friends-list.component';
import { FriendChatComponent } from './chat/client-chat/friend-chat/friend-chat.component';
import { AddGroupComponent } from './chat/add-group/AddGroupComponent';
import { GroupChatComponent } from './chat/client-chat/group-chat/group-chat.component';
import { FriendRequestComponent } from './chat/friend-request/friend-request.component';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
  declarations: [
    AppComponent,
   NavbarComponent,
   ChatLogoComponent,
   ClientChatComponent,
   FriendsComponent,
   AddFriendComponent,
   FriendsListComponent,
   FriendChatComponent,
   AddGroupComponent,
   GroupChatComponent,
   FriendRequestComponent
  ],
 /*providers: [
    {
      provide: HTTP_INTERCEPTORS,
    //  useClass: AuthInterceptor,
      multi: true,
    },
    TextInputComponent
  ],*/
  imports: [
    BrowserModule,
    AcountModule,
   AppRoutingModule,
    HttpClientModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgbDropdownModule,

  
  ],
 
  bootstrap: [AppComponent]
})
export class AppModule { }
