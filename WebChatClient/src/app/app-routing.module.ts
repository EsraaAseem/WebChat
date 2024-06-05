import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatLogoComponent } from './chat/chat-logo/chat-logo.component';
import { LoginComponent } from './acount/login/login.component';
import { RegisterComponent } from './acount/register/register.component';
import { ClientChatComponent } from './chat/client-chat/client-chat.component';
import { AddFriendComponent } from './chat/add-friend/add-friend.component';
import { FriendsComponent } from './chat/friends/friends.component';
import { FriendsListComponent } from './chat/client-chat/friends-list/friends-list.component';
import { FriendChatComponent } from './chat/client-chat/friend-chat/friend-chat.component';
import { AddGroupComponent } from './chat/add-group/AddGroupComponent';
import { GroupChatComponent } from './chat/client-chat/group-chat/group-chat.component';
import { FriendRequestComponent } from './chat/friend-request/friend-request.component';
const approute:Routes=[
 {path:'',redirectTo:'/chat-logo',pathMatch:'full'}, 
 {path:'chat/client',component:ClientChatComponent,children:[
  //{path:'',component:FriendsListComponent},
  {path:'group/:groupId/:groupName',component:GroupChatComponent},
  {path:'friend/:friendshipId',component:FriendChatComponent},

]

 },
 {path:'chat-logo',component:ChatLogoComponent},
 {path:'chat/login',component:LoginComponent},
 {path:'chat/friendRequest',component:FriendRequestComponent},
 {path:'chat/register',component:RegisterComponent},
 {path:'chat/addFrind',component:AddFriendComponent},
 {path:'chat/getFriends',component:FriendsComponent},
 {path:'chat/addGroup',component:AddGroupComponent},

 
]
@NgModule({
  imports:[RouterModule.forRoot(approute)],
  
  exports:[RouterModule]
})
export class AppRoutingModule { }
