import { UserDelGroupMsgDto } from "./user-del-group-msg-dto.model";

export interface GroupMessagesDto{
    senderId:string,
    messageTime:string,
    content?:string,
    isDeleteBySender:number
    isDeleteByReciver:number
    messageId:number ,
    senderName:string,
    senderImgUrl:string,
    deletedForUserIds?:UserDelGroupMsgDto[];
}