import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from './user.model';
import { BaseResponse } from './base-response.model';

@Injectable({
  providedIn: 'root'
})
export class ClientServiceService {
  public userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  resault:User;
  baseapiurl:string=environment.baseurl;
  constructor(private http:HttpClient,private router:Router) {
    this.userSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem('currentUser')));
      this.user = this.userSubject.asObservable();
   }
   public get userValue(): User {
    return this.userSubject.value;
  }
 Login(userName:string,password:string){
  return this.http.post<BaseResponse<User>>(this.baseapiurl+'/api/Auth/Login',{
    userName:userName,
    password:password
  }).pipe(
    map((response/*{isAuthenticated,userName,id,expiresOn,token}*/) => {
      let user: User = {
        isAuthenticated:response.data.isAuthenticated,
        id: response.data.id,
        userName:response.data.userName,
        expiresOn:response.data.expiresOn,
        token: response.data.token,
        imgUrl:response.data.imgUrl
      };
      this.userSubject.next(user);

      localStorage.setItem('currentUser', JSON.stringify(user));
      return user;
    })
  );
}
 

 LogOut(){
  localStorage.removeItem('currentUser');
  localStorage.removeItem('authToken');
 this.userSubject.next(null);

 }

    registerUser(registerData:FormData): Observable<any> {
      return this.http.post<any>(`${this.baseapiurl}/api/Auth/Register`, registerData);
    }

}
