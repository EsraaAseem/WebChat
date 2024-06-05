import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FriendRequest } from './friend-request.model';
import {Observable, Subject } from 'rxjs';
import {  FriendDto } from './friend.dto.model';
import { HttpClient } from '@angular/common/http';
import { FrienChatdDto } from './friend-chat-dto-model';
import * as signalR from '@microsoft/signalr';
import { FriendsChat } from './friends-chat';
import { GroupDto } from './group-dto.model';
import { GroupWithMessages } from './group-with-messages.model';
import { CreateGroupMessageDto } from './create-group-message.model';
import { BaseResponse } from './base-response.model';
import { FriendRequestResponseDto } from './friend-request-response-dto.model';
import { GroupFriendMessageDto } from './group-friend-message-dto.model';
import { UserChatsDto } from './user-chats-dto.model';
import { AcceptFriendRequestDto } from './Accept-friend-request-dto.model';
import { UserProfileDto } from './user-profile-dto.model';
import { FriendWithChat } from './friend-with-chat-model';
import { DeleteMessageDto } from './delete-message-dto.model';
import { GroupMemberDto } from './group-members-dto.model';
import { GroupMessagesDto } from './group-messages-dto.model';
import { DelGroupMsg } from './del-group-msg-dto.model';
import { GroupImg } from './group-img-dto.model';

@Injectable({
  providedIn: 'root'
})
export class FriendServiceService {
  baseapiurl:string=environment.baseurl;
  private Connection:signalR.HubConnection;
  private messageSubject: Subject<any> = new Subject<any>();
  private groupMessagesSubject: Subject<any> = new Subject<any>();
  private friendRequestSubject: Subject<any> = new Subject<any>();
  private friendsSubject: Subject<any> = new Subject<any>();
  private DeleteMessageSubject: Subject<any> = new Subject<any>();
  private delGroupMsgSubject:Subject<any> = new Subject<any>();
  private GroupSubject: Subject<any> = new Subject<any>();
  public friendWithChatSubject: Subject<any> = new Subject<any>();

  constructor(private http: HttpClient) 
  {
    this.start();
    this.messagesOnConnect();
    this.groupMessagesOnConnect();
    this.updateConnectedUsers();
    this.ReceiveFriendRequestOnConnect();
    this.ReceiveNewFriendOnConnect();
    this.ReceiveMessageNotifactionOnConnect();
    this.DeleteMessageOnConnect();
    this.delGroupMessageOnConnect();
    this.OnGroupCreated();
  }
  public  start()
  {
    try
   {
    this.Connection=new signalR.HubConnectionBuilder().withUrl(this.baseapiurl+"/chatHub")
    .configureLogging(signalR.LogLevel.Information).build();

     this.Connection.start().then(()=>{console.log("connected success")})
   }
  catch(error)
  {
    console.log("error in start :::: "+error)
  }
 }
 public messagesOnConnect = () => {
  this.Connection.on('ReceiveMessage', (friendsChat:FriendsChat) => {
    console.log("friends chat on connect")
    this.messageSubject.next(friendsChat);
  }
  ),error=>{console.log("error on connectio")}}
public  ReceiveFriendRequestOnConnect=()=>{
  this.Connection.on('ReceiveFriendRequest',(friend:FriendRequestResponseDto)=>{
    "recive on connect sueccess"
  this.friendRequestSubject.next(friend);
  console.log("from friend request on connect",this.friendRequestSubject)
  })

}
public DeleteMessageOnConnect = () => {
  this.Connection.on('DeleteMessage', (friendsChat:FriendsChat) => {
    console.log("from group  on connect")
    this.DeleteMessageSubject.next(friendsChat);
  }
  ),error=>{console.log("error on connection")}

}
public delGroupMessageOnConnect = () => {
  this.Connection.on('DeleteGroupMessage', (groupMsg:GroupMessagesDto) => {
    console.log("from del on conect")
    this.delGroupMsgSubject.next(groupMsg);
  }
  ),error=>{console.log("error on connection")}
}

  public groupMessagesOnConnect = () => {
    this.Connection.on('ReceiveGroupMessage', (groupMessage:GroupFriendMessageDto) => {
      this.groupMessagesSubject.next(groupMessage);
    }
    ),error=>{console.log("error on connectio")}
  }
  public updateConnectedUsers=()=>{
    this.Connection.on('UpdateConnectedUsers', (updatedConnectedUsers) => {
      console.log("update users on connect"+updatedConnectedUsers);
    });
  }
  public ReceiveNewFriendOnConnect=()=>{
    this.Connection.on('ReceiveNewFriend', (friendDto:FriendDto) => {
      this.friendsSubject.next(friendDto);
    });
  }
  public ReceiveMessageNotifactionOnConnect=()=>{
    this.Connection.on('ReceiveMessageNotifaction', (message:string) => {
      console.log("recive message when user accept friend request"+message);
    });
  }
  public OnGroupCreated = () => {
    this.Connection.on('ReciveGroup', (group:UserChatsDto) => {
      console.log("from group on connect")

 //     this.GroupSubject.next(group);
   this.friendWithChatSubject.next(group);
    }
    ),error=>{console.log("error on connectio")}
  }
    
  getMessageObservable() {
    return this.messageSubject.asObservable();
  }
  getMessageOnDeletedObservable() {
    return this.DeleteMessageSubject.asObservable();
  }
  getGroupMsgOnDelObservable() {
    return this.delGroupMsgSubject.asObservable();
  }
  getGroupMessageObservable() {
    return this.groupMessagesSubject.asObservable();
  }
  getGroupObservable() {
    return this.GroupSubject.asObservable();
  }
  getfriendRequestsObservable()
  {
    return this.friendRequestSubject.asObservable();
  }
  getfriendWithChatObservable()
  {
    return this.friendWithChatSubject.asObservable();
  }
  getfriendsObservable()
  {
    return this.friendsSubject.asObservable();
  }
  public sendMessage = (message:FrienChatdDto) => {
    return this.Connection
       .invoke('SendMessage',message)
       .catch(
        err =>{console.error(err),
          console.log("error in send")}
        );
   }
   public sendGroupMessage = (message:CreateGroupMessageDto) => {
    return this.Connection
       .invoke('SendGroupMessage',message)
       .catch(
        err =>{console.error(err),
          console.log("error in send")}
        );
   }
   public CreateGroup = (group:GroupMemberDto) => {
    return this.Connection
       .invoke('CreateGroup',group)
       .catch(
        err =>{console.error(err),
          console.log("error in send")}
        );
   }
   public CreateFriendRequest = (friendRequest:FriendRequest) => {
    return this.Connection
       .invoke('SendFriendRequest',friendRequest)
       .catch(
        err =>{console.error(err),
          console.log("error in send")}
        );
   }
   public deleteFriendMessage = (deleteRequest:DeleteMessageDto) => {
    return this.Connection
       .invoke('DeleteFriendMessage',deleteRequest)
       .catch(
        err =>{console.error(err),
          console.log("error in send")}
        );
   }
   public deleteGroupMessage = (deleteRequest:DelGroupMsg) => {
    return this.Connection
       .invoke('DeleteGroupMessage',deleteRequest)
       .catch(
        err =>{console.error(err),
          console.log("error in send")}
        );
   }

   public AcceptFriendRequest = (friendResponse:AcceptFriendRequestDto) => {
    return this.Connection
       .invoke('AcceptFriendRequest',friendResponse)
       .catch(
        err =>{console.error(err),
          console.log("error in  Accep friend request invoke")}
        );
   }

   public  AddConnectedUsers = (userId:string) => {
    return  this.Connection
       .invoke('OnConnectUser',userId)
       .catch(
        error=>{console.error(error)
          //console.log("error in Add Connect User")
        }
        );
   }

  addFriend(friend: FriendRequest): Observable<any> {
    return this.http.post(`${this.baseapiurl}/api/Friends`, friend);
  }
  createMessage(friendDto:FrienChatdDto)
  {
    return this.http.post(`${this.baseapiurl}/api/Friends/createMesssage`,friendDto);
  }
  getFriend(friendShipId:number,userId: string): Observable< BaseResponse<FriendWithChat>> {
    return this.http.get<BaseResponse<FriendWithChat>>(`${this.baseapiurl}/api/Messages/friend/${friendShipId}/${userId}`);
  }
  getFriendId(friendShipId:number,userId: string): Observable<any> {
    return this.http.get<any>(`${this.baseapiurl}/api/Friends/friendId/${friendShipId}/${userId}`);
  }

  getFriends(userId: string): Observable<BaseResponse<FriendDto[]>> {
    return this.http.get<BaseResponse<FriendDto[]>>(`${this.baseapiurl}/api/Friends/friends/${userId}`);
  }
  
  getFriendsRequests(userId: string): Observable<BaseResponse<FriendRequestResponseDto[]>> {
    return this.http.get<BaseResponse<FriendRequestResponseDto[]>>(`${this.baseapiurl}/api/Friends/friends/requests/${userId}`);
  }
  getUsers(): Observable<FriendDto[]> {
    return this.http.get<FriendDto[]>(`${this.baseapiurl}/api/Friends`);
  }
  

  getUserChats(userId:string):Observable<BaseResponse<UserChatsDto[]>>{
    return this.http.get<BaseResponse<UserChatsDto[]>>(`${this.baseapiurl}/api/Friends/user/messsages/${userId}`);

  }
  getUseProfile(userId:string):Observable<BaseResponse<UserProfileDto>>{
    console.log("friend id"+userId)
    return this.http.get<BaseResponse<UserProfileDto>>(`${this.baseapiurl}/api/Friends/user/profile/${userId}`);

  }
  
  getGroup(groupId: number): Observable<BaseResponse<GroupMessagesDto[]>> {
    return this.http.get<BaseResponse<GroupMessagesDto[]>>(`${this.baseapiurl}/api/Messages/getGroupMesssages/${groupId}`);
  }
  getGroupImg(groupId: number): Observable<BaseResponse<GroupImg>> {
    return this.http.get<BaseResponse<GroupImg>>(`${this.baseapiurl}/api/Groups/groupImg/${groupId}`);
  }

  createGroupAsync(groupData: FormData): Observable<BaseResponse<GroupMemberDto>> {
    return this.http.post<BaseResponse<GroupMemberDto>>(`${this.baseapiurl}/api/Groups/createGroup`, groupData);
  }
}
