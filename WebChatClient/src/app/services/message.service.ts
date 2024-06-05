import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
  })
  export class MessageService {
     BaseURI:string = environment.baseurl;
    constructor(private http: HttpClient){

    }
    getUserReceivedMessages(userId:string) {
        return this.http.get(this.BaseURI + '/message');
      }
      deleteMessage(message) {
        return this.http.post(this.BaseURI + '/message',message);
      }
  }